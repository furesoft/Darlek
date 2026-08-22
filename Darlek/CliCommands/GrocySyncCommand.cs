using Darlek.Core;
using Darlek.Core.GrocySync;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Darlek.CliCommands;

public class GrocySyncCommand : AsyncCommand<GrocySyncCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "url")]
        public string Url { get; set; }
    }

    protected override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var crawler = CrawlerFactory.GetCrawlerByHost(settings.Url);

        if (crawler == null)
        {
            AnsiConsole.MarkupLine("[red]No crawler found for the specified URL.[/]");
            return 1;
        }

        var recipe = await crawler.Crawl(new Uri(settings.Url));
        await GrocySyncService.Sync(recipe);

        return 0;
    }
}