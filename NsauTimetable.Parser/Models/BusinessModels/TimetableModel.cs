using System.Collections.Generic;

namespace NsauTimetable.Parser.Models.BusinessModels
{
    public class TimetableModel
    {
        public string SheetTitle { get; set; }
        public List<string> Groups { get; set; }
        public List<SubjectModel> Subjects { get; set; }
    }
}
