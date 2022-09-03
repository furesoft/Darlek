using Darlek.Core;
using Darlek.Core.CLI;
using System;
using System.Linq;

namespace Darlek.Commands;

public class ShowImportersCommand : ICliCommand
{
    public string Name => "importer-list";

    public string HelpText => "importer-list";

    public string Description => "Get a list of all registered importers";

    public int Invoke(CommandlineArguments args)
    {
        foreach (var ip in ImportProvider.GetAllProviders())
        {
            Console.WriteLine(ip.AsString);
        }

        return 0;
    }
}