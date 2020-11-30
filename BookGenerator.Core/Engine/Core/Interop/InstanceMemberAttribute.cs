using System;

namespace BookGenerator.Core.Engine.Core.Interop
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public sealed class InstanceMemberAttribute : Attribute
	{
	}
}