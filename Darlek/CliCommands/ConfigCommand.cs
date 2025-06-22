using Darlek.Core;
using Spectre.Console.Cli;

namespace Darlek.CliCommands;

public class ConfigCommand : Command<ConfigCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "key")]
        public string Key { get; set; }

        [CommandArgument(1, "value")]
        public string Value { get; set; }
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        Config.Set(settings.Key, settings.Value);
        return 0;
    }
}