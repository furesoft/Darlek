using BookGenerator.Core.CLI;
using BookGenerator.Core.Engine.BaseLibrary;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Extensions;

namespace BookGenerator.Commands
{
    [DoNotTrack]
    public class JsCommand : ICliCommand
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _help;

        public Function Invoker;

        public JsCommand(string name, string description, string help, Function invoker)
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
            return Invoker.Call(new Arguments { JSValue.Marshal(args) }).As<int>();
        }
    }
}