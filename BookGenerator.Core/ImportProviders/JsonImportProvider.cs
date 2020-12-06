using LiteDB;
using Schemy;
using System;
using System.IO;
using System.Text;

namespace BookGenerator.Core.ImportProviders
{
    public class JsonImportProvider : IImportProvider
    {
        public Symbol Extension => Symbol.FromString(".json");

        public BsonDocument Import(byte[] content)
        {
            var contentText = Encoding.Default.GetString(content);

            var obj = new JsonReader(new StringReader(contentText));
            var doc = obj.Deserialize();

            return doc.AsDocument;
        }
    }
}