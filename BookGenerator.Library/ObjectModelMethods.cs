using BookGenerator.Core.RuntimeLibrary;
using LiteDB;
using Schemy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookGenerator.Library
{

    [RuntimeType("ObjectModel")]
    public static class ObjectModelMethods
    {
        [RuntimeMethod("make-object")]
        public static object MakeObject()
        {
            return new BsonDocument();
        }

        [RuntimeMethod("set-property")]
        public static object SetProperty(BsonDocument doc, Symbol name, object value)
        {
            if (doc.ContainsKey(name.AsString))
            {
                doc[name.AsString] = BsonMapper.Global.Serialize(value);
            }
            else
            {
                doc.Add(name.AsString, BsonMapper.Global.Serialize(value));
            }

            return None.Instance;
        }
    }
}