using System.IO;

namespace BookGenerator.Core.Crawler
{
    public static class EvaluatorSelector
    {
        public static IEvaluator<T> GetEvaluator<T>(string filename)
        {
            if (Path.GetExtension(filename) == ".ss")
            {
                return (IEvaluator<T>)new SchemeEvaluator();
            }

            return null;
        }
    }
}