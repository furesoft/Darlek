using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using System;

namespace Darlek.Library;

[RuntimeType]
public static class MenuMethods
{
    [RuntimeMethod("show-menu")]
    public static void ShowMenu(Menu menu)
    {
        menu.Show();
    }

    [RuntimeMethod("wait-menu")]
    public static void WaitMenu(Menu menu)
    {
        Console.ReadKey();
        menu.Show();
    }
}