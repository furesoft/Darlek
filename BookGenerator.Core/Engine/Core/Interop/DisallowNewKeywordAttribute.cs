using System;

namespace BookGenerator.Core.Engine.Core.Interop
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class DisallowNewKeywordAttribute : Attribute
	{
	}
}