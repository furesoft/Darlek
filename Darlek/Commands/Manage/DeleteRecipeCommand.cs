using Darlek.Core;
using LiteDB;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class DeleteRecipeCommand : IMenuCommand
{
    private BsonDocument selectedRecipe;

    public DeleteRecipeCommand(BsonDocument selectedRecipe)
    {
        this.selectedRecipe = selectedRecipe;
    }

    public void Invoke(Menu parentMenu)
    {
        var confirmation = AnsiConsole.Confirm($"Do you really want to delete '{selectedRecipe["name"]}'?", true);

        if (confirmation)
        {
            Repository.Remove(selectedRecipe);
        }

        parentMenu.Parent.Show();
    }
}