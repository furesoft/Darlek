using LiteDB;
using Schemy;

namespace BookGenerator.Core
{
    public interface IImportProvider
    {
        Symbol Extension { get; }

        BsonDocument Import(byte[] content);
    }
}