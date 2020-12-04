using BookGenerator.Core.CLI;
using BookGenerator.Core.RuntimeLibrary;
using BookGenerator.Core.SchemeLibrary;
using BookGenerator.Library;
using Schemy;
using System;
using System.Collections;

namespace BookGenerator.Commands
{
    public class SchemeCommands : ICliCommand
    {
        public string Name => "scheme-commands";

        public string HelpText => "scheme-commands";

        public string Description => "List all possible commands";

        public int Invoke(CommandlineArguments args)
        {
            var interpreter = new Interpreter();

            SchemeCliLoader.Apply(typeof(StringMethods).Assembly, interpreter);
            SchemeCliLoader.Apply(typeof(RepositoryMethods).Assembly, interpreter);

            var table = new ConsoleTable(Console.CursorTop, ConsoleTable.Align.Left, new string[] { "Function", "Module" });
            var rows = new ArrayList();
            foreach (var f in interpreter.Environment.store)
            {
                rows.Add(new string[] { f.Key.AsString, "" });
            }
            foreach (var m in SchemeCliLoader.Modules)
            {
                foreach (var f in m.Value.store)
                {
                    rows.Add(new string[] { f.Key.AsString, m.Key.AsString });
                }
            }

            table.RePrint(rows);

            return 0;
        }
    }
}