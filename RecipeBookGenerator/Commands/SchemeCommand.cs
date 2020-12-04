using BookGenerator.Core.CLI;
using Schemy;

namespace BookGenerator.Commands
{
    [DoNotTrack]
    public class SchemeCommand : ICliCommand
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _help;

        public Procedure Invoker;

        public SchemeCommand(string name, string description, string help, Procedure invoker)
        {
            _name = name;
            _description = description;
            _help = help;
            Invoker = invoker;
        }

        public string Name => _name;

        public string HelpText => _help;

        public string Description => _description;

        public int Invoke(CommandlineArguments args)
        {
            return (int)Invoker.Call(new System.Collections.Generic.List<object> { args });
        }
    }
}