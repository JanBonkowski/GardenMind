using System.Text.Json;

namespace GardenMind.Shared
{
    public class JsonConverterOptions
    {
        public static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions
        {
            MaxDepth = 3,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            PreferredObjectCreationHandling = System.Text.Json.Serialization.JsonObjectCreationHandling.Replace
        };
    }
}