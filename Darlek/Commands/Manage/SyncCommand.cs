using Darlek.Core;
using Darlek.Core.GrocySync;
using Darlek.Core.GrocySync.Models;
using LiteDB;
using Spectre.Console;
using System;

namespace Darlek.Commands.Manage;

public class SyncCommand(BsonDocument selectedrecipe) : IMenuCommand
{
    private readonly GrocyClient _client = new();
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

        var products = _client.GetAllProducts();
        var quantityUnits = _client.GetAllQuantityUnits();

        var ingredients = selectedrecipe["ingredientsTables"].AsArray;
        foreach (var table in ingredients)
        {
            foreach (var ingredient in table["elements"].AsArray)
            {
                var productName = ingredient["item"].AsString.Trim();
                var measure = ingredient["measure"].AsString.Trim();

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
                    AnsiConsole.WriteLine($"[red]Cannot find {measure}[/red]");
                }

                recipe.Ingredients.Add(ingr);
            }
        }

        //_client.AddRecipe(recipe);
        parentMenu.WaitAndShow();
    }
}