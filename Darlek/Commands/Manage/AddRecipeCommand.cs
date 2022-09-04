using Darlek.Core;
using Spectre.Console;
using System;
using System.IO;
using System.Threading;

namespace Darlek.Commands.Manage;

public class AddRecipeCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var crawler = CrawlerFactory.GetCrawler(Repository.GetMetadata("crawler") ?? "chefkoch");

        var url = AnsiConsole.Prompt(new TextPrompt<string>("URL:"));

        AnsiConsole.Status().AutoRefresh(true).Start("Crawling", (_) => {
            var r = crawler.Crawl(new Uri(url, UriKind.RelativeOrAbsolute)).Result;
            r.Add("addedDate", DateTime.Now);
            r.Add("url", url);

            Repository.Add(r);

            if (r.ContainsKey("imageUri"))
            {
                Repository.AddFile(r["imageUri"], r["_id"].RawValue.ToString(), r["_id"].RawValue + Path.GetExtension(r["imageUri"]));
            }
        });

        Console.WriteLine("Success");
        Thread.Sleep(1000);

        parentMenu.Show();
    }
}