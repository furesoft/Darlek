using Darlek.Core.RuntimeLibrary;
using Darlek.Scheme;

namespace Darlek.Library;

[RuntimeType]
public static class ConsoleMethods
{
    [RuntimeMethod("display")]
    public static object Display(string msg)
    {
        Console.Write(msg);

        return None.Instance;
    }

    [RuntimeMethod("read-key")]
    public static object ReadKey()
    {
        return Console.ReadKey();
    }

    [RuntimeMethod("clear-console")]
    public static object Clear()
    {
        Console.Clear();

        return None.Instance;
    }
}