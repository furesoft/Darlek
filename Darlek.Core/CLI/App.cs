using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Darlek.Core.CLI;

/// <summary>
/// A Class to build CommandLine Applications easily
/// </summary>
public class App
{
    public static App Current = new App();
    private Dictionary<string, ICliCommand> _commands = new Dictionary<string, ICliCommand>();

    public event Action BeforeRun;

    public void AddCommand(ICliCommand cmd)
    {
        _commands.Add(cmd.Name, cmd);
    }

    /// <summary>
    /// Start The Application
    /// </summary>
    /// <returns>The Return Code</returns>
    public int Run()
    {
        //collect all command processors
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(_ => _.GetTypes());

        BeforeRun?.Invoke();

        foreach (var t in types)
        {
            if (t.GetCustomAttribute<DoNotTrackAttribute>() != null)
            {
                continue;
            }

            if (t.IsInterface || t.IsAbstract)
            {
                continue;
            }
            else if (typeof(ICliCommand).IsAssignableFrom(t))
            {
                var instance = (ICliCommand)Activator.CreateInstance(t, Array.Empty<Type>());
                _commands.Add(instance.Name, instance);
            }
        }

        var args = Environment.GetCommandLineArgs();

        if (args.Length == 1)
        {
            PrintAllCommands();
            return -1;
        }

        if (args.Length == 2 && (args[1] == "--interactive" || args[1] == "-i"))
        {
            while (true)
            {
                Console.Write(">> ");
                var input = Console.ReadLine();
                ProcessCommand(input.Split(' ', StringSplitOptions.RemoveEmptyEntries), true);
            }
        }
        else
        {
            return ProcessCommand(args);
        }
    }

    public int EvaluateLine(string cmd, bool isInteractive = false)
    {
        return ProcessCommand(cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries), isInteractive);
    }

    public void PrintAllCommands()
    {
        var table = new Table();
        table.AddColumn(new TableColumn("Command").LeftAligned());
        table.AddColumn(new TableColumn("Description").RightAligned());
        table.AddColumn(new TableColumn("Example").LeftAligned());

        foreach (var cmd in _commands)
        {
            table.AddRow(cmd.Key, cmd.Value.Description, cmd.Value.HelpText);
        }

        AnsiConsole.Write(table);
    }

    private int ProcessCommand(string[] args, bool isInteractive = false)
    {
        if (args.Length == 0)
        {
            PrintAllCommands();
            return 0;
        }

        var name = "";
        if (isInteractive)
        {
            name = args[0];
        }
        else
        {
            name = args[1];
        }

        //find correct processor and invoke it with new argumentvector
        if (_commands.ContainsKey(name))
        {
            return _commands[name].Invoke(new CommandlineArguments(args));
        }
        else if (name == "help")
        {
            PrintAllCommands();
        }
        else
        {
            // Print list of commands with helptext
            PrintAllCommands();
        }

        return -1;
    }
}