using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using LiteDB;

namespace Darlek.Core.Crawler;

public class ChefkochCrawler : ICrawler
{
    public async Task<BsonDocument> Crawl(string id)
    {
#if DEBUG
        return await Crawl(new Uri($"{Environment.CurrentDirectory}\\debug.html"));
#else

			return await Crawl(new Uri($"https://www.chefkoch.de/rezepte/drucken/{id}.html"));
#endif
    }

    public async Task<BsonDocument> Crawl(Uri url)
    {
        var parser = new HtmlParser();

        var wc = new WebClient();

        var content = await wc.DownloadStringTaskAsync(url.ToString());
        var document = parser.ParseDocument(content);

        var recipe = new BsonDocument();
        recipe.Add("name", document.QuerySelectorAll("a[class='ds-copy-link bi-recipe-title']")[0].TextContent.Trim());
        recipe.Add("content", document.QuerySelectorAll("div[class='ds-box']")[4].TextContent.Trim());
        recipe.Add("author", document.QuerySelectorAll("a[class='ds-copy-link bi-profile']")[1].TextContent.Trim());
        recipe.Add("imageUri", document.QuerySelectorAll("img")[0].Attributes.GetNamedItem("src").Value);
        recipe.Add("portions", document.QuerySelectorAll("div[class='ds-box']")[1].QuerySelector("h3").TextContent);
        recipe.Add("time", document.QuerySelector("table[id='recipe-info']").OuterHtml.Trim());

        recipe.Add("ingredients", ParseIngredients(document.QuerySelectorAll("div[class='ds-box']")[1].QuerySelector("table")));

        return recipe;
    }

    private BsonArray ParseIngredients(IElement element)
    {
        var res = new BsonArray();

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
                    entryDocument.Add("measure", measureEntry.QuerySelector("strong").TextContent);
                }
                if (!string.IsNullOrEmpty(itemEntry.TextContent.Trim()))
                {
                    entryDocument.Add("item", itemEntry.QuerySelector("span").TextContent);
                }

                res.Add(entryDocument);
            }
        }

        return res;
    }
}