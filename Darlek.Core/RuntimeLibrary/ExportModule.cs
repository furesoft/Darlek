using Darlek.Core.Schemy;

namespace Darlek.Core.RuntimeLibrary;

public class ExportModule
{
    public ExportModule(Symbol symbol, object value)
    {
        Symbol = symbol;
        Value = value;
    }

    public Symbol Symbol { get; }
    public object Value { get; }
}