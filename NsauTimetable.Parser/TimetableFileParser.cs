using ExcelDataReader;
using NsauTimetable.Parser.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace NsauTimetable.Parser
{
    public class TimetableFileParser
    {
        public void ParseExcelFile(string fileName)
        {
            ConsoleHelper.HorizontalLine();

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports: Binary Excel files (2.0-2003 format; *.xls), OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    int[] skipThisRows = { 1, 3, 4, 5 };
                    int sectionWidthSubject = 4;

                    int amountOfPeriodsByDay = 6; // width of day section
                    int amountOfDays = 6;

                    int columnOfSubjectInfo = 1;

                    do
                    {
                        ConsoleHelper.WriteOk(reader.Name);
                        int rowCounter = 0;
                        while (reader.Read())
                        {
                            rowCounter++;
                            
                            // Skip some rows
                            if (skipThisRows.Contains(rowCounter))
                            {
                                continue;
                            }

                            if (rowCounter == 2)
                            {
                                List<string> groupNumbers = GetGroupNumbers(reader);
                                ConsoleHelper.WriteOk(string.Join(", ", groupNumbers.ToArray()));
                                continue;
                            }

                            int currentDay = 1;
                            int currentPeriod = 1;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i < sectionWidthSubject)
                                {
                                    if (i != columnOfSubjectInfo)
                                    {
                                        continue;
                                    }

                                    // Get subject and time
                                    string value = GetStringValue(reader, i);
                                    ConsoleHelper.WriteOk(value);
                                    continue;
                                }

                                if (currentDay <= amountOfDays)
                                {
                                    if (currentPeriod <= amountOfPeriodsByDay)
                                    {
                                        currentPeriod++;
                                        // TODO:
                                    }
                                    else
                                    {
                                        currentDay++;
                                    }
                                }
                                
                                // TODO: Get teachers
                                // Here code

                                Console.WriteLine($"{i}: {GetStringValue(reader, i)}");
                            }
                        }
                    } while (reader.NextResult());
                }
            }

            ConsoleHelper.HorizontalLine();
        }

        private string GetStringValue(IExcelDataReader reader, int field)
        {
            object columnValue = reader.GetValue(field);

            if (columnValue is string stringValue)
            {
                return stringValue;
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

            Match match = Regex.Match(str, groupNumbersPattern);

            Group regexGroup = match.Groups[regexLabel];
            List<string> groupNumbersOfStudents = regexGroup.Value.Trim().Replace(" ", "").Split(',').ToList();

            return groupNumbersOfStudents;
        }
    }
}
