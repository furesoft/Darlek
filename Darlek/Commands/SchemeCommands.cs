using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Spectre.Console;
using System;

namespace Darlek.Commands;

public class SchemeCommands : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var interpreter = new Interpreter();

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

        Console.ReadKey();

        parentMenu.Show();
    }
}