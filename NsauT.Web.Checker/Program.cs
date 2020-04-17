using NsauT.Shared.Models.BusinessModels;
using NsauT.Web.Parser;
using System;
using System.Collections.Generic;

namespace NsauT.Web.Checker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checker started.");

            var parser = new ParserWorker();
            parser.TimetableFileParsed += StartCheckTimetables;
            parser.StartParse();
        }

        private static void StartCheckTimetables(object sender, EventArgs e)
        {
            var parserArgs = e as ParserEventArgs;
            var timetableChecker = new TimetableChecker();

            List<TimetableModel> timetables = parserArgs.Timetables;
            timetableChecker.CheckTimetables(timetables);
        }
    }
}
