using System.Text.Json;

namespace DigileraCommerceApp.Helpers;

public static class JsonHelper
{
    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value);
    }

    public static T Deserialize<T>(dynamic json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions);
    }
}
