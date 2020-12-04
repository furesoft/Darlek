using System;
using System.IO;
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
            var extension = Path.GetExtension(name);
            var content = Repository.GetFile($"{name}");
            if (extension == ".js") new JSCrawler(content);
            if (extension == ".ss") new SchemeCrawler(content);
            // need use crawler command
            return new JSCrawler(Repository.GetFile($"{name}.js"));
        }

        public static bool IsDefault(string name)
        {
            return name == "chefkoch";
        }
    }
}