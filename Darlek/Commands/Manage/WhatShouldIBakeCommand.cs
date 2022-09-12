using AngleSharp.Html.Parser;
using Darlek.Core;
using Darlek.Core.Crawler;
using Spectre.Console;
using System.Linq;
using System.Net;

namespace Darlek.Commands.Manage;

public class WhatShouldIBakeCommand : IMenuCommand
{
    private ChefkochCrawler crawler;

    public WhatShouldIBakeCommand(ChefkochCrawler crawler)
    {
        this.crawler = crawler;
    }

    public void Invoke(Menu parentMenu)
    {
        var url = "https://www.chefkoch.de/rezepte/was-backe-ich-heute/";
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
            Repository.Crawl(selected.Item2);
        }

        parentMenu.Show();
    }
}