using Darlek.Core.Schemy;

namespace Darlek.Core.RuntimeLibrary;

public class ExportModule(Symbol symbol, object value)
{

    public Symbol Symbol { get; } = symbol;
    public object Value { get; } = value;
}