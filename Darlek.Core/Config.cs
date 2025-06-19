using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Darlek.Core;

public static class Config
{
    private static readonly Dictionary<string, string> _settings = new();

    public static void Load()
    {
        if (!File.Exists("darlek.conf"))
        {
            return;
        }

        var lines = System.IO.File.ReadAllLines("darlek.conf");
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            var parts = line.Split('=');
            if (parts.Length == 2)
            {
                var key = parts[0].Trim();
                var value = parts[1].Trim();
                _settings[key] = value;
            }
        }
    }

    public static T Get<T>(string key)
        where T : IParsable<T>
    {
        return T.Parse(_settings[key], CultureInfo.InvariantCulture);
    }

    public static string Get(string key)
    {
        return _settings.TryGetValue(key, out var value) ? value : string.Empty;
    }

    public static void Set(string key, string value)
    {
        _settings[key] = value;
        Save();
    }

    private static void Save()
    {
        using var writer = new StreamWriter("darlek.conf");
        foreach (var kvp in _settings)
        {
            writer.WriteLine($"{kvp.Key}={kvp.Value}");
        }
    }
}