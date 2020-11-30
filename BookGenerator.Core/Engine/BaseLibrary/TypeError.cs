using System;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Core.Interop;

namespace BookGenerator.Core.Engine.BaseLibrary
{
	[Prototype(typeof(Error))]
#if !(PORTABLE || NETCORE)
	[Serializable]
#endif
	public sealed class TypeError : Error
	{
		[DoNotEnumerate]
		public TypeError(Arguments args)
			: base(args[0].ToString())
		{
		}

		[DoNotEnumerate]
		public TypeError()
		{
		}

		[DoNotEnumerate]
		public TypeError(string message)
			: base(message)
		{
		}
	}
}