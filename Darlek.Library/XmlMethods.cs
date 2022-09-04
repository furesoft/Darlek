using Darlek.Core.RuntimeLibrary;
using System.Linq;
using System.Xml;

namespace Darlek.Library;

[RuntimeType("data.xml")]
public static class XmlMethods
{
    [RuntimeMethod("parse-xml")]
    public static object Parse(string xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);

        return doc;
    }

    [RuntimeMethod("xml-xpath")]
    public static object XPath(XmlDocument doc, string xpath)
    {
        return doc.SelectSingleNode(xpath);
    }

    [RuntimeMethod("xml-getby-id")]
    public static object GetElementByID(XmlDocument doc, string id)
    {
        return doc.GetElementById(id);
    }

    [RuntimeMethod("xml-getcontent")]
    public static object GetContent(XmlDocument doc)
    {
        return doc.InnerText;
    }

    [RuntimeMethod("xml-getattr")]
    public static object GetAttr(XmlElement doc, string attname)
    {
        return doc.GetAttribute(attname);
    }

    [RuntimeMethod("xml-getchildren")]
    public static object GetChildren(XmlElement doc)
    {
        return doc.ChildNodes.Cast<object>();
    }
}