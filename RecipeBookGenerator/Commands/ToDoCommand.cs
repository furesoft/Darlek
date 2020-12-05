using BookGenerator.Core.CLI;
using System;

namespace BookGenerator.Commands
{
    public class ToDoCommand : ICliCommand
    {
        public string Name => "todo";

        public string HelpText => "todo";

        public string Description => "Display a manual";

        public int Invoke(CommandlineArguments args)
        {
            Console.WriteLine("1. Add some metadata with: darlek set --title <title> --author <author>");
            Console.WriteLine("2. Add some recipes with: darlek add <url>");
            Console.WriteLine("3. Generate Epub with: darlek publish");

            return 0;
        }
    }
}