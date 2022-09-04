using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using System.Collections.Generic;

namespace Darlek.Library;

[RuntimeType]
public static class IOMethods
{
    [RuntimeMethod("printf")]
    public static void PrintF(string format, List<object> args)
    {
        Tools.printf(format, args.ToArray());
    }
}