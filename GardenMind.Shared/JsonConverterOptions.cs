using System.Text.Json;
using System.Text.Json.Serialization;

namespace GardenMind.Shared
{
    public class JsonConverterOptions
    {
        public static JsonSerializerOptions JsonSerializerOptions => new()
        {
            MaxDepth = 3,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
    }
}