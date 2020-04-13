using System;
using System.Collections.Generic;

namespace NsauTimetable.Client.Models
{
    public class TimetableByDay
    {
        public DayOfWeek Day { get; set; }
        public string DayTitle { get; set; }
        public List<Period> Timetable { get; set; }
    }
}
