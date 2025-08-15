using System.Runtime.CompilerServices;

namespace AngelStack.Common.Guards;

public static class GuardExtensions
{
    /// <summary>
    /// Throws an exception if the value is null.
    /// </summary>
    /// <exception cref="Exception"></exception>"
    /// <exception cref="ArgumentNullException"></exception>"
    public static T Guard<T>(
        this T? value,
        Exception? exception = null,
        [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value is not null) return value;

        throw exception ?? new ArgumentNullException(paramName);
    }
}