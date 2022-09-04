using Darlek.Core.Schemy;
using LiteDB;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Darlek.Core.Crawler;

public class SchemeCrawler : ICrawler
{
    public SchemeCrawler(string source)
    {
        Source = source;
    }

    public string Source { get; }

    public async Task<BsonDocument> Crawl(Uri url)
    {
        var eval = new SchemeEvaluator();
        var ctx = SchemeEvaluator.Init();

        ctx.DefineGlobal(Symbol.FromString("set-crawler"), NativeProcedure.Create<Procedure, Procedure>(_ => {
            return _;
        }));

        var obj = ctx.Evaluate(new StringReader(Source));
        var crawler = obj.Result as Procedure;

        return (BsonDocument)crawler.Call(new System.Collections.Generic.List<object> { url.ToString() });
    }
}