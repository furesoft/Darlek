using BookGenerator.Commands;
using BookGenerator.Core.API;
using BookGenerator.Core.CLI;
using BookGenerator.Core.Crawler;
using BookGenerator.Core.Engine.BaseLibrary;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Extensions;
using BookGenerator.Core.SyntaxExtensions;
using BookGenerator.Properties;
using System;

namespace BookGenerator.Core
{
    public class JsEvaluator : IEvaluator<Context>
    {
        public Context Init()
        {
            //add syntax extensions
            Parser.DefineCustomCodeFragment(typeof(KeysOfOperator));
            Parser.DefineCustomCodeFragment(typeof(UsingStatement));

            //init js engine
            var ctx = new Context();
            ctx.DefineConstructor(typeof(Repository));

            return ctx;
        }

        public void AddCustomCommands(string source)
        {
            var ctx = new Context();
            ctx.Eval(Resources.Library);

            ctx.DefineVariable("registerCommand").Assign(JSValue.Marshal(new Action<Arguments>((args) =>
            {
                var invoker = args[3].As<Function>();

                var cmd = new JsCommand(args[0].As<string>(), args[1].As<string>(), args[2].As<string>(), invoker);
                App.Current.AddCommand(cmd);
            })));

            ctx.Eval(source);
        }

        public ICrawler GetCrawler(string source)
        {
            return new JSCrawler(source);
        }
    }
}