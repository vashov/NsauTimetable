using NsauT.Shared.Helpers;
using NsauT.Shared.Models.BusinessModels;
using NsauT.Web.Parser.Models.ExcelParsedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NsauT.Web.Parser
{
    public class ParserWorker
    {
        public event EventHandler TimetableFileParsed = delegate { };

        static ParserWorker()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void StartParse()
        {
            //var webPageParser = new WebPageParser();
            //var result = webPageParser.GetLinksOfTimetables();
            //ConsoleHelper.WriteOk("WebParser OK");

            //result.Links = result.Links.Take(1).ToList();

            //var downloader = new FileDownloader();
            //downloader.DownloadFilesInParallel(result.Links, ParseFileOfTimetable);

            string fileName =
                @"C:\dev\Xamarin\NsauTimetable\NsauT.Web.Checker\bin\Debug\netcoreapp3.1\TimetablesFiles\1101_10_time_18_04_2020_12_38_05_496.xls";

            ParseFileOfTimetable(fileName);
        }

        private void ParseFileOfTimetable(string fileName)
        {
            ConsoleHelper.Write("File downloaded: " + fileName);

            var timetableFileParser = new TimetableFileParser();
            List<TimetableInfo> parsedTimetables = timetableFileParser.ParseExcelFile(fileName);

            var modelConverter = new BusinessModelMapper();
            List<TimetableModel> modelTimetables = modelConverter.Map(parsedTimetables);

            var args = new ParserEventArgs()
            {
                Timetables = modelTimetables
            };

            TimetableFileParsed.Invoke(this, args);
        }
    }
}
