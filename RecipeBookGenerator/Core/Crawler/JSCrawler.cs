using System;
using System.Threading.Tasks;
using BookGenerator.Core.API;
using BookGenerator.Core.Engine.BaseLibrary;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Extensions;
using BookGenerator.Properties;
using LiteDB;

namespace BookGenerator.Core.Crawler
{
    public class JSCrawler : ICrawler
    {
        public JSCrawler(string source)
        {
            Source = source;
        }

        public string Source { get; }

        public async Task<BsonDocument> Crawl(string id)
        {
#if DEBUG
            return await Crawl(new Uri($"{Environment.CurrentDirectory}\\debug.html"));
#else

            return await Crawl(new Uri($"https://www.chefkoch.de/rezepte/drucken/{id}.html"));
#endif
        }

        public async Task<BsonDocument> Crawl(Uri url)
        {
            var eval = EvaluatorSelector.GetEvaluator<Context>("command.js");
            var ctx = eval.Init();
            var crawlFunc = GetCrawler(ctx, url.ToString(), Source);

            var obj = crawlFunc.Call(new Arguments()).As<JSObject>();

            return obj.ToBsonDocument();
        }

        private static Function GetCrawler(Context ctx, string url, string source)
        {
            ctx.Add("document", JSValue.Wrap(WebRequest.GetDocument(url)));
            ctx.DefineVariable("$").Assign(JSValue.Marshal(new Func<string, JSValue>(_ =>
            {
                var doc = WebRequest.GetDocument(url);

                return Extensions.ToJSValue(doc.QuerySelector(_));
            }
            )));

            ctx.Eval(Resources.Library);

            //invoke crawl
            var crawler = ctx.Eval(source);
            var crawlFunc = ctx.GetVariable("crawl").As<Function>();

            return crawlFunc;
        }
    }
}