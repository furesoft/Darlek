using Darlek.Commands;
using Darlek.Core;
using Darlek.Core.UI;
using Darlek.Properties;

namespace Darlek;

public static class Program
{
    public static void Main()
    {
        App_BeforeRun();

        var mainMenu = new Menu(null);
        mainMenu.Items.Add("Open", new OpenCommand());
        mainMenu.Items.Add("Create", new CreateCommand());
        mainMenu.Items.Add("Scheme REPL", new SchemeReplCommand());

        new SchemeEvaluator().AddCustomCommands(Resources.Commands1, mainMenu);

        mainMenu.Items.Add("Publish", new PublishCommand());
        mainMenu.Items.Add("Exit", new ExitCommand());

        mainMenu.Show();
    }

    private static void App_BeforeRun()
    {
        //Repository.CollectCustomCommands();

        ImportProvider.Collect(typeof(IImportProvider).Assembly);
    }
}