using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Spectre.Console;
using System;

namespace Darlek.Library;

public static class ConsoleMethods
{
    [RuntimeMethod("display")]
    public static object Display(string msg)
    {
        Console.WriteLine(msg);

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

    [RuntimeMethod("ask")]
    public static object Ask(string msg)
    {
        return AnsiConsole.Ask<string>(msg);
    }
}