using BookGenerator.Core.CLI;
using BookGenerator.Core.Crawler;
using Schemy;
using System.IO;
using System.Linq;

namespace BookGenerator.Commands
{
    public class ScriptCommand : ICliCommand
    {
        public string Name => "script";

        public string HelpText => "script --file <filename>";

        public string Description => "Run a set of command by a scheme script";

        public int Invoke(CommandlineArguments args)
        {
            var eval = EvaluatorSelector.GetEvaluator<Interpreter>(".ss");

            var interpreter = eval.Init();

            interpreter.DefineGlobal(Symbol.FromString("run-command"), new NativeProcedure(_ =>
            {
                App.Current.EvaluateLine((string)_.First(), true);

                return None.Instance;
            }));

            var content = File.ReadAllText(args.GetValue<string>("file"));

            interpreter.Evaluate(new StringReader(content));

            return 0;
        }
    }
}