using BookGenerator.Core.CLI;
using System;
using System.Collections;

namespace BookGenerator.Commands
{
    public class HelpCommand : ICliCommand
    {
        public string Name => "help";

        public string HelpText => "help";

        public string Description => "Get help of specific commands";

        public int Invoke(CommandlineArguments args)
        {
            var table = new ConsoleTable(Console.CursorTop, ConsoleTable.Align.Left, new string[] { "Command", "Description" });
            var rows = new ArrayList();

            if (args.HasOption("metadata"))
            {
                rows.Add(new string[] { "--filename", "Set the output filename" });
                rows.Add(new string[] { "--title", "Set the title of the book" });
                rows.Add(new string[] { "--author", "Set the author of the book" });
                rows.Add(new string[] { "--cover", "Set the Cover of the book" });
            }
            else if (args.HasOption("settings"))
            {
                rows.Add(new string[] { "enable", "Enable a Feature" });
                rows.Add(new string[] { "disable", "Disable a Feature" });

                rows.Add(new string[] { "", "" });

                rows.Add(new string[] { "--filename", "Set the output filename" });
                rows.Add(new string[] { "use", "Set which crawler to use" });
                rows.Add(new string[] { "--custom_template", "Set the template" });
            }
            else
            {
                App.Current.PrintAllCommands();
                return 0;
            }

            table.RePrint(rows);

            return 0;
        }
    }
}