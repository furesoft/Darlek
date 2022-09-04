using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Spectre.Console;
using System;

namespace Darlek.Core.SchemeLibrary;

[RuntimeType("console")]
public static class ConsoleMethods
{
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

    [RuntimeMethod("ask")]
    public static object Ask(string msg)
    {
        return AnsiConsole.Ask<string>(msg);
    }
}