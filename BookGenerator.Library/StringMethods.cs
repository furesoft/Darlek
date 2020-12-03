using BookGenerator.Core.RuntimeLibrary;

namespace BookGenerator.Library
{
    [RuntimeType("strings")]
    public static class StringMethods
    {
        [RuntimeMethod("substr")]
        public static string Substr(string src, int start, int length = 0)
        {
            if (length == 0)
            {
                return src.Substring(start);
            }

            return src.Substring(start, length);
        }

        [RuntimeMethod("index-of")]
        public static int IndexOf(string src, string value)
        {
            return src.IndexOf(value);
        }

        [RuntimeMethod("last-index-of")]
        public static int LastIndexOf(string src, string value)
        {
            return src.LastIndexOf(value);
        }
    }
}