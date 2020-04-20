using NsauT.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NsauT.Client.Services.TimetableService
{
    public class MockTimetable : ITimetableService
    {
        private List<string> _groups = new List<string>
        {
            "1101", "1102", "1103", "1104", "1105", "1106", "1107", "1108", "1109", "1110",
            "1201", "1202", "1203", "1204", "1205", "1206", "1207", "1208", "1209", "1210",
            "1301", "1302", "1303", "1304", "1305", "1306", "1307", "1308", "1309", "1310",
            "1401", "1402", "1403", "1404", "1405", "1406", "1407", "1408", "1409", "1410",
        };

        private List<TimetableByDay> _timetableByDays;

        private List<Period> _timetables = new List<Period>()
        {
            new Period
            {
                Title = "Ботаника",
                Teachers = new List<string> { "ПАЛЬЧИКОВА Е.В." },
                Cabinet = "Д-129",
                Interval = new PeriodInterval
                {
                    PeriodNumber = 1,
                    Start = new TimeSpan(9, 0, 0),
                    End = new TimeSpan(10, 30, 0)
                }
            },
            new Period
            {
                Title = "Химия физич.и коллоид.",
                Teachers = new List<string> { "СОЛОВЬЕВА Ю.В." },
                Cabinet = "Д-313",
                Interval = new PeriodInterval
                {
                    PeriodNumber = 2,
                    Start = new TimeSpan(10, 40, 0),
                    End = new TimeSpan(12, 10, 0)
                }
            },
            new Period
            {
                Title = "Методика опытного дела и не опытного",
                Teachers = new List<string> { "ГАЛЕЕВ Р.Р." },
                Cabinet = "Д-327",
                Interval = new PeriodInterval
                {
                    PeriodNumber = 3,
                    Start = new TimeSpan(12, 50, 0),
                    End = new TimeSpan(14, 20, 0)
                }
            },
            new Period
            {
                Title = "Методика опытного дела",
                Teachers = new List<string> { "ГАЛЕЕВ Р.Р." },
                Cabinet = "Д-226",
                Interval = new PeriodInterval
                {
                    PeriodNumber = 4,
                    Start = new TimeSpan(14, 30, 0),
                    End = new TimeSpan(16, 0, 0)
                }
            }
        };

        public MockTimetable()
        {
            _timetableByDays = new List<TimetableByDay>()
            {
                new TimetableByDay
                {
                    Day = DayOfWeek.Monday,
                    DayTitle = "ПН",
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Tuesday,
                    DayTitle = "ВТ",
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Wednesday,
                    DayTitle = "СР",
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Thursday,
                    DayTitle = "ЧТ",
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Friday,
                    DayTitle = "ПТ",
                    Timetable = _timetables.ToList()
                },
                new TimetableByDay
                {
                    Day = DayOfWeek.Saturday,
                    DayTitle = "СБ",
                    Timetable = _timetables.ToList()
                }
            };
        }

        public List<string> GetGroups()
        {
            return _groups;
        }

        public List<TimetableByDay> GetTimetables()
        {
            return _timetableByDays;
        }
    }
}
