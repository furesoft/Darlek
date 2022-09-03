using Darlek.Core;
using Darlek.Core.UI;
using LiteDB;
using Spectre.Console;
using System;

namespace Darlek.Commands.Manage;

public class InfoCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var metaTable = new Grid().AddColumn().AddColumn().Centered();
        foreach (var md in Repository.GetAllMetadata())
        {
            metaTable.AddRow(md.Key, md.Value);
        }

        var metaPanel = new Panel(metaTable);
        metaPanel.Header("Metadata");

        AnsiConsole.Write(metaPanel);

        var statTable = new Table().AddColumn("Title").AddColumn("Author");

        foreach (var item in Repository.GetAll<BsonDocument>())
        {
            statTable.AddRow(item["Name"], item["Author"]);
        }
        AnsiConsole.Write(statTable);

        var key = Console.ReadKey();
        parentMenu.Show();
    }
}