using System;

namespace BookGenerator.Core.RuntimeLibrary
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RuntimeMethodAttribute : Attribute
    {
        public RuntimeMethodAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}