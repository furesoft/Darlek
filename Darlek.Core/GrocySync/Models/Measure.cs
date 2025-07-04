﻿using Darlek.Core.GrocySync.Dto;

namespace Darlek.Core.GrocySync.Models;

internal class Measure
{
    public double Quantity { get; set; }
    public bool IsResolved => Quantity > 0 && QuantityUnit != null;
    public string Source { get; set; }

    public QuantityUnit QuantityUnit { get; set; }

    public string VariableAmount { get; set; }

    public override string ToString() =>
        $"{Quantity} {QuantityUnit?.Name ?? "Unknown"} ({Source})";
}