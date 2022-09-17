using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Darlek.Library;

[RuntimeType]
public static class ConsoleMethods
{
    [RuntimeMethod("display")]
    public static object Display(string msg)
    {
        AnsiConsole.MarkupLine(msg);

        return None.Instance;
    }

    [RuntimeMethod("read-key")]
    public static object ReadKey()
    {
        return Console.ReadKey();
    }

    [RuntimeMethod("clear-console")]
    public static object Clear()
    {
        AnsiConsole.Clear();

        return None.Instance;
    }

    [RuntimeMethod("prompt")]
    public static object Prompt(string msg)
    {
        return AnsiConsole.Ask<string>(msg);
    }

    [RuntimeMethod("confirm")]
    public static object Confirm(string msg)
    {
        return AnsiConsole.Confirm(msg);
    }

    [RuntimeMethod("display-selection")]
    public static object Selection(List<object> arg)
    {
        var s = new SelectionPrompt<object>();
        s.AddChoices(arg);

        return AnsiConsole.Prompt(s);
    }
}