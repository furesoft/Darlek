using Darlek.Core.UI;
using System;

namespace Darlek.Commands;

internal class ExitCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        Console.Clear();
        Environment.Exit(0);
    }
}