using System;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Core.Interop;

namespace BookGenerator.Core.Engine.BaseLibrary
{
    [Prototype(typeof(Error))]
#if !(PORTABLE || NETCORE)
    [Serializable]
#endif
    public sealed class SyntaxError : Error
    {
        [DoNotEnumerate]
        public SyntaxError()
        {
        }

        [DoNotEnumerate]
        public SyntaxError(Arguments args)
            : base(args[0].ToString())
        {
        }

        [DoNotEnumerate]
        public SyntaxError(string message)
            : base(message)
        {
        }
    }
}