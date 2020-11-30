using System;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Core.Interop;

namespace BookGenerator.Core.Engine.BaseLibrary
{
#if !(PORTABLE || NETCORE)

    [Serializable]
#endif
    public sealed class EvalError : Error
    {
        [DoNotEnumerate]
        public EvalError()
        {
        }

        [DoNotEnumerate]
        public EvalError(Arguments args)
            : base(args[0].ToString())
        {
        }

        [DoNotEnumerate]
        public EvalError(string message)
            : base(message)
        {
        }
    }
}