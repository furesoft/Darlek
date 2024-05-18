using Darlek.Core.Crawler;

namespace Darlek.Commands.Manage.Chefkoch;

public class WhatShouldICookCommand(ChefkochCrawler crawler) : WhatShouldICommand(crawler, "https://www.chefkoch.de/rezepte/was-koche-ich-heute/")
{
}