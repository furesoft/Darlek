using System;

namespace Darlek.Core.GrocySync.Extraction
{
    public class MeasureExtractor
    {
        public (double? quantity, string unit, string source) ExtractMeasure(string measureString)
        {
            if (string.IsNullOrWhiteSpace(measureString))
                return (null, null, null);

            // Einfache Extraktion: "2 EL" => (2, "EL")
            var parts = measureString.Trim().Split(' ', 2);
            if (parts.Length == 2 && double.TryParse(parts[0], out var quantity))
            {
                return (quantity, parts[1], measureString);
            }
            // Fallback: keine Zahl gefunden
            return (null, null, measureString);
        }
    }
}

