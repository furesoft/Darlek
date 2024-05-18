using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Darlek.Library.Types;
using System.Net;

namespace Darlek.Library;

[RuntimeType("network")]
public static class RequestMethods
{
    [RuntimeMethod("make-request")]
    public static XmlHttpRequest Make()
    {
        return new XmlHttpRequest();
    }

    [RuntimeMethod("get-raw-bytes-from-url")]
    public static byte[] GetRawBytesFromUri(string url)
    {
        var wc = new WebClient();

        return wc.DownloadData(url);
    }

    [RuntimeMethod("get-raw-string-from-url")]
    public static string GetRawStringFromUri(string url)
    {
        var wc = new WebClient();

        return wc.DownloadString(url);
    }

    [RuntimeMethod("request-open")]
    public static void Open(XmlHttpRequest req, string method, string url)
    {
        req.Open(method, url);
    }

    [RuntimeMethod("request-send")]
    public static void Send(XmlHttpRequest req, object data, Procedure callback)
    {
        req.OnReadyStateChange += () => {
            callback.Call([req.ReadyState, req.ResponseText]);
        };

        req.Send(data);
    }

    [RuntimeMethod("request-get-response-text")]
    public static string GetResponse(XmlHttpRequest req)
    {
        return req.ResponseText;
    }
}