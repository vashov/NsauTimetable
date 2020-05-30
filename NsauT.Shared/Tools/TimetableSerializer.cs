using NsauT.Shared.Converters;
using NsauT.Shared.BusinessModels;
using System.Text.Json;

namespace NsauT.Shared.Tools
{
    public class TimetableSerializer
    {
        private static JsonSerializerOptions JsonOptions { get; }

        static TimetableSerializer()
        {
            var jsonOptions = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };
            jsonOptions.Converters.Add(new DateWithoutTimeConverter());
            jsonOptions.Converters.Add(new PeriodOptionConverter());

            JsonOptions = jsonOptions;
        }

        public static string SerializeToJson<T>(T model)
        {
            string json = JsonSerializer.Serialize(model, JsonOptions);
            return json;
        }

        public static T DeserializeJson<T>(string json)
        {
            T model = JsonSerializer.Deserialize<T>(json, JsonOptions);
            return model;
        }
    }
}
