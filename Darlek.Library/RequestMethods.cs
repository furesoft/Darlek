﻿using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Darlek.Library.Types;
using System.Collections.Generic;
using System.Net;

namespace Darlek.Library;

[RuntimeType("network")]
public static class RequestMethods
{
    [RuntimeMethod("make-request")]
    public static object Make()
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
        req.OnReadyStateChange += () => {
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