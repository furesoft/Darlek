using BookGenerator.Core;
using BookGenerator.Core.RuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BookGenerator.Library
{
    [RuntimeType]
    public static class IOMethods
    {
        [RuntimeMethod("printf")]
        public static void PrintF(string format, List<object> args)
        {
            Tools.printf(format, args.ToArray());
        }
    }
}