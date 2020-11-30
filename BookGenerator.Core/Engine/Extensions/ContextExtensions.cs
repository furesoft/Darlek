using BookGenerator.Core.Engine.Core;

namespace BookGenerator.Core.Engine.Extensions
{
	public static class ContextExtensions
	{
		public static void Add(this Context context, string key, object value)
		{
			context.DefineVariable(key).Assign(value);
		}
	}
}