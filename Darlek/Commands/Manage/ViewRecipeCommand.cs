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
        DisplayRecipe();

        parentMenu.WaitAndShow();
    }

    public void DisplayRecipe()
    {
        var grid = new Grid();
        grid.AddColumn(new GridColumn() { Alignment = Justify.Left });
        grid.AddColumn(new GridColumn() { Alignment = Justify.Right, Padding = new(10, 0, 10, 0) });
        grid.AddColumn(new GridColumn() { Alignment = Justify.Right, Padding = new(10, 0, 10, 0) });

        grid.AddRow(new Text(selectedRecipe["name"]).Centered(), new Text("Author: " + selectedRecipe["author"].AsString).RightAligned(), new Text("Date: " + (selectedRecipe["addedDate"].IsNull ? "-" : selectedRecipe["addedDate"].AsDateTime.ToString())));
        grid.AddEmptyRow();

        AnsiConsole.Write(grid);

        AnsiConsole.WriteLine(selectedRecipe["portions"].AsString);

        foreach (var t in selectedRecipe["ingredientsTables"].AsArray)
        {
            var table = new Table();
            table.AddColumns("Measure", "Ingredient");

            if (t.AsDocument.ContainsKey("name"))
            {
                AnsiConsole.Write(t["name"].AsString);
            }

            foreach (var row in t["elements"].AsArray)
            {
                if (!row["measure"].IsNull)
                {
                    table.AddRow(row["measure"], row["item"]);
                }
                else
                {
                    table.AddRow("", row["item"]);
                }
            }

            AnsiConsole.Write(table.MinimalBorder());
        }

        AnsiConsole.Write(new Text(selectedRecipe["content"]));
    }
}