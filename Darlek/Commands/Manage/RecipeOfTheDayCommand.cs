using AngleSharp.Html.Parser;
using Darlek.Core;
using Darlek.Core.Crawler;
using System;
using System.Linq;
using System.Net;

namespace Darlek.Commands.Manage;

public class RecipeOfTheDayCommand : IMenuCommand
{
    private ChefkochCrawler cc;

    public RecipeOfTheDayCommand(ChefkochCrawler cc)
    {
        this.cc = cc;
    }

    public void Invoke(Menu parentMenu)
    {
        var wc = new WebClient();

        var content = wc.DownloadStringTaskAsync("https://www.chefkoch.de/rezept-des-tages/").Result;
        var parser = new HtmlParser();
        var document = parser.ParseDocument(content);

        var title = document.QuerySelectorAll("div[class='card__main'] h3").First().InnerHtml;
        var link = document.QuerySelectorAll("div[class='card__main'] a").First().GetAttribute("href");

        var url = "https://www.chefkoch.de" + link + title.Replace(" ", "_") + ".html";

        var crawler = CrawlerFactory.GetCrawler(Repository.GetMetadata("crawler") ?? "chefkoch");
        var r = crawler.Crawl(new Uri(url, UriKind.RelativeOrAbsolute)).Result;

        var menu = new Menu(parentMenu);
        menu.Items.Add("View", new ViewRecipeCommand(r));
        menu.Items.Add("Add", new DelegateCommand(_ => {
            Repository.Crawl(url);

            parentMenu.Show();
        }));

        menu.Show();
    }
}