using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using System;

namespace Darlek.Library;

[RuntimeType()]
public static class ConsoleMethods
{
    [RuntimeMethod("display")]
    public static object Display(string msg)
    {
        Console.WriteLine(msg);

        return None.Instance;
    }
}