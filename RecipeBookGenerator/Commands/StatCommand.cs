using BookGenerator.Core;
using BookGenerator.Core.CLI;
using LiteDB;
using Spectre.Console;

namespace BookGenerator.Commands
{
    public class StatCommand : ICliCommand
    {
        public string Name => "stat";

        public string HelpText => "stat";

        public string Description => "Display all Items in Cache that will be added to EBook";

        public int Invoke(CommandlineArguments args)
        {
            var all = Repository.GetAll<BsonDocument>();

            var table = new Table();
            table.AddColumn(new TableColumn("Name").LeftAligned());
            table.AddColumn(new TableColumn("Author").Centered());
            table.AddColumn(new TableColumn("ID").RightAligned());

            foreach (var item in all)
            {
                table.AddRow(new string[] { item["Name"], item["Author"], item["_id"].AsObjectId.ToString() });
            }

            AnsiConsole.Write(table);

            return 0;
        }
    }
}