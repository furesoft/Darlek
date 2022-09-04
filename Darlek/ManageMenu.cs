using Darlek.Commands.Manage;
using Darlek.Core;

namespace Darlek;

public static class ManageMenu
{
    public static Menu Menu;

    public static void Init(Menu mainMenu)
    {
        Menu = new Menu(mainMenu);

        Menu.Items.Add("Set Cover", new CoverCommand());
        Menu.Items.Add("Add", new AddCommand());
        Menu.Items.Add("Remove", new RemoveCommand());
        Menu.Items.Add("Publish", new PublishCommand());
        Menu.Items.Add("Clear", new ClearCommand());
        Menu.Items.Add("Info", new InfoCommand());
    }

    public static void Show()
    {
        Menu.Show();
    }
}