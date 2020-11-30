using BookGenerator.Core.RuntimeLibrary;

namespace BookGenerator.Library
{
    [RuntimeType]
    public static class ConverterMethods
    {
        [RuntimeMethod("object->string")]
        public static string ToString(object input)
        {
            return input.ToString();
        }

        [RuntimeMethod("string->num")]
        public static int ToNum(string input)
        {
            return int.Parse(input);
        }
    }
}