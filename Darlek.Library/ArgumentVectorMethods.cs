using Darlek.Core.CLI;
using Darlek.Core.RuntimeLibrary;

namespace Darlek.Library;

[RuntimeType("cli-vector")]
public static class ArgumentVectorMethods
{
    [RuntimeMethod("get-option")]
    public static object GetOption(CommandlineArguments cmdvec, string shortTerm, string longTerm)
    {
        return cmdvec.GetOption(shortTerm, longTerm);
    }

    [RuntimeMethod("option?")]
    public static object HasOption(CommandlineArguments cmdvec, string key)
    {
        return cmdvec.HasOption(key);
    }

    [RuntimeMethod("get-value")]
    public static object GetValue(CommandlineArguments cmdvec, string param)
    {
        return cmdvec.GetValue<string>(param);
    }
}