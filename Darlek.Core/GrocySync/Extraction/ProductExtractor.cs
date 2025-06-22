using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Darlek.Core.GrocySync.Extraction
{
    public class ProductExtractor
    {
        public List<string> ExtractProducts(string productString)
        {
            var products = new List<string>();
            if (string.IsNullOrWhiteSpace(productString))
                return products;

            // Beispiel: "Tomaten und Gurken"
            var parts = productString.Split(new[] { " und " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                var trimmed = part.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                    products.Add(trimmed);
            }
            return products;
        }
    }
}

