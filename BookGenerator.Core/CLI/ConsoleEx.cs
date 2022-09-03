using System;

namespace BookGenerator.Core.CLI
{
    public static class ConsoleEx
    {
        public static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}