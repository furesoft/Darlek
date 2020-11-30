namespace BookGenerator.Core.Epub.Extensions
{
	public static class StringExt
	{
		public static string ToAbsolutePath(this string filename, string basePath)
		{
			var path = PathExt.Combine(PathExt.GetDirectoryPath(basePath), filename);
			return path;
		}
	}
}