using LiteDB;
using System;
using System.Threading.Tasks;

namespace Darlek.Core;

public interface ICrawler
{
    Task<BsonDocument> Crawl(string id);

    Task<BsonDocument> Crawl(Uri url);
}