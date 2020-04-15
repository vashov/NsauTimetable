using System.Collections.Generic;

namespace NsauTimetable.Parser.Models
{
    public class WebPageParserResult
    {
        public int Count { get; set; }
        public List<HyperlinkInfo> Links { get; set; }
    }
}
