using LiteDB;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookGenerator.Core.Crawler
{
    public static class EvaluatorSelector
    {
        public static IEvaluator<T> GetEvaluator<T>(string filename)
        {
            if (Path.GetExtension(filename) == ".js")
            {
                return (IEvaluator<T>)new JsEvaluator();
            }

            return null;
        }
    }
}