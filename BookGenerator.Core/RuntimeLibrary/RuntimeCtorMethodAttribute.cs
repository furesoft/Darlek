using System;

namespace BookGenerator.Core.RuntimeLibrary
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class RuntimeCtorMethodAttribute : Attribute
    {
        public RuntimeCtorMethodAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}