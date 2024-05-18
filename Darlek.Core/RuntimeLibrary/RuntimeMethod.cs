using System;

namespace Darlek.Core.RuntimeLibrary;

[AttributeUsage(AttributeTargets.Method)]
public class RuntimeMethodAttribute(string name) : Attribute
{

    public string Name { get; set; } = name;
}