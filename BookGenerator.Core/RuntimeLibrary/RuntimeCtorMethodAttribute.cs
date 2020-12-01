using System;

namespace BookGenerator.Core.RuntimeLibrary
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class RuntimeCtorMethodAttribute : Attribute
    {
        public RuntimeCtorMethodAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}