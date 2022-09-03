using Darlek.Core;
using Darlek.Core.CLI;
using System;
using System.IO;

namespace Darlek.Commands;

public class ImportCommand : ICliCommand
{
    public string Name => "import";

    public string HelpText => "import <filename>";

    public string Description => "Import entry from file";

    public int Invoke(CommandlineArguments args)
    {
        string filename = null;
        if (args.HasOption("path"))
        {
            filename = args.GetValue<string>("path");
        }
        else
        {
            filename = args.GetLastValue();
        }

        var provider = ImportProvider.GetProvider(filename);

        var content = File.ReadAllBytes(filename);
        var document = provider.Import(content);

        Repository.Add(document);

        Console.WriteLine("Successfully imported");

        return 0;
    }
}