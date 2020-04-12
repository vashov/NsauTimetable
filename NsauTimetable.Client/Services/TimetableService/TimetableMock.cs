using NsauTimetable.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NsauTimetable.Client.Services.TimetableService
{
    public class TimetableMock : ITimetableService
    {
        private List<TimetableByDay> _timetableByDays;
        private List<string> _timetables = new List<string>()
        {
            "Пара 1", "Пара 2", "Пара 3", "Пара 4", "Пара 5"
        };

        public TimetableMock()
        {
            _timetableByDays = new List<TimetableByDay>()
            {
                new TimetableByDay
                {
                    Day = DayOfWeek.Monday,
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Tuesday,
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Wednesday,
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Thursday,
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Friday,
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Saturday,
                    Timetable = _timetables.ToList()
                }
            };
        }

        public List<TimetableByDay> GetTimetables()
        {
            return _timetableByDays;
        }
    }
}
