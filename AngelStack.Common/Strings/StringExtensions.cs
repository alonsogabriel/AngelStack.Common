using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AngelStack.Common.Guards;

namespace AngelStack.Common.Strings;

public static partial class StringExtensions
{
    public static string RemoveDiacritics(this string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder();

        foreach (var ch in normalized)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(ch);
            }
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static Guid ToGuid(this string value)
    {
        return Guid.Parse(value);
    }

    [GeneratedRegex(" {2,}", RegexOptions.Compiled)]
    public static partial Regex ExtraSpaceRegex();

    public static string RemoveExtraSpaces(this string value)
    {
        return ExtraSpaceRegex().Replace(value.Trim(), " ");
    }

    public static T ParseEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum
    {
        value.Guard();

        return Enum.Parse<T>(value, ignoreCase);
    }

    public static T? ParseJson<T>(this string json, JsonSerializerOptions? options = null)
    {
        options ??= new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public static StringReplacements ToReplacements(this string replacements)
    {
        return StringReplacements.FromString(replacements);
    }

    public static string ReplaceAll(this string value, StringReplacements replacements)
    {
        return replacements.Replace(value);
    }

    public static string ReplaceAll(this string value, string replacements)
    {
        return value.ReplaceAll(replacements.ToReplacements());
    }

    public static string ToSnakeCase(string value)
    {
        return string.Concat(
            value.Select((c, i) =>
                i > 0 && char.IsUpper(c) ? "_" + c : c.ToString()
            )
        ).ToLower();
    }
}