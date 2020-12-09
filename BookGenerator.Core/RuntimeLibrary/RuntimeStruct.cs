using Schemy;
using System.Collections.Generic;

namespace BookGenerator.Core.RuntimeLibrary
{
    public class RuntimeStruct : Dictionary<Symbol, object>
    {
        public Symbol Typename { get; set; }
    }
}