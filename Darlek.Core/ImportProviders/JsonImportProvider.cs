using Darlek.Core;
using Darlek.Core.Schemy;
using LiteDB;
using System;
using System.IO;
using System.Text;

namespace Darlek.Core.ImportProviders;

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