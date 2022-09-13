using Darlek.Core;
using LiteDB;
using System;
using System.IO;

namespace Darlek.Commands.Manage;

public class RenewCommand : IMenuCommand
{
    public RenewCommand(BsonDocument selectedRecipe)
    {
        SelectedRecipe = selectedRecipe;
    }

    public BsonDocument SelectedRecipe { get; set; }

    public void Invoke(Menu parentMenu)
    {
        var url = SelectedRecipe["url"];
        var crawler = CrawlerFactory.GetCrawlerByHost(url);

        var r = crawler.Crawl(new Uri(url, UriKind.RelativeOrAbsolute)).Result;
        r["_id"] = SelectedRecipe["_id"];

        Repository.Update(r);

        if (r.ContainsKey("imageUri"))
        {
            Repository.AddFile(r["imageUri"], r["_id"].RawValue.ToString(), r["_id"].RawValue + Path.GetExtension(r["imageUri"]));
        }
    }
}