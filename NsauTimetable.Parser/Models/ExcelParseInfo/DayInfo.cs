using System;
using System.Collections.Generic;

namespace NsauTimetable.Parser.Models.ExcelParseInfo
{
    public class DayInfo
    {
        public DayOfWeek Day { get; set; }
        public List<PeriodInfo> Periods { get; set; }
    }
}
