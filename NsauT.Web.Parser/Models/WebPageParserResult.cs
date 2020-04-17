using System.Collections.Generic;

namespace NsauT.Web.Parser.Models
{
    public class WebPageParserResult
    {
        public int Count { get; set; }
        public List<HyperlinkInfo> Links { get; set; }
    }
}
