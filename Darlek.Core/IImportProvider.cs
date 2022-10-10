using Darlek.Scheme;
using LiteDB;

namespace Darlek.Core;

public interface IImportProvider
{
    Symbol Extension { get; }

    BsonDocument Import(byte[] content);
}