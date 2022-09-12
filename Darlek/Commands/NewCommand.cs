using Darlek.Core;
using Spectre.Console;

namespace Darlek.Commands;

public class NewCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var title = AnsiConsole.Prompt(new TextPrompt<string>("Title:"));
        var author = AnsiConsole.Prompt(new TextPrompt<string>("Author:"));

        Repository.Init(title, author);

        ManageMenu.Show();
    }
}