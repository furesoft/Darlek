using Darlek.Core;
using Darlek.Core.GrocySync;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Threading.Tasks;

namespace Darlek.CliCommands;

public class GrocySyncCommand : AsyncCommand<GrocySyncCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "url")]
        public string Url { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var crawler = CrawlerFactory.GetCrawlerByHost(settings.Url);

        if (crawler == null)
        {
            AnsiConsole.MarkupLine("[red]No crawler found for the specified URL.[/]");
            return 1;
        }

        var recipe = crawler.Crawl(new Uri(settings.Url)).Result;
        await GrocySyncService.Sync(recipe);

        return 0;
    }
}