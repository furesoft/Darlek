using BookGenerator.Core;
using BookGenerator.Core.CLI;

namespace BookGenerator.Commands
{
    public class CoverCommand : ICliCommand
    {
        public string Name => "cover";

        public string HelpText => "cover <coveruri>";

        public string Description => "Override default cover";

        public int Invoke(CommandlineArguments args)
        {
            Repository.AddFile(args.GetLastValue(), "cover", "cover.jpg");
            return 0;
        }
    }
}