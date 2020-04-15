using NsauTimetable.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace NsauTimetable.Parser
{
    public class Worker
    {
        static Worker()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void Work()
        {
            //var webPageParser = new WebPageParser();
            //var result = webPageParser.GetLinksOfTimetables();
            //ConsoleHelper.WriteOk("WebParser OK");

            //result.Links = result.Links.Take(1).ToList();

            //var downloader = new FileDownloader();
            //downloader.DownloadFilesInParallel(result.Links, ParseFileOfTimetable);

            string fileName =
                @"C:\dev\Xamarin\NsauTimetable\NsauTimetable.Parser.Debug\bin\Debug\netcoreapp3.1\TimetablesFiles\1101_10_time_15_04_2020_08_38_03_558.xls";
            var timetableFileParser = new TimetableFileParser();
            timetableFileParser.ParseExcelFile(fileName);
        }

        private static void ParseFileOfTimetable(string fileName)
        {
            ConsoleHelper.Write("File downloaded: " + fileName);

            var timetableFileParser = new TimetableFileParser();
            timetableFileParser.ParseExcelFile(fileName);
        }
    }
}
