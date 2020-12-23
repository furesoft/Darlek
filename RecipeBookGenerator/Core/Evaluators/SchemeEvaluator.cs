using BookGenerator.Commands;
using BookGenerator.Core.CLI;
using BookGenerator.Core.Crawler;
using BookGenerator.Core.ImportProviders;
using BookGenerator.Core.RuntimeLibrary;
using BookGenerator.Core.SchemeLibrary;
using BookGenerator.Library;
using Schemy;
using System.IO;

namespace BookGenerator.Core
{
    public class SchemeEvaluator : IEvaluator<Interpreter>
    {
        public Interpreter Init()
        {
            var interpreter = new Interpreter();

            SchemeCliLoader.Apply(typeof(StringMethods).Assembly, interpreter);
            SchemeCliLoader.Apply(typeof(RepositoryMethods).Assembly, interpreter);

            return interpreter;
        }

        public void AddCustomCommands(string source)
        {
            var ctx = Init();

            ctx.DefineGlobal(Symbol.FromString("register-command"), new NativeProcedure((args) =>
            {
                var invoker = (Procedure)args[3];

                var cmd = new SchemeCommand(args[0].ToString(), args[1].ToString(), args[2].ToString(), invoker);
                App.Current.AddCommand(cmd);

                return None.Instance;
            }));

            ctx.DefineGlobal(Symbol.FromString("register-importer"), new NativeProcedure((args) =>
            {
                var invoker = (Procedure)args[1];

                var importer = new SchemeImporter((Symbol)args[0], invoker);
                ImportProvider.Register(importer);

                return None.Instance;
            }));

            var result = ctx.Evaluate(new StringReader(source));
        }

        public ICrawler GetCrawler(string source)
        {
            return new SchemeCrawler(source);
        }
    }
}