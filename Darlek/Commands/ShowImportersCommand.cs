using Darlek.Core;
using System;

namespace Darlek.Commands;

public class ShowImportersCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        foreach (var ip in ImportProvider.GetAllProviders())
        {
            Console.WriteLine(ip.AsString);
        }

        parentMenu.WaitAndShow();
    }
}