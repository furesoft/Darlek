using Darlek.Core.Crawler;
using System;
using System.Collections.Generic;

namespace Darlek.Core;

public static class CrawlerFactory
{
    public static Dictionary<string, ICrawler> Crawlers = new()
    {
        ["www.chefkoch.de"] = new ChefkochCrawler()
    };

    public static ICrawler GetCrawlerByHost(string url)
    {
        var uri = new Uri(url);

        return GetCrawlerByHost(uri);
    }

    public static ICrawler GetCrawlerByHost(Uri uri)
    {
        if (Crawlers.ContainsKey(uri.Host))
        {
            return Crawlers[uri.Host];
        }

        return new ChefkochCrawler();
    }
}