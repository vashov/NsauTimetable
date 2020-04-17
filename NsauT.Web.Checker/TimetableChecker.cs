using NsauT.Shared.Converters;
using NsauT.Shared.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NsauT.Web.Checker
{
    public class TimetableChecker
    {
        public void CheckTimetables(IEnumerable<TimetableModel> timetableModels)
        {
            foreach (TimetableModel timetable in timetableModels)
            {
                CheckOneTimetable(timetable);
            }
        }

        private void CheckOneTimetable(TimetableModel timetable)
        {
            Console.WriteLine("Check: " + timetable.SheetTitle + " | Groups: " + string.Join(", ", timetable.Groups));
        }

        private string Serialize(object o)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };
            jsonOptions.Converters.Add(new DateWithoutTimeConverter());
            jsonOptions.Converters.Add(new PeriodOptionConverter());

            string json = JsonSerializer.Serialize(o, jsonOptions);

            return json;
        }
    }
}
