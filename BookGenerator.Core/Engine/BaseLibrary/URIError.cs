using System;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Core.Interop;

namespace BookGenerator.Core.Engine.BaseLibrary
{
    [Prototype(typeof(Error))]
#if !(PORTABLE || NETCORE)
    [Serializable]
#endif
    public sealed class URIError : Error
    {
        [DoNotEnumerate]
        public URIError()
        {
        }

        [DoNotEnumerate]
        public URIError(Arguments args)
            : base(args[0].ToString())
        {
        }

        [DoNotEnumerate]
        public URIError(string message)
            : base(message)
        {
        }
    }
}