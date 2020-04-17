using System.Collections.Generic;

namespace NsauTimetable.Parser.Models.ExcelParsedModels
{
    public class WeekInfo
    {
        /// <summary>
        /// Contains subject title or from-to-time
        /// </summary>
        public string SubjectSection { get; set; }
        public List<SchoolDayInfo> Days { get; set; }
        public string TeachersSection { get; set; }
    }
}
