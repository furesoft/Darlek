using Darlek.Core;
using Darlek.Core.CLI;

namespace Darlek.Commands;

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