using System;

namespace BookGenerator.Core.Engine.Core.Interop
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, AllowMultiple = false)]
	public sealed class NotConfigurable : Attribute
	{
	}
}