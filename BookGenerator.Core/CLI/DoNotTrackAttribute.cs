using System;

namespace BookGenerator.Core.CLI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DoNotTrackAttribute : Attribute
    {
    }
}