using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SurgeonPortal.Api.Extensions
{
    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateString = reader.GetString();

            if (dateString != null)
            {
                return DateTime.Parse(dateString);
            }
            else
            {
                //TODO - Should we throw an exception instead?
                return DateTime.MaxValue;
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFZ"));
        }
    }
}