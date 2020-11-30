using System;

namespace BookGenerator.Core.Engine.Core.Interop
{
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class InliningAttribute : Attribute
	{
	}
}