using System;
using BookGenerator.Core;
using BookGenerator.Core.Crawler;

namespace BookGenerator.Commands
{
	public static class Crawlerfactory
	{
		public static ICrawler GetCrawler(string name)
		{
			if (string.Equals(name, "chefkoch", StringComparison.CurrentCultureIgnoreCase))
			{
				return new ChefkochCrawler();
			}

			// need use crawler command
			return new JSCrawler(Repository.GetFile($"{name}.js"));
		}

		public static bool IsDefault(string name)
		{
			return name == "chefkoch";
		}
	}
}