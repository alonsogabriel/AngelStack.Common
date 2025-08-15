using System.Diagnostics.CodeAnalysis;

namespace AngelStack.Common.Strings;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class Required(bool value = true) : Attribute
{
    public bool Value { get; } = value;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MaxLength(int value) : Attribute
{
    public int Value { get; } = value;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MinLength(int value) : Attribute
{
    public int Value { get; } = value;
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RegularExpression([StringSyntax(StringSyntaxAttribute.Regex)] string value) : Attribute
{
    public string Value { get; } = value;
}