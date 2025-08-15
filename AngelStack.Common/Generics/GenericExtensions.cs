using System.Text.Json;

namespace AngelStack.Common.Generics;

public static class GenericExtensions
{
    public static KeyValuePair<TKey, TValue> WithKey<TKey, TValue>(this TValue value, TKey key)
    {
        return new KeyValuePair<TKey, TValue>(key, value);
    }
    public static KeyValuePair<string, TValue> WithKey<TValue>(this TValue value, string key)
    {
        return value.WithKey<string, TValue>(key);
    }

    public static string ToJson<T>(this T value, JsonSerializerOptions? options = null)
    {
        options ??= new JsonSerializerOptions
        {
            WriteIndented = false,
        };
        return JsonSerializer.Serialize(value, options);
    }
}