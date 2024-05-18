using AngleSharp.Html.Parser;
using Darlek.Core;
using Darlek.Core.Crawler;
using Spectre.Console;
using System;
using System.Net;

namespace Darlek.Commands.Manage.Chefkoch;

public class RecipeOfTheDayCommand(ChefkochCrawler chefkochCrawler) : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var wc = new WebClient();

        var content = wc.DownloadStringTaskAsync("https://www.chefkoch.de/rezept-des-tages/").Result;
        var parser = new HtmlParser();
        var document = parser.ParseDocument(content);

        var title = document.QuerySelector("div[class='card__main'] h3").InnerHtml;
        var link = document.QuerySelector("div[class='card__main'] a").GetAttribute("href");

        var url = "https://www.chefkoch.de" + link + title.Replace(" ", "_") + ".html";

        var crawler = CrawlerFactory.GetCrawlerByHost(url);

        var recipe = AnsiConsole.Status().AutoRefresh(true).Start("Loading " + title, _ => {
            return crawler.Crawl(new Uri(url, UriKind.RelativeOrAbsolute)).Result;
        });

        var menu = new Menu(parentMenu);
        menu.Items.Add("View", new ViewRecipeCommand(recipe));
        menu.Items.Add("Add", new DelegateCommand(_ => {
            AnsiConsole.Status().AutoRefresh(true).Start("Crawling " + title, _ => {
                Repository.Crawl(url);
            });

            parentMenu.Show();
        }));

        menu.Show();
    }
}