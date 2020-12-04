using BookGenerator.Core.CLI;
using BookGenerator.Core.RuntimeLibrary;
using Schemy;
using System;

namespace BookGenerator.Library
{
    [RuntimeType("cli-vector")]
    public static class ArgumentVectorMethods
    {
        [RuntimeMethod("get-option")]
        public static object GetOption(CommandlineArguments cmdvec, string shortTerm, string longTerm)
        {
            return cmdvec.GetOption(shortTerm, longTerm);
        }

        [RuntimeMethod("get-value")]
        public static object GetValue(CommandlineArguments cmdvec, string param)
        {
            return cmdvec.GetValue<string>(param);
        }
    }
}