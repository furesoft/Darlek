using Darlek.Core.RuntimeLibrary;
using Spectre.Console;
using System;

namespace Darlek.Library;

public static class ConsoleMethods
{
    [RuntimeMethod("display")]
    public static void Display(string msg)
    {
        Console.WriteLine(msg);
    }

    [RuntimeMethod("read-key")]
    public static ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    [RuntimeMethod("clear-console")]
    public static void Clear()
    {
        AnsiConsole.Clear();
    }

    [RuntimeMethod("ask")]
    public static string Ask(string msg)
    {
        return AnsiConsole.Ask<string>(msg);
    }
}