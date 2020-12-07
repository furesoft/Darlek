using BookGenerator.Core.CLI;
using Schemy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BookGenerator.Core
{
    public static class ImportProvider
    {
        private static Dictionary<Symbol, IImportProvider> _providers = new Dictionary<Symbol, IImportProvider>();

        public static void Register(IImportProvider provider)
        {
            if (!_providers.ContainsKey(provider.Extension))
            {
                _providers.Add(provider.Extension, provider);
            }
        }

        public static IEnumerable<Symbol> GetAllProviders()
        {
            return _providers.Keys;
        }

        public static IImportProvider GetProvider(string filename)
        {
            var extension = Symbol.FromString(Path.GetExtension(filename));

            if (_providers.ContainsKey(extension))
            {
                return _providers[extension];
            }

            throw new System.Exception($"No import provider registered for type '{extension.AsString}'");
        }

        public static void Collect(Assembly ass)
        {
            foreach (var t in ass.GetTypes())
            {
                if (typeof(IImportProvider).IsAssignableFrom(t) && !t.IsInterface)
                {
                    var att = t.GetCustomAttribute<DoNotTrackAttribute>();

                    if (att == null)
                    {
                        var instance = (IImportProvider)Activator.CreateInstance(t);

                        Register(instance);
                    }
                }
            }
        }
    }
}