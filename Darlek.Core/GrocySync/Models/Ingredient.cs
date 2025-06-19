using Darlek.Core.GrocySync.Dto;

namespace Darlek.Core.GrocySync.Models;

public class Ingredient
{
    public Product Product { get; set; }
    public Measure Measure { get; set; } = new();
    public bool IsResolved => Product is not null && Measure.IsResolved;
    public string ProductName { get; set; }
}