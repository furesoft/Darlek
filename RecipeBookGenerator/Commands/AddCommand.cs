using System;
using System.IO;
using BookGenerator.Core;
using BookGenerator.Core.CLI;

namespace BookGenerator.Commands
{
    public class AddCommand : ICliCommand
    {
        public string Name => "add";

        public string HelpText => "add --url <url>";

        public string Description => "Add New Element to Ebook Cache";

        public int Invoke(CommandlineArguments args)
        {
            var crawler = Crawlerfactory.GetCrawler(Repository.GetMetadata("crawler") ?? "chefkoch");
            //ToDo: Check url of validity

            var r = crawler.Crawl(new Uri(args.GetValue<string>("url"), UriKind.RelativeOrAbsolute)).Result;
            Repository.Add(r);

            if (r.ContainsKey("imageUri"))
            {
                Repository.AddFile(r["imageUri"], r["_id"].RawValue.ToString(), r["_id"].RawValue + Path.GetExtension(r["imageUri"]));
            }

            return 0;
        }
    }
}