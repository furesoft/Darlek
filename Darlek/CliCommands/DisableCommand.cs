using Darlek.Core;
using Darlek.Core.CLI;

namespace Darlek.Commands;

public class DisableCommand : ICliCommand
{
    public string Name => "disable";

    public string HelpText => "disable --crawler";

    public string Description => "Disable a feature";

    public int Invoke(CommandlineArguments args)
    {
        var key = args.GetKey(1);

        Repository.RemoveSetting(key);

        return 0;
    }
}