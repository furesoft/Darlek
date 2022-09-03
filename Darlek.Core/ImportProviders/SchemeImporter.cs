using Darlek.Core.CLI;
using Darlek.Core.Schemy;
using LiteDB;

namespace Darlek.Core.ImportProviders;

[DoNotTrack]
public class SchemeImporter : IImportProvider
{
    private readonly Procedure _invoker;
    private readonly Symbol _extension;

    public SchemeImporter(Symbol extension, Procedure invoker)
    {
        _invoker = invoker;
        _extension = extension;
    }

    public Symbol Extension => _extension;

    public BsonDocument Import(byte[] content)
    {
        return (BsonDocument)_invoker.Call(new System.Collections.Generic.List<object> { content });
    }
}