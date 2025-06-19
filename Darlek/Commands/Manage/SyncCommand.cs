using Darlek.Core;
using Darlek.Core.GrocySync;
using Darlek.Core.GrocySync.Dto;
using Darlek.Core.GrocySync.Models;
using LiteDB;
using Spectre.Console;
using System;
using System.Collections.Generic;

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
        products = _client.GetAllProducts();
        quantityUnits = _client.GetAllQuantityUnits();
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
                var measure = ingredient["measure"].AsString.Trim();
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

        double quantity = 0;
        string unit = null;

        try
        {
            InterpolatedParsing.InterpolatedParser.Parse(measure, $"{quantity} {unit}");
            ingr.Measure.Quantity = quantity;
            ingr.Measure.QuantityUnit = quantityUnits.Find(qu => qu.Name == unit ||
                                                                 qu.Userfields["symbol"] == unit ||
                                                                 qu.NamePlural == unit
            );
        }
        catch
        {
            AnsiConsole.WriteLine($"Cannot parse {measure} in {productName}. Please check the measure.");
        }

        return ingr;
    }
}