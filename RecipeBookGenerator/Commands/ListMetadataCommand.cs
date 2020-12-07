using BookGenerator.Core;
using BookGenerator.Core.CLI;

namespace BookGenerator.Commands
{
    public class ListMetadataCommand : ICliCommand
    {
        public string Name => "list-metadata";

        public string HelpText => "list-metadata";

        public string Description => "List all Metadata set for the current book";

        public int Invoke(CommandlineArguments args)
        {
            foreach (var md in Repository.GetAllMetadata())
            {
                System.Console.WriteLine(md.Key + ": " + md.Value.RawValue);
            }

            return 0;
        }
    }
}