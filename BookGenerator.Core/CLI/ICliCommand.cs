﻿namespace BookGenerator.Core.CLI
{
    public interface ICliCommand
    {
        string Name { get; }
        string HelpText { get; }
        string Description { get; }

        int Invoke(CommandlineArguments args);
    }
}