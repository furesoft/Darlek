using BookGenerator.Core.RuntimeLibrary;
using Schemy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookGenerator.Library
{
    [RuntimeType()]
    public static class ConsoleMethods
    {
        [RuntimeMethod("display")]
        public static object Display(string msg)
        {
            Console.WriteLine(msg);

            return None.Instance;
        }
    }
}