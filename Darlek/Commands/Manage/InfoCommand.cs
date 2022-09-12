using Darlek.Core;
using LiteDB;
using Spectre.Console;

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

        var statTable = new Table().AddColumns("Title", "Author", "Date");

        foreach (var item in Repository.GetAll<BsonDocument>())
        {
            statTable.AddRow(item["Name"], item["Author"], item["addedDate"].IsNull ? "-" : item["addedDate"].AsDateTime.ToString());
        }
        AnsiConsole.Write(statTable);

        parentMenu.WaitAndShow();
    }
}