using Darlek.Core;

namespace Darlek.Commands.Manage;

public class ManageRecipeCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var selectedRecipe = Utils.SelectRecipe(parentMenu);

        var menu = new Menu(parentMenu);

        menu.Items.Add("Delete", null);
        menu.Items.Add("Edit", null);
        menu.Items.Add("Renew", new RenewCommand(selectedRecipe));

        menu.Show();

        parentMenu.WaitAndShow();
    }
}