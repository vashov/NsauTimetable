using NsauT.Web.Parser.Models;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace NsauT.Web.Parser
{
    internal class WebPageParser
    {
        private const string NsauTimetablePageUrl = "https://nsau.edu.ru/student/timetable/";
        private const string GroupLink = "link";
        private const string GroupNumber = "number";

        private readonly string TimetableUriPattern =
            $"<\\s*a href\\s*=\\s*\"(?<{GroupLink}>http://nsau.edu.ru/file/\\d+)\"\\s*>\\s*гр\\s*.?\\s*" +
            $"(?<{GroupNumber}>\\d+\\s?[-,]?\\s?\\d*)\\s*<\\s*/a\\s*>";

        public WebPageParserResult GetLinksOfTimetables()
        {
            string html = GetHtmlOfTimetable();
            WebPageParserResult result = FindTimetablesLinks(html);

            return result;
        }

        private WebPageParserResult FindTimetablesLinks(string html)
        {
            MatchCollection matches = Regex.Matches(html, TimetableUriPattern);

            var result = new WebPageParserResult
            {
                Count = matches.Count
            };

            var hyperlinks = new List<HyperlinkInfo>();

            foreach (Match match in matches)
            {
                string link = match.Groups[GroupLink].Value;
                string title = match.Groups[GroupNumber].Value;

                hyperlinks.Add(new HyperlinkInfo
                {
                    Link = link,
                    Title = title
                });
            }

            result.Links = hyperlinks;
            return result;
        }

        private string GetHtmlOfTimetable()
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.GetEncoding(1251);
                return client.DownloadString(NsauTimetablePageUrl);
            }
        }
    }
}
