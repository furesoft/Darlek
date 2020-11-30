using BookGenerator.Core;
using BookGenerator.Core.CLI;

namespace BookGenerator.Commands
{
    public class ClearCommand : ICliCommand
    {
        public string Name => "clear";

        public string HelpText => "clear";

        public string Description => "Delete all Entries";

        public int Invoke(CommandlineArguments args)
        {
            Repository.Clear();
            return 0;
        }
    }
}