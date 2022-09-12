﻿using Darlek.Core.Crawler;

namespace Darlek.Commands.Manage;

public class WhatShouldIBakeCommand : WhatShouldICommand
{
    public WhatShouldIBakeCommand(ChefkochCrawler crawler) :
        base(crawler, "https://www.chefkoch.de/rezepte/was-backe-ich-heute/")
    {
    }
}