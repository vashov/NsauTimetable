using ExcelDataReader;
using NsauT.Shared.Enums;
using NsauT.Shared.Helpers;
using NsauT.Web.Parser.Models.ExcelParsedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NsauT.Web.Parser
{
    internal class TimetableFileParser
    {
        private const int SubjectSectionWidth = 4;
        private readonly int[] _skipThisRows = { 1, 3, 4, 5 };
        
        public List<TimetableInfo> ParseExcelFile(string fileName)
        {
            ConsoleHelper.HorizontalLine();

            var timetables = new List<TimetableInfo>();

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format: 
                // Binary Excel files (2.0-2003 format; *.xls), OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        if (reader.VisibleState != "visible")
                        {
                            continue;
                        }

                        var timetable = new TimetableInfo()
                        {
                            SheetTitle = reader.Name,
                            Subjects = new List<SubjectInfo>()
                        };

                        ParseHeader(reader, timetable);

                        SubjectInfo subject = null;
                        do
                        {
                            subject = ParseRowsOfSubject(reader);

                            if (subject != null)
                            {
                                timetable.Subjects.Add(subject);
                            }
                        }
                        while (subject != null);

                        timetables.Add(timetable);

                    } while (reader.NextResult());
                }
            }

            ConsoleHelper.HorizontalLine();

            return timetables;
        }

        private void ParseHeader(IExcelDataReader reader, TimetableInfo timetable)
        {
            const int headerHeight = 5;

            for (int currentRow = 1; currentRow <= headerHeight; currentRow++)
            {
                if (!reader.Read())
                {
                    return;
                }

                if (!_skipThisRows.Contains(currentRow))
                {
                    timetable.Groups = GetGroupNumbers(reader);
                }
            }
        }

        /// <summary>
        /// Return null if there are no more subjects
        /// </summary>
        private SubjectInfo ParseRowsOfSubject(IExcelDataReader reader)
        {
            var subject = new SubjectInfo()
            {
                Days = new List<SchoolDayInfo>()
            };

            const int weekCount = 2;
            bool isEvenWeek = false;

            for (int week = 1; week <= weekCount; week++, isEvenWeek = !isEvenWeek)
            {
                WeekInfo weekInfo = ParseWeek(reader, isEvenWeek);
                if (weekInfo == null)
                {
                    return null;
                }

                if (isEvenWeek)
                {
                    string fromToShared = weekInfo.SubjectSection;
                    FillFromTo(subject, fromToShared);
                }
                else
                {
                    subject.Title = weekInfo.SubjectSection;
                    subject.Teachers = weekInfo.TeachersSection;
                }

                weekInfo.Days.ForEach(d => d.IsDayOfEvenWeek = isEvenWeek);
                subject.Days.AddRange(weekInfo.Days);
            }

            return subject;
        }

        private void FillFromTo(SubjectInfo subject, string fromToShared)
        {
            string pattern = "\\s*\\.*\\s*с\\s*\\d*\\.\\d*\\s*по\\s*\\d*\\.\\d*";
            string patternLecture = "л" + pattern;
            string patternPractice = "пр" + pattern;
            Match lecture = Regex.Match(fromToShared, patternLecture, RegexOptions.IgnoreCase);
            Match practice = Regex.Match(fromToShared, patternPractice, RegexOptions.IgnoreCase);

            subject.FromToLecture = lecture.Value;
            subject.FromToPractice = practice.Value;
        }

        /// <summary>
        /// Return null if there are no more subjects
        /// </summary>
        private WeekInfo ParseWeek(IExcelDataReader reader, bool isEvenWeek)
        {
            bool isUpperRowOfWeek = true;
            const int rowCount = 2;

            WeekInfo weekInfo = null;

            for (int weekRow = 1; weekRow <= rowCount; weekRow++, isUpperRowOfWeek = !isUpperRowOfWeek)
            {
                WeekInfo weekRowInfo = ParseWeekRow(reader, isEvenWeek, isUpperRowOfWeek);
                if (weekRowInfo == null)
                {
                    return null;
                }

                if (weekInfo == null) // if isUpperRowOfWeek
                {
                    weekInfo = weekRowInfo;
                }
                else
                {
                    foreach (SchoolDayInfo day in weekInfo.Days)
                    {
                        foreach (PeriodInfo period in day.Periods)
                        {
                            PeriodInfo periodInfo = weekRowInfo
                                .Days.First(lowDay => lowDay.Day == day.Day)
                                .Periods.First(lowPeriod => lowPeriod.Number == period.Number);

                            period.LowerInfo = periodInfo.LowerInfo;
                        }
                    }
                }
            }

            DeleteEmptyPeriodsAndEmptyDays(weekInfo);

            // if empty rows were readed
            if (!weekInfo.Days.Any())
            {
                return null;
            }

            return weekInfo;
        }

        private void DeleteEmptyPeriodsAndEmptyDays(WeekInfo week)
        {
            // clean periods
            week.Days.ForEach(day => day.Periods.RemoveAll(period
                => string.IsNullOrWhiteSpace(period.UpperInfo) && string.IsNullOrWhiteSpace(period.LowerInfo)));

            // clean days
            week.Days.RemoveAll(day => !day.Periods.Any());
        }

        /// <summary>
        /// Return null if there are no more subjects
        /// </summary>
        private WeekInfo ParseWeekRow(IExcelDataReader reader, bool isEvenWeek, bool isUpperRowOfWeek)
        {
            if (!reader.Read())
            {
                return null;
            }

            var weekRowInfo = new WeekInfo()
            {
                Days = new List<SchoolDayInfo>()
            };

            int subjectTitleColumn = 1;
            if (isUpperRowOfWeek)
            {
                // Get subject info: title / from-to-time
                weekRowInfo.SubjectSection = GetStringValue(reader, subjectTitleColumn);
            }

            for (DayOfWeek day = DayOfWeek.Monday; DayHelper.IsSchoolDay(day); day++)
            {
                List<PeriodInfo> periods = ParseSchoolDay(reader, day, isUpperRowOfWeek);

                weekRowInfo.Days.Add(new SchoolDayInfo
                {
                    Day = day,
                    Periods = periods
                });
            }

            int teachersColumn = GetTeachersSectionColumn();

            // Get teachers
            if (!isEvenWeek && isUpperRowOfWeek)
            {
                string teachers = GetStringValue(reader, teachersColumn);
                weekRowInfo.TeachersSection = teachers;
                //ConsoleHelper.WriteOk("Teachers: " + teachers);
            }

            return weekRowInfo;
        }

        private List<PeriodInfo> ParseSchoolDay(IExcelDataReader reader, DayOfWeek day, bool isUpperRowOfWeek)
        {
            int dayColumnWidth = PeriodHelper.GetCountOfPeriods();
            int firstPeriodAdditionalValue = SubjectSectionWidth + (dayColumnWidth * ((int)day - 1)) - 1;

            var periodsInfo = new List<PeriodInfo>();

            for (PeriodNumber number = PeriodNumber.First; PeriodHelper.IsValidPeriod(number); number++)
            {
                int column = firstPeriodAdditionalValue + (int)number;

                string info = GetStringValue(reader, column);

                var periodInfo = new PeriodInfo()
                {
                    Number = number
                };

                if (isUpperRowOfWeek)
                {
                    periodInfo.UpperInfo = info;
                }
                else
                {
                    periodInfo.LowerInfo = info;
                }

                periodsInfo.Add(periodInfo);
                //ConsoleHelper.WriteOk($"День: {currentDay}, Пара: {currentPeriod}, " +
                //    $"Вверх: {isUpperRowOfWeek}, Четная: {isEvenWeek}," + period);
            }

            return periodsInfo;
        }

        private int GetTeachersSectionColumn()
        {
            int dayColumnWidth = PeriodHelper.GetCountOfPeriods();
            int daysSummaryWidth = dayColumnWidth * DayHelper.GetCountOfSchoolDays();
            int teachersSectionColumn = SubjectSectionWidth + daysSummaryWidth;
            return teachersSectionColumn;
        }

        private string GetStringValue(IExcelDataReader reader, int field)
        {
            object columnValue = reader.GetValue(field);

            if (columnValue is string stringValue)
            {
                return stringValue?.Trim();
            }

            if (columnValue is double doubleValue)
            {
                return doubleValue.ToString();
            }

            if (columnValue is int intValue)
            {
                return intValue.ToString();
            }

            // reader.GetValue() can return: double, int, bool, DateTime, TimeSpan, string, or null
            return string.Empty;
        }

        private List<string> GetGroupNumbers(IExcelDataReader reader)
        {
            int fieldNumber = 0;
            string str = reader.GetString(fieldNumber);

            // pattern example: " гр . 1101, 1102,1123  ,  123"
            string regexLabel = "groupsOfStudents";
            string groupNumbersPattern = $"\\s+гр\\s*\\.*\\s*(?<{regexLabel}>(\\d+)(\\s*,+\\s*\\d+)*)";

            Match match = Regex.Match(str, groupNumbersPattern, RegexOptions.IgnoreCase);

            Group regexGroup = match.Groups[regexLabel];
            List<string> groupNumbersOfStudents = 
                regexGroup.Value.Trim().Replace(" ", "").Split(',').ToList();

            return groupNumbersOfStudents;
        }
    }
}
