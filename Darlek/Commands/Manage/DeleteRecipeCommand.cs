using Darlek.Core;
using LiteDB;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class DeleteRecipeCommand(BsonDocument selectedRecipe) : IMenuCommand
{
    private BsonDocument selectedRecipe = selectedRecipe;

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