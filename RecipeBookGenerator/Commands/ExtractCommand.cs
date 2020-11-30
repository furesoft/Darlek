using System;
using System.IO;
using BookGenerator.Core.CLI;
using BookGenerator.Properties;

namespace BookGenerator.Commands
{
    public class ExtractCommand : ICliCommand
    {
        public string Name => "extract";

        public string HelpText => "extract --<crawler|template>";

        public string Description => "Extract the default template or sample js crawler";

        public int Invoke(CommandlineArguments args)
        {
            var arg = args.GetKey(0);

            if (args.GetOption("c", "crawler"))
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "crawler.js"), Resources.SampleCrawler);
            }
            if (args.GetOption("t", "template"))
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Template.html"), Resources.Template);
            }
            if (args.GetOption("c", "commands"))
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "commands.js"), Resources.Commands);
            }

            return 0;
        }
    }
}