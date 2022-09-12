using Darlek.Core.Crawler;

namespace Darlek.Commands.Manage;

public class WhatShouldICookCommand : WhatShouldICommand
{
    public WhatShouldICookCommand(ChefkochCrawler crawler) :
        base(crawler, "https://www.chefkoch.de/rezepte/was-koche-ich-heute/")
    {
    }
}