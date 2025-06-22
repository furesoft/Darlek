using Darlek.CliCommands;
using Darlek.Commands;
using Darlek.Core;
using Darlek.Properties;
using Spectre.Console.Cli;

namespace Darlek;

public static class Program
{
    public static int Main(string[] args)
    {
        if (args.Length > 1)
        {
            var app = new CommandApp<ConfigCommand>();
            return app.Run(args);
        }

        ImportProvider.Collect(typeof(IImportProvider).Assembly);
        Config.Load();

        var mainMenu = new Menu(null);
        mainMenu.Items.Add("New", new NewCommand());
        mainMenu.Items.Add("Open", new OpenCommand());
        mainMenu.Items.Add("Dev Tools", new DelegateCommand(_ => {
            var m = new Menu(_);

            m.Items.Add("Scheme REPL", new SchemeReplCommand());
            m.Items.Add("Scheme Commands", new SchemeCommands());
            m.Show();
        }));

        ManageMenu.Init(mainMenu);

        new SchemeEvaluator().AddCustomCommands(Resources.Commands1, mainMenu);

        mainMenu.Items.Add("Exit", new ExitCommand());

        mainMenu.Show();
        return 0;
    }
}