using System;

namespace NsauT.Shared.Helpers
{
    public static class ConsoleHelper
    {
        private const ConsoleColor OkColor = ConsoleColor.Green;

        public static void WriteOk(string message)
        {
            Console.ForegroundColor = OkColor;
            Write(message);
            ResetConsoleColor();
        }

        public static void Write(string message)
        {
            Console.WriteLine(message);
        }

        public static void HorizontalLine() => Console.WriteLine(new string('-', 15));

        private static void ResetConsoleColor() => Console.ResetColor();
    }
}
