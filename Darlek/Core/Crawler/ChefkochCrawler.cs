using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Darlek.Core.Crawler;

public class ChefkochCrawler : ICrawler
{
    public async Task<BsonDocument> Crawl(Uri url)
    {
        var parser = new HtmlParser();

        var wc = new WebClient();

        if (!url.LocalPath.Contains("drucken"))
        {
            url = new Url($"https://www.chefkoch.de/rezepte/drucken/{url.Segments[^2]}{url.Segments[^1]}");
        }

        var content = await wc.DownloadStringTaskAsync(url.ToString());
        var document = parser.ParseDocument(content);

        var recipe = new BsonDocument
        {
            { "name", document.QuerySelectorAll("a[class='ds-copy-link bi-recipe-title']")[0].TextContent.Trim() },
            { "content", document.QuerySelectorAll("div[class='ds-box']")[4].TextContent.Trim() },
            { "author", document.QuerySelectorAll("a[class='ds-copy-link bi-profile']")[1].TextContent.Trim() },
            { "imageUri", document.QuerySelectorAll("img")[0].Attributes.GetNamedItem("src").Value },
            { "portions", document.QuerySelectorAll("div[class='ds-box']")[1].QuerySelector("h3").TextContent },
            { "time", document.QuerySelector("table[id='recipe-info']").OuterHtml.Trim() },
            { "ingredientsTables", ParseIngredients(document.QuerySelectorAll("div[class='ds-box']")[1].QuerySelector("table")) },
            { "tags", new BsonArray()}
        };

        return recipe;
    }

    private BsonArray ParseIngredients(IElement element)
    {
        //arr[(name, elements)]

        //doc
        //name
        //elements
        var elements = new BsonArray();
        var res = new List<BsonDocument>();
        var tabledoc = new BsonDocument();

        foreach (var row in element.QuerySelectorAll("tr"))
        {
            var entry = row.QuerySelectorAll("td");
            var measureEntry = entry.First();
            var itemEntry = entry.Last();

            if (string.IsNullOrEmpty(measureEntry.TextContent.Trim())
                && string.IsNullOrEmpty(itemEntry.TextContent.Trim()))
            {
                continue;
            }

            if (measureEntry != null && itemEntry != null)
            {
                var entryDocument = new BsonDocument();

                if (!string.IsNullOrEmpty(measureEntry.TextContent.Trim()))
                {
                    if (measureEntry.QuerySelector("strong") != null)
                    {
                        entryDocument.Add("measure", measureEntry.QuerySelector("strong").TextContent);
                    }
                    else if (measureEntry.QuerySelector("span") != null)
                    {
                        tabledoc.Add("elements", elements);
                        tabledoc = [];
                        elements = [];

                        res.Add(tabledoc);

                        tabledoc.Add("name", measureEntry.QuerySelector("span").TextContent);
                        continue;
                    }
                }
                if (!string.IsNullOrEmpty(itemEntry.TextContent.Trim()))
                {
                    entryDocument.Add("item", itemEntry.QuerySelector("span").TextContent);
                }

                elements.Add(entryDocument);
            }
        }

        tabledoc.Add("elements", elements);

        if (res.Count == 0)
        {
            res.Add(tabledoc);
        }

        return new BsonArray(res);
    }
}