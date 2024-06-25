using System.Text.Json;
using System.Text.Json.Serialization;
using TechAssess.ScrapingService.Models;

namespace TechAssess.ScrapingService.Converters
{
    public class ScrapeDataConverter : JsonConverter<ScrapeData>
    {
        public override void Write(Utf8JsonWriter writer, ScrapeData value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            writer.WriteStartObject();
            foreach (var prop in type.GetProperties())
            {
                writer.WritePropertyName(JsonNamingPolicy.CamelCase.ConvertName(prop.Name));
                JsonSerializer.Serialize(writer, prop.GetValue(value), prop.PropertyType, options);
            }
            writer.WriteEndObject();
        }

        public override ScrapeData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not supported.");
        }
    }
}
