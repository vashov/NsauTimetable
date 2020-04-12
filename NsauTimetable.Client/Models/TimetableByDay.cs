using System;
using System.Collections.Generic;

namespace NsauTimetable.Client.Models
{
    public class TimetableByDay
    {
        public DayOfWeek Day { get; set; }
        public List<string> Timetable { get; set; }
    }
}
