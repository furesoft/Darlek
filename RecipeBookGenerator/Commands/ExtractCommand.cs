using System;
using System.IO;
using BookGenerator.Core.CLI;
using BookGenerator.Properties;

namespace BookGenerator.Commands
{
    public class ExtractCommand : ICliCommand
    {
        public string Name => "extract";

        public string HelpText => "extract --<crawler|template|commands> --language <js|ss>";

        public string Description => "Extract the default template or sample js crawler";

        public int Invoke(CommandlineArguments args)
        {
            var arg = args.GetKey(0);

            if (args.GetOption("c", "crawler") && args.GetValue<string>("language") == "js")
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "crawler.js"), Resources.SampleCrawler);
            }
            else if (args.GetOption("c", "crawler") && args.GetValue<string>("language") == "ss")
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "crawler.ss"), Resources.SampleCrawler1);
            }
            if (args.GetOption("t", "template"))
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Template.html"), Resources.Template);
            }
            if (args.GetOption("c", "commands") && args.GetValue<string>("language") == "js")
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "commands.js"), Resources.Commands);
            }
            else if (args.GetOption("c", "commands") && args.GetValue<string>("language") == "ss")
            {
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "commands.ss"), Resources.Commands1);
            }

            return 0;
        }
    }
}