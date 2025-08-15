using System.Text.RegularExpressions;
using AngelStack.Common.Guards;

namespace AngelStack.Common.Strings;

public partial class StringReplacements : Dictionary<string, string>
{
    public StringReplacements() : base() { }
    public StringReplacements(IEnumerable<KeyValuePair<string, string>> pairs) : base(pairs) { }

    /// <summary>
    /// <para>
    /// Generates a dictionary from a key-value string that uses semicolon ';' to separate pairs and equals '=' to separate
    /// keys from their respective values.
    /// </para>
    /// <para>
    /// For example: the key-value string
    /// <code>"Product=Mechanical Keyboard;Color=Blue;Rgb=True;Price=$89.99"</code>
    /// Generates the following dictionary:
    /// <code>[Product] => "Mechanical Keyboard"</code>
    /// <code>[Color] => "Blue"</code>
    /// <code>[Rgb] => "True"</code>
    /// <code>[Price] => "$89.99"</code>
    /// Useful for building message replacers in a practical way.
    /// </para>
    /// </summary>
    /// <returns>
    /// A dictionary from the input key-value string.
    /// </returns>
    public StringReplacements(string replacements)
    {
        replacements.Guard();

        var matches = KeyValueRegex().Matches(replacements);

        foreach (Match match in matches)
        {
            var key = match.Groups["key"].Value.Trim();
            var value = match.Groups["value"].Value.Trim();
            this[key] = value;
        }
    }

    [GeneratedRegex(@"(?<key>[^=;]+)\s*=\s*(?<value>[^;]*)", RegexOptions.Compiled)]
    public static partial Regex KeyValueRegex();

    public static StringReplacements FromString(string replacements) => new(replacements);

    public string Replace(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Value can not be null or empty.", nameof(value));
        }

        if (Count == 0) return value;

        var pattern = string.Join("|", Keys.OrderByDescending(k => k.Length).Select(Regex.Escape));
        var result = Regex.Replace(value, pattern, match => this[match.Value]);

        return result;
    }
}