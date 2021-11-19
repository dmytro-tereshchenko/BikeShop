using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BikeShop.WebUI.Infrastructure
{
    public static class SessionExtensions
    {
        public static JsonSerializerOptions Options { get; set; }
        static SessionExtensions()
        {
            Options = new JsonSerializerOptions();
            Options.MaxDepth = 10;
            Options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        }
        public static void Set<T>(this ISession session, string key, T value)
        {
            try
            {
                session.SetString(key, JsonSerializer.Serialize<T>(value, Options));
            }
            catch(JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public static T Get<T>(this ISession session, string key)
        {
            try
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonSerializer.Deserialize<T>(value, Options);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
                return default(T);
            }
        }
    }
}
