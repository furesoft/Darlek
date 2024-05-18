using Darlek.Core.Schemy;
using LiteDB;

namespace Darlek.Core.ImportProviders;

[DoNotTrack]
public class SchemeImporter(Symbol extension, Procedure invoker) : IImportProvider
{
    private readonly Procedure _invoker = invoker;
    private readonly Symbol _extension = extension;

    public Symbol Extension => _extension;

    public BsonDocument Import(byte[] content)
    {
        return (BsonDocument)_invoker.Call([content]);
    }
}