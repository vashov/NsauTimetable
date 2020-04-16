using NsauTimetable.Parser.Models.ExcelParseInfo;
using System;
using System.Collections.Generic;

namespace NsauTimetable.Parser.Models
{
    public class SchoolDay
    {
        public DayOfWeek Day { get; set; }
        public bool IsDayOfEvenWeek { get; set; }
        public List<PeriodInfo> Periods { get; set;}
    }
}
