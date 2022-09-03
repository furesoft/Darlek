using Darlek.Core;
using Darlek.Core.CLI;
using Darlek.Properties;
using System.IO;

namespace Darlek.Commands;

public class UseCommand : ICliCommand
{
    public string Name => "use";

    public string HelpText => "use <crawlername>";

    public string Description => "Set The Crawler";

    public int Invoke(CommandlineArguments args)
    {
        if (args.HasOption("crawler"))
        {
            if (CrawlerFactory.IsDefault(args.GetValue<string>("crawler")))
            {
                Repository.SetMetadata("crawler", args.GetValue<string>("crawler"));
            }
            else if (args.GetValue<string>("crawler") == "sample")
            {
                Repository.SetMetadata("crawler", args.GetValue<string>("crawler"));
                Repository.AddContentFile(Resources.SampleCrawler1, args.GetValue<string>("crawler") + ".ss");
            }
            else
            {
                //if not default use ss file
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