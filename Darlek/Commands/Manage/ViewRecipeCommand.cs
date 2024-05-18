using Darlek.Core;
using LiteDB;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class ViewRecipeCommand(BsonDocument selectedRecipe) : IMenuCommand
{
    private BsonDocument selectedRecipe = selectedRecipe;

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

        grid.AddRow(
            new Text(selectedRecipe["name"]).Centered(), 
            new Text("Author: " + selectedRecipe["author"].AsString).RightJustified(), 
            new Text("Date: " + (selectedRecipe["addedDate"].IsNull ? "-" : selectedRecipe["addedDate"].AsDateTime.ToString())));
        grid.AddEmptyRow();

        AnsiConsole.Write(grid);

        var content = new Grid();
        content.AddColumns(new GridColumn().LeftAligned(), new GridColumn().RightAligned());

        var ingredientsPanel = new Grid().AddColumn();

        ingredientsPanel.AddRow(new Markup("[bold]" + selectedRecipe["portions"].AsString + "[/]"));

        foreach (var t in selectedRecipe["ingredientsTables"].AsArray)
        {
            var table = new Table();
            table.AddColumns("", "");

            if (t.AsDocument.ContainsKey("name"))
            {
                ingredientsPanel.AddRow(t["name"].AsString);
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

            table.AddEmptyRow();

            ingredientsPanel.AddRow(table.NoBorder());
        }

        content.AddRow(new Text(selectedRecipe["content"]), ingredientsPanel);

        AnsiConsole.Write(content);
    }
}