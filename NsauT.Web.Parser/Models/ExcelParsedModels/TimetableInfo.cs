using System.Collections.Generic;

namespace NsauT.Web.Parser.Models.ExcelParsedModels
{
    internal class TimetableInfo
    {
        public string SheetTitle { get; set; }
        public List<string> Groups { get; set; }
        public List<SubjectInfo> Subjects { get; set; }
    }
}
