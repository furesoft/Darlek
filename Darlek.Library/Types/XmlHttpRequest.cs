using System;
using System.IO;
using System.Net;

namespace Darlek.Library.Types;

public class XmlHttpRequest
{
    public enum Readystate { Opened, Recieved, Sended, Finished }

    public Action OnReadyStateChange;

    public Readystate ReadyState {
        get { return _readyState; }
        set {
            OnReadyStateChange();
            _readyState = value;
        }
    }

    public string MimeType { get { return _webrequest.MediaType; } set { _webrequest.MediaType = value; } }
    public string ResponseText { get; set; }

    private HttpWebRequest _webrequest;
    private Readystate _readyState;

    public void Open(string method, string url)
    {
        _webrequest = (HttpWebRequest)WebRequest.Create(url);
        _webrequest.Method = method;
        ReadyState = Readystate.Opened;
    }

    public void Send(object data)
    {
        if (data == null)
        {
            var resp = (HttpWebResponse)_webrequest.GetResponse();
            using (var s = resp.GetResponseStream())
            {
                ResponseText = new StreamReader(s).ReadToEnd();
                ReadyState = Readystate.Recieved;
            }
        }
        else
        {
            using (var s = _webrequest.GetRequestStream())
            {
                var sw = new StreamWriter(s);
                sw.Write(data);
                sw.Flush();
                ReadyState = Readystate.Sended;
            }
        }
    }
}