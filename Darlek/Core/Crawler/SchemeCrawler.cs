using Darlek.Core.Schemy;
using LiteDB;
using System;
using System.Threading.Tasks;

namespace Darlek.Core.Crawler;

public class SchemeCrawler(ICallable callback) : ICrawler
{
    private ICallable callback = callback;

    public async Task<BsonDocument> Crawl(Uri url)
    {
        return (BsonDocument)callback.Call([url.ToString()]);
    }
}