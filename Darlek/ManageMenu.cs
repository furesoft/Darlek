using Darlek.Commands;
using Darlek.Commands.Manage;
using Darlek.Core;

namespace Darlek;

public static class ManageMenu
{
    public static Menu Menu;

    public static void Init(Menu mainMenu)
    {
        Menu = new Menu(mainMenu);

        Menu.Items.Add("Recipes", new DelegateCommand(_ => {
            var m = new Menu(_);
            m.Items.Add("Add Recipe", new AddRecipeCommand());
            m.Items.Add("Manage Recipe", new ManageRecipeCommand());

            m.Show();
        }));

        Menu.Items.Add("Set Cover", new CoverCommand());
        Menu.Items.Add("Publish", new PublishCommand());
        Menu.Items.Add("Select Crawler", new SelectCrawlerCommand());
        Menu.Items.Add("Info", new InfoCommand());
    }

    public static void Show()
    {
        Menu.Show();
    }
}