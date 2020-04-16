using System.Collections.Generic;

namespace NsauTimetable.Parser.Models
{
    public class SubjectModel
    {
        public string Title { get; set; }
        public string Teachers { get; set; }
        public string FromToLecture { get; set; }
        public string FromToPractice { get; set; }

        public List<SchoolDay> Days { get; set; }
    }
}
