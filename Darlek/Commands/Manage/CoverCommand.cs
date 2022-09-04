using Darlek.Core;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class CoverCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var file = AnsiConsole.Prompt(new TextPrompt<string>("File: "));
        Repository.AddFile(file, "cover", "cover.jpg");

        parentMenu.Show();
    }
}