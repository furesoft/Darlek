using Darlek.Core.Schemy;
using System;

namespace Darlek.Core.RuntimeLibrary;

public class RuntimeTypeAttribute : Attribute
{
    public Symbol Module { get; set; }

    public RuntimeTypeAttribute(string module)
    {
        Module = Symbol.FromString(module);
    }

    public RuntimeTypeAttribute()
    {
    }
}