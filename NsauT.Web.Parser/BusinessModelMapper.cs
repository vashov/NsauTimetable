using NsauT.Shared.Models.BusinessModels;
using NsauT.Web.Parser.Models.ExcelParsedModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NsauT.Web.Parser
{
    internal class BusinessModelMapper
    {
        public List<TimetableModel> Map(List<TimetableInfo> parsedTimetables)
        {
            var timetables = new List<TimetableModel>();

            foreach (TimetableInfo parsedTimetable in parsedTimetables)
            {
                TimetableModel timetable = MapTimetable(parsedTimetable);
                timetables.Add(timetable);
            }

            return timetables;
        }

        private TimetableModel MapTimetable(TimetableInfo parsedTimetable)
        {
            var timetable = new TimetableModel();

            timetable.TimetableId = CreateTimetableId(parsedTimetable.Groups);
            timetable.SheetTitle = parsedTimetable.SheetTitle;
            timetable.Groups = parsedTimetable.Groups;
            timetable.Subjects = MapSubjects(parsedTimetable.Subjects);

            return timetable;
        }

        private string CreateTimetableId(IEnumerable<string> groups) => string.Join(",", groups);

        private List<SubjectModel> MapSubjects(List<SubjectInfo> parsedSubjects)
        {
            var subjects = new List<SubjectModel>();
            
            foreach (var parsedSubject in parsedSubjects)
            {
                SubjectModel subjectModel = MapOneSubject(parsedSubject);
                subjects.Add(subjectModel);
            }

            return subjects;
        }

        private SubjectModel MapOneSubject(SubjectInfo parsedSubject)
        {
            SubjectModel subject = new SubjectModel();

            subject.Title = parsedSubject.Title;
            subject.Teachers = parsedSubject.Teachers;

            FillStartAndEndDates(subject, isLectureDates: true, parsedSubject.FromToLecture);
            FillStartAndEndDates(subject, isLectureDates: false, parsedSubject.FromToPractice);

            subject.Days = MapSchoolDays(parsedSubject.Days);
            return subject;
        }

        private List<SchoolDayModel> MapSchoolDays(List<SchoolDayInfo> parsedDays)
        {
            var days = new List<SchoolDayModel>(); 

            foreach (SchoolDayInfo parsedDay in parsedDays)
            {
                SchoolDayModel day = MapOneSchoolDay(parsedDay);
                days.Add(day);
            }

            return days;
        }

        private SchoolDayModel MapOneSchoolDay(SchoolDayInfo parsedDay)
        {
            var day = new SchoolDayModel
            {
                Day = parsedDay.Day,
                IsDayOfEvenWeek = parsedDay.IsDayOfEvenWeek
            };

            day.Periods = MapPeriods(parsedDay.Periods);

            return day;
        }

        private List<PeriodModel> MapPeriods(List<PeriodInfo> parsedPeriods)
        {
            var periods = new List<PeriodModel>();

            foreach (PeriodInfo parsedPeriod in parsedPeriods)
            {
                PeriodModel period = MapOnePeriod(parsedPeriod);
                periods.Add(period);
            }

            return periods;
        }

        private PeriodModel MapOnePeriod(PeriodInfo parsedPeriod)
        {
            var period = new PeriodModel
            {
                Number = parsedPeriod.Number,
            };

            period.Cabinet = parsedPeriod.UpperInfo;

            if (!string.IsNullOrEmpty(parsedPeriod.LowerInfo))
            {
                period.IsLecture = parsedPeriod.LowerInfo.Contains("л") || parsedPeriod.LowerInfo.Contains("Л");
            }

            if (!period.IsLecture)
            {
                period.Subgroup = parsedPeriod.LowerInfo;
            }

            // TODO: period.Modification? It is processed manually. There are no template.

            return period;
        }

        private void FillStartAndEndDates(SubjectModel subject, bool isLectureDates, string fromToDates)
        {
            bool isDatesExist = !string.IsNullOrWhiteSpace(fromToDates);
            if (!isDatesExist)
            {
                return;
            }

            const string groupStart = "start";
            const string groupEnd = "end";

            string pattern = 
                $@"с\s*(?<{groupStart}>\d{{2}}\s*\.\s*\d{{2}})\s*по\s*(?<{groupEnd}>\d{{2}}\s*\.\s*\d{{2}})";

            Match match = Regex.Match(fromToDates, pattern, RegexOptions.IgnoreCase);
            string start = match.Groups[groupStart]?.Value?.Replace(" ", "");
            string end = match.Groups[groupEnd]?.Value?.Replace(" ", "");

            if (string.IsNullOrWhiteSpace(start) || string.IsNullOrWhiteSpace(end))
            {
                return;
            }

            DateTime? startDate = GetDateWithCurrentYear(start);
            DateTime? endDate = GetDateWithCurrentYear(end);

            if (isLectureDates)
            {
                subject.LectureStartDate = startDate;
                subject.LectureEndDate = endDate;
            }
            else
            {
                subject.PracticeStartDate = startDate;
                subject.PracticeEndDate = endDate;
            }
        }

        private DateTime? GetDateWithCurrentYear(string date)
        {
            date = $"{date}.{DateTime.Now.Year}";

            if (DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime startDate))
            {
                return startDate;
            }

            // TODO: log error: something is wrong?
            return null;
        }

    }
}
