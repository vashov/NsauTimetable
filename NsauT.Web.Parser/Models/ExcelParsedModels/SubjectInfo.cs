using System.Collections.Generic;

namespace NsauT.Web.Parser.Models.ExcelParsedModels
{
    internal class SubjectInfo
    {
        public string Title { get; set; }
        public string Teachers { get; set; }
        public string FromToLecture { get; set; }
        public string FromToPractice { get; set; }

        public List<SchoolDayInfo> Days { get; set; }
    }
}
