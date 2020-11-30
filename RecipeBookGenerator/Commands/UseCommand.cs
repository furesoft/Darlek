using System.IO;
using BookGenerator.Core;
using BookGenerator.Core.CLI;
using BookGenerator.Properties;

namespace BookGenerator.Commands
{
    public class UseCommand : ICliCommand
    {
        public string Name => "use";

        public string HelpText => "use <crawlername>";

        public string Description => "Set The Crawler";

        public int Invoke(CommandlineArguments args)
        {
            if (args.HasOption("crawler"))
            {
                if (Crawlerfactory.IsDefault(args.GetValue<string>("crawler")))
                {
                    Repository.SetMetadata("crawler", args.GetValue<string>("crawler"));
                }
                else if (args.GetValue<string>("crawler") == "sample")
                {
                    Repository.SetMetadata("crawler", args.GetValue<string>("crawler"));
                    Repository.AddContentFile(Resources.SampleCrawler, args.GetValue<string>("crawler") + ".js");
                }
                else
                {
                    //if not default use js file
                    Repository.SetMetadata("crawler", args.GetValue<string>("crawler"));
                    Repository.AddFile(File.ReadAllText(args.GetValue<string>("crawler")), args.GetValue<string>("crawler"), args.GetValue<string>("crawler"));
                }
            }
            else if (args.HasOption("template"))
            {
                //ToDo: implement loading template from file
            }

            return 0;
        }
    }
}