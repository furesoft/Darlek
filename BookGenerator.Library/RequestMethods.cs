using BookGenerator.Core.API;
using BookGenerator.Core.RuntimeLibrary;
using Furesoft.Core;
using Schemy;
using System.Collections.Generic;

namespace BookGenerator.Library
{
    [RuntimeType]
    public static class RequestMethods
    {
        [RuntimeMethod("make-request")]
        public static object Make()
        {
            return new XmlHttpRequest();
        }

        [RuntimeMethod("request-open")]
        public static object Open(XmlHttpRequest req, string method, string url)
        {
            req.Open(method, url);

            return None.Instance;
        }

        [RuntimeMethod("request-send")]
        public static object Send(XmlHttpRequest req, object data)
        {
            req.Send(data);

            return None.Instance;
        }

        [RuntimeMethod("request-subscribe-readystatechanged")]
        public static object OnChange(XmlHttpRequest req, Procedure callback)
        {
            req.OnReadyStateChange += () =>
            {
                callback.Call(new List<object> { req.ReadyState });
            };

            return None.Instance;
        }

        [RuntimeMethod("request-get-response")]
        public static object GetResponse(XmlHttpRequest req)
        {
            return req.ResponseText;
        }
    }
}