using System.Collections.Generic;
using System.Linq;

namespace Darlek.Core.GrocySync.Extraction
{
    /// <summary>
    /// Extrahiert mehrere Produkte aus einer Zeile, die durch "und" verbunden sind.
    /// </summary>
    public class AndExtractor
    {
        public List<string> Extract(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<string>();
            // Trenne an " und ", aber nur, wenn es nicht Teil eines Produktnamens ist
            return input.Split(new[] { " und " }, System.StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToList();
        }
    }
}

