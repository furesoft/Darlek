using System.Collections.Generic;

namespace Darlek.Core.GrocySync.Models;

internal class Recipe
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Source { get; set; }
    public string PictureFileName { get; set; }
    public string PictureUrl { get; set; }

    public List<Ingredient> Ingredients { get; set; } = [];
}