using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Darlek.Core;

public class Menu
{
    private Menu parentMenu;

    public Menu(Menu parentMenu)
    {
        this.parentMenu = parentMenu;
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

        if (parentMenu != null)
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
            parentMenu.Show();
            return string.Empty;
        }

        Items[selectedItem]?.Invoke(this);

        return selectedItem;
    }
}