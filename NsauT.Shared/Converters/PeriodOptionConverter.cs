using NsauT.Shared.BusinessModels;
using NsauT.Shared.Enums;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NsauT.Shared.Converters
{
    public class PeriodOptionConverter : JsonConverter<PeriodOption>
    {
        public override PeriodOption Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string optionValue = reader.GetString();

            PeriodOption periodOption = (PeriodOption)Enum.Parse(typeof(PeriodOption), optionValue);
            return periodOption;
        }

        public override void Write(Utf8JsonWriter writer, PeriodOption value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
