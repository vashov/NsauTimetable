using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NsauT.Shared.Converters
{
    public class DateWithoutTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime?));
            string date = reader.GetString();
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }

            return DateTime.Parse(date);
            //return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.Date.ToString("yyyy-MM-dd"));
            //writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
        }
    }
}
