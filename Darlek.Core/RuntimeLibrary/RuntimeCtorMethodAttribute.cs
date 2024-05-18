using System;

namespace Darlek.Core.RuntimeLibrary;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class RuntimeCtorMethodAttribute(string name) : Attribute
{

    public string Name { get; set; } = name;
}