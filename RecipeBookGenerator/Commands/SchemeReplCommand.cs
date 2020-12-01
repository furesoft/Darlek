using BookGenerator.Core.CLI;
using BookGenerator.Core.RuntimeLibrary;
using BookGenerator.Core.SchemeLibrary;
using BookGenerator.Library;
using Schemy;
using System;
using System.IO;

namespace BookGenerator.Commands
{
    internal class SchemeReplCommand : ICliCommand
    {
        public string Name => "scheme";

        public string HelpText => "scheme";

        public string Description => "Start a scheme repl";

        public int Invoke(CommandlineArguments args)
        {
            var interpreter = new Interpreter();

            SchemeCliLoader.Apply(typeof(StringMethods).Assembly, interpreter);
            SchemeCliLoader.Apply(typeof(RepositoryMethods).Assembly, interpreter);

            while (true)
            {
                var input = Console.ReadLine();
                var res = interpreter.Evaluate(new StringReader(input));

                Console.WriteLine(res.Result);
            }

            return 0;
        }
    }
}