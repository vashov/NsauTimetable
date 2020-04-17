using System.Collections.Generic;

namespace NsauTimetable.Parser.Models.ExcelParsedModels
{
    public class TimetableInfo
    {
        public string SheetTitle { get; set; }
        public List<string> Groups { get; set; }
        public List<SubjectInfo> Subjects { get; set; }
    }
}
