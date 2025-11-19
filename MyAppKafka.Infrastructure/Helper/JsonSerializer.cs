namespace MyAppKafka.Infrastructure.Helper
{
    public class JsonSerializer
    {
        public static string Serialize<T>(T obj) => System.Text.Json.JsonSerializer.Serialize(obj);
        public static T? Deserialize<T>(string json) => System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}
