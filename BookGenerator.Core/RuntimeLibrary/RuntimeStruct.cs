using Schemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookGenerator.Core.RuntimeLibrary
{
    public class RuntimeStruct : List<object>
    {
        public Symbol Typename { get; set; }
        public Symbol[] PropertyNames { get; set; }
    }
}