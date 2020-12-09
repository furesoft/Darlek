using Schemy;
using System.Collections.Generic;

namespace BookGenerator.Core.RuntimeLibrary
{
    public class RuntimeStruct : Dictionary<Symbol, object>
    {
        public Symbol Typename { get; set; }

        public override string ToString()
        {
            return Typename.AsString;
        }
    }
}