using Darlek.Core.GrocySync.Dto;

namespace Darlek.Core.GrocySync.Models;

internal class Ingredient
{
    public Product Product { get; set; }
    public Measure Measure { get; set; } = new();
    public bool IsResolved => Product is not null && Measure.IsResolved;
    public string ProductName { get; set; }
    public override string ToString() =>
        $"({Measure.Quantity} {Measure.QuantityUnit?.Name ?? "Unknown"}) {ProductName}";
}