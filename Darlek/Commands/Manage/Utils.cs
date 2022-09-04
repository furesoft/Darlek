using Darlek.Core;
using LiteDB;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public static class Utils
{
    public static BsonDocument SelectRecipe(Menu parentMenu)
    {
        var selection = new SelectionPrompt<BsonDocument>();
        selection.Converter = (_) => _ is null ? ".." : _["Name"];

        selection.AddChoice(null);
        foreach (var item in Repository.GetAll<BsonDocument>())
        {
            selection.AddChoice(item);
        }
        var selected = AnsiConsole.Prompt(selection);

        if (selected == null)
        {
            parentMenu.Show();
        }

        return selected;
    }
}