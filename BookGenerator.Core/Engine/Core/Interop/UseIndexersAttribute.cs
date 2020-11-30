using System;

namespace BookGenerator.Core.Engine.Core.Interop
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class UseIndexersAttribute : Attribute
	{
	}
}