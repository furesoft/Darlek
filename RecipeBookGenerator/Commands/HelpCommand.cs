using BookGenerator.Core.CLI;
using Spectre.Console;

namespace BookGenerator.Commands
{
    public class HelpCommand : ICliCommand
    {
        public string Name => "help";

        public string HelpText => "help";

        public string Description => "Get help of specific commands";

        public int Invoke(CommandlineArguments args)
        {
            var table = new Table();
            table.AddColumn(new TableColumn("Command").LeftAligned());
            table.AddColumn(new TableColumn("Description").RightAligned());

            if (args.HasOption("metadata"))
            {
                table.AddRow(new string[] { "--filename", "Set the output filename" });
                table.AddRow(new string[] { "--title", "Set the title of the book" });
                table.AddRow(new string[] { "--author", "Set the author of the book" });
                table.AddRow(new string[] { "--cover", "Set the Cover of the book" });
            }
            else if (args.HasOption("settings"))
            {
                table.AddRow(new string[] { "enable", "Enable a Feature" });
                table.AddRow(new string[] { "disable", "Disable a Feature" });

                table.AddRow(new string[] { "", "" });

                table.AddRow(new string[] { "--filename", "Set the output filename" });
                table.AddRow(new string[] { "use", "Set which crawler to use" });
                table.AddRow(new string[] { "--custom_template", "Set the template" });
            }
            else
            {
                App.Current.PrintAllCommands();
                return 0;
            }

            AnsiConsole.Write(table);

            return 0;
        }
    }
}