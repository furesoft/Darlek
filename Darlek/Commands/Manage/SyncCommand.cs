using Darlek.Core;
using Darlek.Core.GrocySync;
using Darlek.Core.GrocySync.Models;
using LiteDB;

namespace Darlek.Commands.Manage;

public class SyncCommand(BsonDocument selectedrecipe) : IMenuCommand
{
    GrocyClient client = new GrocyClient();
    public void Invoke(Menu parentMenu)
    {
        client.AddRecipe(selectedrecipe);
        parentMenu.WaitAndShow();
    }
}