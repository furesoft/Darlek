using BookGenerator.Core;
using BookGenerator.Core.CLI;

namespace BookGenerator.Commands
{
    public class SetCommand : ICliCommand
    {
        public string Name => "set";

        public string HelpText => "set --title <title> --author <author>";

        public string Description => "Command for changing meta data";

        public int Invoke(CommandlineArguments args)
        {
            if (args.HasOption("title"))
            {
                Repository.SetMetadata("title", args.GetValue<string>("title"));
            }
            else if (args.HasOption("author"))
            {
                Repository.SetMetadata("author", args.GetValue<string>("author"));
            }
            else if (args.HasOption("filename"))
            {
                Repository.SetMetadata("filename", args.GetValue<string>("filename"));
            }

            return 0;
        }
    }
}