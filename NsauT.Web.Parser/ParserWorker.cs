using NsauT.Shared.BusinessModels;
using NsauT.Shared.Helpers;
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

        public void StartParse(bool downloadOnlyFirstTimetable = false)
        {
            var webPageParser = new WebPageParser();
            var result = webPageParser.GetLinksOfTimetables();
            ConsoleHelper.WriteOk("WebParser OK");

            if (downloadOnlyFirstTimetable)
            {
                result.Links = result.Links.Take(1).ToList();
            }

            var downloader = new FileDownloader();
            downloader.DownloadFilesInParallel(result.Links, ParseFileOfTimetable);
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
