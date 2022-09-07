using Darlek.Core;
using LiteDB;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class ViewRecipeCommand : IMenuCommand
{
    private BsonDocument selectedRecipe;

    public ViewRecipeCommand(BsonDocument selectedRecipe)
    {
        this.selectedRecipe = selectedRecipe;
    }

    public void Invoke(Menu parentMenu)
    {
        var grid = new Grid();
        grid.AddColumn(new GridColumn() { Alignment = Justify.Left });
        grid.AddColumn(new GridColumn() { Alignment = Justify.Right, Padding = new(10, 0, 10, 0) });
        grid.AddColumn(new GridColumn() { Alignment = Justify.Right, Padding = new(10, 0, 10, 0) });

        grid.AddRow(new Text(selectedRecipe["name"]).Centered(), new Text("Author: " + selectedRecipe["author"].AsString).RightAligned(), new Text("Date: " + (selectedRecipe["addedDate"].IsNull ? "-" : selectedRecipe["addedDate"].AsDateTime.ToString())));
        grid.AddEmptyRow();

        AnsiConsole.Write(grid);
        AnsiConsole.Write(new Text(selectedRecipe["content"]));

        parentMenu.WaitAndShow();
    }
}