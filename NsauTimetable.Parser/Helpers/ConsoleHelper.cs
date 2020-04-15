using NsauTimetable.Parser.Models;
using System;

namespace NsauTimetable.Parser.Helpers
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

        public static void ShowWebPageParserResult(WebPageParserResult result)
        {
            Console.WriteLine(result.Count);

            foreach (HyperlinkInfo link in result.Links)
            {
                Console.WriteLine("-------------");
                Console.WriteLine(link.Title);
                Console.WriteLine(link.Link);
            }
        }

        private static void ResetConsoleColor() => Console.ResetColor();
    }
}
