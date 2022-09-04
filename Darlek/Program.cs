using Darlek.Commands;
using Darlek.Core;
using Darlek.Properties;

namespace Darlek;

public static class Program
{
    public static void Main()
    {
        ImportProvider.Collect(typeof(IImportProvider).Assembly);

        var mainMenu = new Menu(null);
        mainMenu.Items.Add("Open", new OpenCommand());
        mainMenu.Items.Add("Create", new CreateCommand());
        mainMenu.Items.Add("Scheme REPL", new SchemeReplCommand());
        mainMenu.Items.Add("Scheme Commands", new SchemeCommands());
        mainMenu.Items.Add("Show Importers", new ShowImportersCommand());
        mainMenu.Items.Add("Show Crawlers", new ShowCrawlersCommand());

        ManageMenu.Init(mainMenu);

        new SchemeEvaluator().AddCustomCommands(Resources.Commands1, mainMenu);

        mainMenu.Items.Add("Exit", new ExitCommand());

        mainMenu.Show();
    }
}