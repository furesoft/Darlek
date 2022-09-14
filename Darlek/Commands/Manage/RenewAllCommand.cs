using Darlek.Core;
using LiteDB;
using Spectre.Console;

namespace Darlek.Commands.Manage;

public class RenewAllCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        foreach (var recipe in Repository.GetAll<BsonDocument>())
        {
            AnsiConsole.Status().Start($"Recrawl {recipe["name"]}", _ => {
                new RenewCommand(recipe).Invoke(parentMenu);
            });
        }

        parentMenu.Show();
    }
}