using System;
using System.IO;
using Darlek.Core;
using Darlek.Core.CLI;

namespace Darlek.Commands;

public class AddCommand : ICliCommand
{
    public string Name => "add";

    public string HelpText => "add --url <url>";

    public string Description => "Add New Element to Ebook Cache";

    public int Invoke(CommandlineArguments args)
    {
        var crawler = CrawlerFactory.GetCrawler(Repository.GetMetadata("crawler") ?? "chefkoch");
        //ToDo: Check url of validity
        string url = null;
        if (args.HasOption("url"))
        {
            url = args.GetValue<string>("url");
        }
        else
        {
            url = args.GetLastValue();
        }

        var r = crawler.Crawl(new Uri(url, UriKind.RelativeOrAbsolute)).Result;
        Repository.Add(r);

        if (r.ContainsKey("imageUri"))
        {
            Repository.AddFile(r["imageUri"], r["_id"].RawValue.ToString(), r["_id"].RawValue + Path.GetExtension(r["imageUri"]));
        }

        return 0;
    }
}