using System.Diagnostics.CodeAnalysis;

namespace AngelStack.Common.Strings;

public record StringValidationOptions(
    bool Required = true,
    int MinLength = 0,
    int? MaxLength = null,
    [StringSyntax(StringSyntaxAttribute.Regex)] string? Pattern = null);