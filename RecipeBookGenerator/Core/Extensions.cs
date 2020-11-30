using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Extensions;
using LiteDB;
using System.Collections.Generic;

namespace BookGenerator.Core
{
    public static class Extensions
    {
        public static BsonDocument ToBsonDocument(this JSObject obj)
        {
            var result = new BsonDocument();

            foreach (var prop in obj)
            {
                result.Add(prop.Key, BuildValue(prop.Value));
            }

            return result;
        }

        private static BsonValue BuildValue(JSValue value)
        {
            return new BsonValue(value.Value);
        }

        public static JSValue ToJSValue(this IHtmlCollection<IElement> coll)
        {
            var res = new List<JSValue>();

            foreach (var item in coll)
            {
                res.Add(ToJSValue((IHtmlElement)item));
            }

            return JSValue.Wrap(res.ToArray());
        }

        public static JSValue ToJSValue(this IElement element)
        {
            var attrs = new Dictionary<string, JSValue>();
            var innerHtml = JSValue.Marshal(element.InnerHtml);
            var content = JSValue.Marshal(element.TextContent);

            var children = new List<JSValue>();

            foreach (var attr in element.Attributes)
            {
                attrs.Add(attr.Name, JSValue.Marshal(attr.Value));
            }

            foreach (var c in element.Children)
            {
                children.Add(ToJSValue((IHtmlElement)c));
            }

            var result = JSObject.CreateObject();

            result.DefineProperty("innerHtml").Assign(innerHtml);
            result.DefineProperty("content").Assign(content);
            result.DefineProperty("children").Assign(JSValue.Marshal(children.ToArray()));
            //ToDo: add attributes property to result object

            return result;
        }
    }
}