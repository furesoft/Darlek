using System;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Core.Interop;

namespace BookGenerator.Core.Engine.BaseLibrary
{
	[Prototype(typeof(Error))]
#if !(PORTABLE || NETCORE)
	[Serializable]
#endif
	public sealed class RangeError : Error
	{
		[DoNotEnumerate]
		public RangeError()
		{
		}

		[DoNotEnumerate]
		public RangeError(Arguments args)
			: base(args[0].ToString())
		{
		}

		[DoNotEnumerate]
		public RangeError(string message)
			: base(message)
		{
		}
	}
}