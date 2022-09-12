using AngleSharp.Html.Parser;
using Darlek.Core;
using Darlek.Core.Crawler;
using Spectre.Console;
using System;
using System.Linq;
using System.Net;

namespace Darlek.Commands.Manage;

public class WhatShouldICommand : IMenuCommand
{
    private ChefkochCrawler crawler;

    private string url;

    public WhatShouldICommand(ChefkochCrawler crawler, string url)
    {
        this.crawler = crawler;
        this.url = url;
    }

    public void Invoke(Menu parentMenu)
    {
        var wc = new WebClient();

        var content = wc.DownloadStringTaskAsync(url.ToString()).Result;
        var parser = new HtmlParser();
        var document = parser.ParseDocument(content);

        var n = document.QuerySelectorAll("a[class='ds-teaser-link ds-recipe-card__link']");

        var selection = new SelectionPrompt<(string, string)>();
        selection.Converter = _ => _.Item1;

        selection.AddChoice(("...", null));
        selection.AddChoices(n.Select(_ => (_.GetAttribute("title"), _.GetAttribute("href"))));

        var selected = AnsiConsole.Prompt(selection);

        if (selected.Item2 != null)
        {
            var crawler = CrawlerFactory.GetCrawler(Repository.GetMetadata("crawler") ?? "chefkoch");
            var r = crawler.Crawl(new Uri(selected.Item2, UriKind.RelativeOrAbsolute)).Result;

            var menu = new Menu(parentMenu);
            menu.Items.Add("View", new ViewRecipeCommand(r));
            menu.Items.Add("Add", new DelegateCommand(_ => {
                Repository.Crawl(selected.Item2);

                parentMenu.Show();
            }));

            menu.Show();
        }

        parentMenu.Show();
    }
}