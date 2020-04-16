using System.Collections.Generic;

namespace NsauTimetable.Parser.Models.ExcelParseInfo
{
    public class WeekInfo
    {
        /// <summary>
        /// Contains subject title or from-to-time
        /// </summary>
        public string SubjectSection { get; set; }
        public List<DayInfo> Days { get; set; }
        public string TeachersSection { get; set; }
    }
}
