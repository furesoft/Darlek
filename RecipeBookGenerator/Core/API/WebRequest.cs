using System.Net;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BookGenerator.Core.Engine.BaseLibrary;

namespace BookGenerator.Core.API
{
	public static class WebRequest
	{
		public static object DownloadObject(string uri)
		{
			var wc = new WebClient();
			var content = wc.DownloadString(uri);

			return JSON.parse(content);
		}

		public static IHtmlDocument GetDocument(string uri)
		{
			var wc = new WebClient();
			var content = wc.DownloadString(uri);

			return new HtmlParser().ParseDocument(content);
		}
	}
}