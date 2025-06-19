using Darlek.Core;
using Darlek.Core.GrocySync;
using Darlek.Core.GrocySync.Dto;
using Darlek.Core.GrocySync.Models;
using InterpolatedParsing;
using LiteDB;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Darlek.Commands.Manage;

public class SyncCommand : IMenuCommand
{
    private readonly GrocyClient _client = new();

    private readonly BsonDocument selectedrecipe;
    private readonly List<Product> products;
    private readonly List<QuantityUnit> quantityUnits;

    public SyncCommand(BsonDocument selectedrecipe)
    {
        this.selectedrecipe = selectedrecipe;
        products = _client.GetAllProducts().OrderBy(p => p.name).ToList();
        quantityUnits = _client.GetAllQuantityUnits().OrderBy(qu => qu.Name).ToList();
    }

    public void Invoke(Menu parentMenu)
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

        var ingredients = selectedrecipe["ingredientsTables"].AsArray;
        foreach (var table in ingredients)
        {
            foreach (var ingredient in table["elements"].AsArray)
            {
                var productName = ingredient["item"].AsString.Trim();
                var measure = ingredient["measure"]?.AsString?.Trim();

                if (productName.Contains("und"))
                {
                    string? product1 = null, product2 = null;
                    InterpolatedParser.Parse(productName, $"{product1} und {product2}");

                    recipe.Ingredients.Add(ResolveIngredient(product1, measure));
                    recipe.Ingredients.Add(ResolveIngredient(product2, measure));
                    continue;
                }

                var ingr = ResolveIngredient(productName, measure);

                recipe.Ingredients.Add(ingr);
            }
        }

        //_client.AddRecipe(recipe);
        parentMenu.WaitAndShow();
    }

    private Ingredient ResolveIngredient(string productName, string measure)
    {
        var product = products.Find(p => p.name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        var ingr = new Ingredient();
        ingr.Product = product;
        ingr.ProductName = productName;
        ingr.Measure.Source = measure;

        if (ingr.Product == null)
        {
           ingr.Product = ManualResolveSelector("product", productName, products, p => p.name);
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

    public static T ManualResolveSelector<T>(string kind, string unresolvableElement, IEnumerable<T> list, Func<T, string> converter = null)
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
}