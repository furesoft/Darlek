using Darlek.Core;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class RemoveCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var menu = new Menu(parentMenu);
        menu.Items.Add("Remove By ID", new DelegateCommand(_ => {
            var id = AnsiConsole.Prompt(new TextPrompt<string>("ID:"));
            Repository.Remove(id);
        }));
        menu.Items.Add("Remove By Name", new DelegateCommand(_ => {
            var name = AnsiConsole.Prompt(new TextPrompt<string>("Name:"));
            Repository.RemoveByName(name);
        }));

        menu.Show();

        parentMenu.WaitAndShow();
    }
}