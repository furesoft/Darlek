using System;
using Darlek.Core;
using Darlek.Core.CLI;

namespace Darlek.Commands;

public class RemoveCommand : ICliCommand
{
    public string Name => "remove";

    public string HelpText => "remove --id <id>";

    public string Description => "Remove Element From Ebook Cache";

    public int Invoke(CommandlineArguments args)
    {
        if (args.HasOption("id"))
        {
            Repository.Remove(args.GetValue<string>("id"));
        }
        if (args.HasOption("name"))
        {
            Repository.RemoveByName(args.GetValue<string>("name"));
        }

        return 0;
    }
}