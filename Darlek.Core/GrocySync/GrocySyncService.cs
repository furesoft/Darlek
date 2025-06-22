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

public class GrocySyncService
{
    private static readonly GrocyClient _client = new();

    private static readonly List<Product> products;
    private static readonly List<QuantityUnit> quantityUnits;

    static GrocySyncService()
    {
        products = _client.GetAllProducts().OrderBy(p => p.Name).ToList();
        quantityUnits = _client.GetAllQuantityUnits().OrderBy(qu => qu.Name).ToList();
    }

    private static Ingredient ResolveIngredient(string productName, string measure)
    {
        var product = products.Find(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        var ingr = new Ingredient();
        ingr.Product = product;
        ingr.ProductName = productName;
        ingr.Measure.Source = measure;

        if (ingr.Product == null)
        {
            ingr.Product = ManualResolveSelector("product", productName, products, p => p.Name);
        }

        double quantity = 0;
        string unit = null;

        try
        {
            InterpolatedParser.Parse(measure, $"{quantity} {unit}");
            ingr.Measure.Quantity = quantity;
            ingr.Measure.QuantityUnit = quantityUnits.Find(qu => qu.Name == unit ||
                                                                 qu.Userfields["symbol"] == unit ||
                                                                 qu.NamePlural == unit
            );

            if (!ingr.Measure.IsResolved)
            {
                ingr.Measure.VariableAmount = ingr.Measure.Source;
            }
        }
        catch
        {
            if (ingr.Measure.QuantityUnit is null)
            {
                ingr.Measure.QuantityUnit = ManualResolveSelector("quantity unit", measure, quantityUnits, qu => qu.Name);
            }
        }

        return ingr;
    }

    private static T ManualResolveSelector<T>(string kind, string unresolvableElement, IEnumerable<T> list, Func<T, string> converter = null)
        where T : new()
    {
        var selection = new SelectionPrompt<T>
        {
            Converter = (_) => converter != null ? converter(_) : _.ToString()
        };

        selection.AddChoices(list);

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