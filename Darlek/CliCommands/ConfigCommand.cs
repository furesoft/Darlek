using Darlek.Core;

namespace Darlek.CliCommands;

public class ConfigCommand
{
    public static void SetConfig(string[] args)
    {
        var key = args[1];
        var value = args[2];

        Config.Set(key, value);
    }
}