using NsauTimetable.Parser.Models.BusinessModels;
using NsauTimetable.Parser.Models.ExcelParsedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NsauTimetable.Parser
{
    public class BusinessModelConverter
    {
        public List<TimetableModel> Convert(List<TimetableInfo> parsedTimetables)
        {
            var timetables = new List<TimetableModel>();

            foreach (TimetableInfo parsedTimetable in parsedTimetables)
            {
                TimetableModel timetable = ConvertTimetable(parsedTimetable);
                timetables.Add(timetable);
            }

            return timetables;
        }

        private TimetableModel ConvertTimetable(TimetableInfo parsedTimetable)
        {
            var timetable = new TimetableModel();




            return timetable;
        }
    }
}
