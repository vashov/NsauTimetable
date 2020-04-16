using System;
using System.Collections.Generic;

namespace NsauTimetable.Parser.Helpers
{
    public static class DayHelper
    {
        private readonly static List<DayOfWeek> _schoolDays = new List<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday
        };

        public static bool IsSchoolDay(DayOfWeek day) => _schoolDays.Contains(day);

        public static int GetCountOfSchoolDays() => _schoolDays.Count;
    }
}
