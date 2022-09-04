using Darlek.Core;
using LiteDB;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Darlek.Commands.Manage;

public static class Utils
{
    public static BsonDocument SelectRecipe(Menu parentMenu)
    {
        return Selection(parentMenu, Repository.GetAll<BsonDocument>(), _ => _["Name"]);
    }

    public static T Selection<T>(Menu parentMenu, IEnumerable<T> list, Func<T, string> converter = null)
    {
        var selection = new SelectionPrompt<T>();

        selection.Converter = (_) => _ is null ? ".." : converter != null ? converter(_) : _.ToString();

        selection.AddChoice(default);
        selection.AddChoices(list);
        var selected = AnsiConsole.Prompt(selection);

        if (selected == null)
        {
            parentMenu.Show();
        }

        return selected;
    }
}