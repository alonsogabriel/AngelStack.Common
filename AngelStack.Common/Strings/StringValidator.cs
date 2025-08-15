using System.Text.RegularExpressions;
using AngelStack.Common.Guards;

namespace AngelStack.Common.Strings;

public enum StringValidationError
{
    Required,
    TooShort,
    TooLong,
    InvalidFormat,
}

public class StringValidator
{
    private readonly Regex? _regex;

    public StringValidator(StringValidationOptions options)
    {
        Options = options.Guard();

        if (options.Pattern != null)
        {
            _regex = new(options.Pattern, RegexOptions.Compiled);
        }
    }
    public StringValidationOptions Options { get; }

    public virtual bool Validate(string? value, out IEnumerable<StringValidationError> errors)
    {
        List<StringValidationError> errorList = [];

        if (Options.Required && string.IsNullOrWhiteSpace(value))
        {
            errorList.Add(StringValidationError.Required);
        }

        if (string.IsNullOrEmpty(value) == false)
        {
            if (value.Length < Options.MinLength)
            {
                errorList.Add(StringValidationError.TooShort);
            }

            if (value.Length > Options.MaxLength)
            {
                errorList.Add(StringValidationError.TooLong);
            }

            if (_regex is not null && _regex.IsMatch(value) == false)
            {
                errorList.Add(StringValidationError.InvalidFormat);
            }
        }

        errors = errorList;
        return errorList.Count == 0;
    }
}