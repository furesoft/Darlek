using BookGenerator.Core;
using BookGenerator.Core.CLI;
using BookGenerator.Properties;

namespace BookGenerator
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            App.Current.BeforeRun += App_BeforeRun;

            return App.Current.Run();
        }

        private static void App_BeforeRun()
        {
            Repository.CollectCustomCommands();
            new SchemeEvaluator().AddCustomCommands(Resources.Commands1);
        }
    }
}