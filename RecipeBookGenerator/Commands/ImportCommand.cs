using BookGenerator.Core;
using BookGenerator.Core.CLI;
using Schemy;
using System;
using System.IO;

namespace BookGenerator.Commands
{
    public class ImportCommand : ICliCommand
    {
        public string Name => "import";

        public string HelpText => "import <filename>";

        public string Description => "Import entry from file";

        public int Invoke(CommandlineArguments args)
        {
            ImportProvider.Collect(typeof(IImportProvider).Assembly);

            var filename = args.GetLastValue();
            var provider = ImportProvider.GetProvider(filename);

            var content = File.ReadAllBytes(filename);
            var document = provider.Import(content);

            Repository.Add(document);

            Console.WriteLine("Successfully imported");

            return 0;
        }
    }
}