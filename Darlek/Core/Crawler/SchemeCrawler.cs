using Darlek.Core.Schemy;
using LiteDB;
using System;
using System.Threading.Tasks;

namespace Darlek.Core.Crawler;

public class SchemeCrawler : ICrawler
{
    private ICallable callback;

    public SchemeCrawler(ICallable callback)
    {
        this.callback = callback;
    }

    public async Task<BsonDocument> Crawl(Uri url)
    {
        return (BsonDocument)callback.Call(new System.Collections.Generic.List<object> { url.ToString() });
    }
}