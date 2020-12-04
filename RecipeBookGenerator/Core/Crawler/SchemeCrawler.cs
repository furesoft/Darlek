using System;
using System.IO;
using System.Threading.Tasks;
using BookGenerator.Core.API;
using BookGenerator.Properties;
using LiteDB;
using Schemy;

namespace BookGenerator.Core.Crawler
{
    public class SchemeCrawler : ICrawler
    {
        public SchemeCrawler(string source)
        {
            Source = source;
        }

        public string Source { get; }

        public async Task<BsonDocument> Crawl(string id)
        {
#if DEBUG
            return await Crawl(new Uri($"{System.Environment.CurrentDirectory}\\debug.html"));
#else

            return await Crawl(new Uri($"https://www.chefkoch.de/rezepte/drucken/{id}.html"));
#endif
        }

        public async Task<BsonDocument> Crawl(Uri url)
        {
            var eval = EvaluatorSelector.GetEvaluator<Interpreter>("command.ss");
            var ctx = eval.Init();

            ctx.DefineGlobal(Symbol.FromString("set-crawler"), NativeProcedure.Create<Procedure, Procedure>(_ =>
            {
                return _;
            }));

            var obj = ctx.Evaluate(new StringReader(Source));
            var crawler = obj.Result as Procedure;

            return (BsonDocument)crawler.Call(new System.Collections.Generic.List<object> { url.ToString() });
        }
    }
}