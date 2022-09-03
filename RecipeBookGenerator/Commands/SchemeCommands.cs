using BookGenerator.Core.CLI;
using BookGenerator.Core.RuntimeLibrary;
using BookGenerator.Core.SchemeLibrary;
using BookGenerator.Library;
using Schemy;
using Spectre.Console;

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

            var table = new Table();
            table.AddColumn(new TableColumn("Function").LeftAligned());
            table.AddColumn(new TableColumn("Module").RightAligned());

            foreach (var f in interpreter.Environment.store)
            {
                table.AddRow(f.Key.AsString, "");
            }

            foreach (var m in SchemeCliLoader.Modules)
            {
                foreach (var f in m.Value.store)
                {
                    table.AddRow(f.Key.AsString, m.Key.AsString);
                }
            }

            AnsiConsole.Write(table);

            return 0;
        }
    }
}