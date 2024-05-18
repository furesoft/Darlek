using Darlek.Core;
using Spectre.Console;
using System;
using System.Threading;

namespace Darlek.Commands.Manage;

public class AddRecipeCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var url = AnsiConsole.Prompt(new TextPrompt<string>("URL:"));

        AnsiConsole.Status().AutoRefresh(true).Start("Crawling", (_) => {
            Repository.Crawl(url);
        });

        AnsiConsole.WriteLine("Success");
        Thread.Sleep(1000);

        parentMenu.Show();
    }
}