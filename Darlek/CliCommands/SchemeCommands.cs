using Darlek.Core.CLI;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.SchemeLibrary;
using Darlek.Core.Schemy;
using Darlek.Library;
using Spectre.Console;

namespace Darlek.Commands;

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