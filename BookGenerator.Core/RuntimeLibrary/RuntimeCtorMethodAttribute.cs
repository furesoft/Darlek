using System;

namespace BookGenerator.Core.RuntimeLibrary
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RuntimeCtorMethodAttribute : Attribute
    {
        public RuntimeCtorMethodAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}