using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Darlek.Core;

public class Menu
{
    public Menu Parent;

    public Menu(Menu parentMenu)
    {
        Parent = parentMenu;
    }

    public Dictionary<string, IMenuCommand> Items { get; set; } = new();

    public void WaitAndShow()
    {
        Console.ReadKey();
        Show();
    }

    public string Show()
    {
        Console.Clear();
        var header = new FigletText("Darlek - Recipe Crawler").Centered();

        var pnl = new Panel(header);
        pnl.BorderStyle(Style.Parse("Blue"));
        AnsiConsole.Write(pnl);

        var prompt = new SelectionPrompt<string>();

        if (Parent != null)
        {
            prompt.AddChoice("..");
        }

        foreach (var item in Items)
        {
            prompt.AddChoice(item.Key);
        }

        var selectedItem = AnsiConsole.Prompt(prompt);

        if (selectedItem == "..")
        {
            Parent.Show();
            return string.Empty;
        }

        Items[selectedItem]?.Invoke(this);

        return selectedItem;
    }
}