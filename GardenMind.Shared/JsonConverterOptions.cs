using System.Text.Json;

namespace GardenMind.Shared
{
    public class JsonConverterOptions
    {
        public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
        {
            MaxDepth = 1,
            PreferredObjectCreationHandling = System.Text.Json.Serialization.JsonObjectCreationHandling.Populate
        };
    }
}