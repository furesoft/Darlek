using Darlek.Core.GrocySync.Dto;
using Darlek.Core.GrocySync.Models;
using InterpolatedParsing;
using LiteDB;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Darlek.Core.GrocySync;

public static class GrocySyncService
{
    private static readonly GrocyClient _client = new();

    private static readonly List<Product> _products;
    private static readonly List<QuantityUnit> _quantityUnits;

    static GrocySyncService()
    {
        _products = _client.GetAllProducts().OrderBy(p => p.Name).ToList();
        _quantityUnits = _client.GetAllQuantityUnits().OrderBy(qu => qu.Name).ToList();
    }

    private static Ingredient ResolveIngredient(string productName, string measure)
    {
        var product = _products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        var ingr = new Ingredient
        {
            Product = product,
            ProductName = productName,
            Measure = { Source = measure }
        };

        if (ingr.Product == null)
        {
            ingr.Product = ManualResolveSelector("product", productName, _products, p => p.Name);
        }

        double quantity = 0;
        string unit = null;

        try
        {
            InterpolatedParser.Parse(measure, $"{quantity} {unit}");
            ingr.Measure.Quantity = quantity;
            ingr.Measure.QuantityUnit = _quantityUnits.Find(qu => qu.Name == unit ||
                                                                 qu.Userfields["symbol"] == unit || qu.NamePlural == unit
            );

            if (!ingr.Measure.IsResolved)
            {
                ingr.Measure.VariableAmount = ingr.Measure.Source;
            }
        }
        catch
        {
            ingr.Measure.QuantityUnit ??= ManualResolveSelector("quantity unit", measure, _quantityUnits, qu => qu.Name);
        }

        return ingr;
    }

    private static int StringSimilarity(string a, string b)
    {
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return int.MaxValue;
        a = a.ToLowerInvariant();
        b = b.ToLowerInvariant();
        if (a == b) return 0;
        if (a.StartsWith(b) || b.StartsWith(a)) return 1;
        if (a.Contains(b) || b.Contains(a)) return 2;

        var d = new int[a.Length + 1, b.Length + 1];
        for (int i = 0; i <= a.Length; i++)
        {
            d[i, 0] = i;
        }

        for (int j = 0; j <= b.Length; j++)
        {
            d[0, j] = j;
        }

        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                d[i, j] = a[i - 1] == b[j - 1]
                    ? d[i - 1, j - 1]
                    : 1 + Math.Min(Math.Min(d[i - 1, j], d[i, j - 1]), d[i - 1, j - 1]);
            }
        }

        return d[a.Length, b.Length] + 3;
    }

    private static T ManualResolveSelector<T>(string kind, string unresolvableElement, IEnumerable<T> list, Func<T, string> converter = null)
        where T : new()
    {
        var sortedList = list.OrderBy(item => StringSimilarity(
            converter != null ? converter(item) : item.ToString(),
            unresolvableElement)).ToList();

        var selection = new SelectionPrompt<T>
        {
            Converter = (_) => converter != null ? converter(_) : _.ToString()
        };

        selection.AddChoices(sortedList);

        AnsiConsole.WriteLine($"Couldn't resolve {kind} ({unresolvableElement}) automatically. Please select it manually:");
        var selected = AnsiConsole.Prompt(selection);

        return selected;
    }

    public static async Task Sync(BsonDocument selectedrecipe)
    {
        var imgurl = selectedrecipe["imageuri"].AsString;
        var uri = new Uri(imgurl);
        var pictureFileName = uri.Segments[^1];

        var recipe = new Recipe
        {
            Name = selectedrecipe["name"].AsString,
            Description = selectedrecipe["content"].AsString,
            PictureFileName = pictureFileName,
            PictureUrl = imgurl,
            Source = selectedrecipe["url"]?.AsString
        };

        SetIngredients(selectedrecipe, recipe);

        await _client.AddRecipe(recipe);
    }

    private static void SetIngredients(BsonDocument selectedrecipe, Recipe recipe)
    {
        var ingredients = selectedrecipe["ingredientsTables"].AsArray;
        foreach (var table in ingredients)
        {
            foreach (var ingredient in table["elements"].AsArray)
            {
                var productName = ingredient["item"].AsString.Trim();
                var measure = ingredient["measure"]?.AsString?.Trim();

                if (productName.Contains("und"))
                {
                    string product1 = null, product2 = null;
                    InterpolatedParser.Parse(productName, $"{product1} und {product2}");

                    recipe.Ingredients.Add(ResolveIngredient(product1, measure));
                    recipe.Ingredients.Add(ResolveIngredient(product2, measure));
                    continue;
                }

                var ingr = ResolveIngredient(productName, measure);

                recipe.Ingredients.Add(ingr);
            }
        }
    }
}