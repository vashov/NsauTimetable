using NsauT.Shared.Converters;
using NsauT.Shared.BusinessModels;
using System.Text.Json;

namespace NsauT.Shared.Tools
{
    public class TimetableSerializer
    {
        private JsonSerializerOptions JsonOptions { get; set; }

        public TimetableSerializer()
        {
            var jsonOptions = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };
            jsonOptions.Converters.Add(new DateWithoutTimeConverter());
            jsonOptions.Converters.Add(new PeriodOptionConverter());

            JsonOptions = jsonOptions;
        }

        public string SerializeToJson(TimetableModel timetable)
        {
            string json = JsonSerializer.Serialize(timetable, JsonOptions);
            return json;
        }

        public TimetableModel DeserializeJson(string json)
        {
            TimetableModel timetable = JsonSerializer.Deserialize<TimetableModel>(json, JsonOptions);
            return timetable;
        }
    }
}
