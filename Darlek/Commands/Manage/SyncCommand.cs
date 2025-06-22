using Darlek.Core;
using Darlek.Core.GrocySync;
using Darlek.Core.GrocySync.Dto;
using Darlek.Core.GrocySync.Models;
using InterpolatedParsing;
using LiteDB;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Darlek.Commands.Manage;

public class SyncCommand(BsonDocument selectedrecipe) : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        GrocySyncService.Sync(selectedrecipe);

        parentMenu.WaitAndShow();
    }
}