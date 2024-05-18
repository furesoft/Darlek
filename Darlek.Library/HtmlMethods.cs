using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Darlek.Core.RuntimeLibrary;

namespace Darlek.Library;

[RuntimeType("html")]
public static class HtmlMethods
{
    [RuntimeMethod("parse-document")]
    public static IHtmlDocument Parse(string content)
    {
        var parser = new HtmlParser();
        
        return parser.ParseDocument(content);
    }

    [RuntimeMethod("query-selector-all")]
    public static IHtmlCollection<IElement> QuerySelectorAll(IHtmlDocument document, string selector)
    {
        return document.QuerySelectorAll(selector);
    }

    [RuntimeMethod("query-selector")]
    public static IElement QuerySelector(IHtmlDocument document, string selector)
    {
        return document.QuerySelector(selector);
    }
}