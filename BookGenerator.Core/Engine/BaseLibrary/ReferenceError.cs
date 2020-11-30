using System;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Core.Interop;

namespace BookGenerator.Core.Engine.BaseLibrary
{
	[Prototype(typeof(Error))]
#if !(PORTABLE || NETCORE)
	[Serializable]
#endif
	public sealed class ReferenceError : Error
	{
		[DoNotEnumerate]
		public ReferenceError(Arguments args)
			: base(args[0].ToString())
		{
		}

		[DoNotEnumerate]
		public ReferenceError()
		{
		}

		[DoNotEnumerate]
		public ReferenceError(string message)
			: base(message)
		{
		}
	}
}