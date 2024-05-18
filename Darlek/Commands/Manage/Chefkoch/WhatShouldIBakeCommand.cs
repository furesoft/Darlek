using Darlek.Core.Crawler;

namespace Darlek.Commands.Manage.Chefkoch;

public class WhatShouldIBakeCommand(ChefkochCrawler crawler) : WhatShouldICommand(crawler, "https://www.chefkoch.de/rezepte/was-backe-ich-heute/")
{
}