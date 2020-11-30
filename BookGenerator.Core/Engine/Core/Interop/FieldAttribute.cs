using System;

namespace BookGenerator.Core.Engine.Core.Interop
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	internal sealed class FieldAttribute : Attribute
	{
	}
}