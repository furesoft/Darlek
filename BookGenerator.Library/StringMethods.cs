using BookGenerator.Core.RuntimeLibrary;

namespace BookGenerator.Library
{
    [RuntimeType]
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
    }
}