using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using LiteDB;

namespace Darlek.Library;

[RuntimeType("ObjectModel")]
public static class ObjectModelMethods
{
    [RuntimeMethod("make-object")]
    public static BsonDocument MakeObject()
    {
        return [];
    }

    [RuntimeMethod("get-property")]
    public static BsonValue GetProperty(BsonDocument doc, Symbol name)
    {
        return doc[name.AsString];
    }

    [RuntimeMethod("set-property")]
    public static void SetProperty(BsonDocument doc, Symbol name, object value)
    {
        if (doc.ContainsKey(name.AsString))
        {
            doc[name.AsString] = BsonMapper.Global.Serialize(value);
        }
        else
        {
            doc.Add(name.AsString, BsonMapper.Global.Serialize(value));
        }
    }
}