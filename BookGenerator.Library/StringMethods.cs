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

    [RuntimeType]
    public struct Point
    {
        [RuntimeCtorMethod("point")]
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}