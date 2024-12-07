using Crop.Hello.Framework.Contracts.Errors;

namespace Crop.Hello.Framework.Contracts.Results;

// TODO
// - 값이 있을 때도, public static ValidationResult WithErrors(ICollection<Error> validationErrors) 메서드가 필요하지 않을까?
// - 값이 있을 때와 값이 없을 때 모두 재사용 필요: KeepErrorsLenghtNotTooLongForSecurityReasons

public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private const int MaximalErrorsLength = 40;

    private ValidationResult(Error[] validationErrors)
        : base(default, Error.ValidationError)
    {
        ValidationErrors = KeepErrorsLenghtNotTooLongForSecurityReasons(validationErrors);
    }

    private ValidationResult(TValue? value)
        : base(value, Error.None)
    {
        ValidationErrors = [];
    }

    public Error[] ValidationErrors { get; }

    public static ValidationResult<TValue> WithErrors(params Error[] validationErrors)
    {
        return new(validationErrors);
    }

    // public static ValidationResult WithErrors(ICollection<Error> validationErrors)

    public static ValidationResult<TValue> WithoutErrors(TValue? value)
    {
        return new(value);
    }

    private static Error[] KeepErrorsLenghtNotTooLongForSecurityReasons(Error[] validationErrors)
    {
        return validationErrors.Length > MaximalErrorsLength
            ? validationErrors.Take(MaximalErrorsLength).ToArray()
            : validationErrors;
    }
}

public sealed class ValidationResult : Result, IValidationResult
{
    private const int MaximalErrorsLength = 40;

    private static readonly ValidationResult _successValidationResult = new();

    private ValidationResult(Error[] validationErrors)
        : base(Error.ValidationError)
    {
        //ValidationErrors = validationErrors;
        ValidationErrors = KeepErrorsLenghtNotTooLongForSecurityReasons(validationErrors);
    }

    private ValidationResult()
        : base(Error.None)
    {
        ValidationErrors = [];
    }

    public Error[] ValidationErrors { get; }

    public static ValidationResult WithErrors(params Error[] validationErrors)
    {
        return new([.. validationErrors]);
    }

    public static ValidationResult WithErrors(ICollection<Error> validationErrors)
    {
        return new([.. validationErrors]);
    }

    public static ValidationResult WithoutErrors()
    {
        return _successValidationResult;
    }

    // 통합?
    private static Error[] KeepErrorsLenghtNotTooLongForSecurityReasons(Error[] validationErrors)
    {
        return validationErrors.Length > MaximalErrorsLength
            ? validationErrors.Take(MaximalErrorsLength).ToArray()
            : validationErrors;
    }
}
