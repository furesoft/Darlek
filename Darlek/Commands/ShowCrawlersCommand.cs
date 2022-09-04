using Darlek.Core;
using System;

namespace Darlek.Commands;

public class ShowCrawlersCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        Console.WriteLine("chefkoch");

        parentMenu.WaitAndShow();
    }
}