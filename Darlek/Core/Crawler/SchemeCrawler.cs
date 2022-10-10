using Darlek.Scheme;
using LiteDB;
using System;
using System.Threading.Tasks;

namespace Darlek.Core.Crawler;

public class SchemeCrawler : ICrawler
{
    private readonly ICallable _callback;

    public SchemeCrawler(ICallable callback)
    {
        _callback = callback;
    }

    public Task<BsonDocument> Crawl(Uri url)
    {
        return Task.FromResult((BsonDocument)_callback.Call(new System.Collections.Generic.List<object> { url.ToString() }));
    }
}