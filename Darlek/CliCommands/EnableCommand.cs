using Darlek.Core;
using Darlek.Core.CLI;

namespace Darlek.Commands;

public class EnableCommand : ICliCommand
{
    public string Name => "enable";

    public string HelpText => "enable --crawler <id>";

    public string Description => "Enable a feature";

    public int Invoke(CommandlineArguments args)
    {
        var key = args.GetKey(1);

        Repository.SetSetting(key, args.GetValue<string>(key));

        return 0;
    }
}