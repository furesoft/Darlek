using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using System;

namespace Darlek.Library;

[RuntimeType]
public static class MenuMethods
{
    [RuntimeMethod("show-menu")]
    public static object ShowMenu(Menu menu)
    {
        menu.Show();

        return None.Instance;
    }

    [RuntimeMethod("wait-menu")]
    public static object WaitMenu(Menu menu)
    {
        Console.ReadKey();
        menu.Show();

        return None.Instance;
    }
}