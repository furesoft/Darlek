using BookGenerator.Core.CLI;
using LiteDB;
using Schemy;
using System;

namespace BookGenerator.Core.ImportProviders
{
    [DoNotTrack]
    public class SchemeImporter : IImportProvider
    {
        private readonly Procedure _invoker;
        private readonly Symbol _extension;

        public SchemeImporter(Symbol extension, Procedure invoker)
        {
            this._invoker = invoker;
            this._extension = extension;
        }

        public Symbol Extension => _extension;

        public BsonDocument Import(byte[] content)
        {
            return (BsonDocument)_invoker.Call(new System.Collections.Generic.List<object> { content });
        }
    }
}