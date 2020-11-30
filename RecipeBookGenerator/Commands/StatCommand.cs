using System;
using System.Collections;
using BookGenerator.Core;
using BookGenerator.Core.CLI;
using LiteDB;

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
            var table = new ConsoleTable(Console.CursorTop, ConsoleTable.Align.Left, new string[] { "Name", "Author", "ID" });
            var entries = new ArrayList();
            foreach (var item in all)
            {
                entries.Add(new string[] { item["Name"], item["Author"], item["_id"].AsObjectId.ToString() });
            }

            table.RePrint(entries);

            return 0;
        }
    }
}