using Darlek.Core;

namespace Darlek.Commands.Manage;

public class ManageRecipeCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var selectedRecipe = Utils.SelectRecipe(parentMenu);

        var menu = new Menu(parentMenu);

        menu.Items.Add("View", new ViewRecipeCommand(selectedRecipe));
        menu.Items.Add("Delete", new DeleteRecipeCommand(selectedRecipe));
        menu.Items.Add("Edit", new EditCommand(selectedRecipe));
        menu.Items.Add("Renew", new RenewCommand(selectedRecipe));

        menu.Show();

        parentMenu.WaitAndShow();
    }
}