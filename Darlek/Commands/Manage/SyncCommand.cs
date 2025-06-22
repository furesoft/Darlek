using Darlek.Core;
using Darlek.Core.GrocySync;
using LiteDB;
using Spectre.Console;
using System;

namespace Darlek.Commands.Manage;

public class SyncCommand(BsonDocument selectedrecipe) : IMenuCommand
{
    public async void Invoke(Menu parentMenu)
    {
        try
        {
            await GrocySyncService.Sync(selectedrecipe);

            parentMenu.WaitAndShow();
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }
}