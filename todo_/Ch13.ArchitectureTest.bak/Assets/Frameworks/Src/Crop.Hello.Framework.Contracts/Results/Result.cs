using Crop.Hello.Framework.Contracts.Errors;

namespace Crop.Hello.Framework.Contracts.Results;

// TODO
// - Result<TValue> 값으로 생성할 때 값이 NULL이면 -> InvalidOperationException 예외?
// - protected internal = protected 또는 internal
//   - 다른 어셈블리의 파생 클래스에서만 사용할 수 있도록 제한합니다.
// - private protected

public class Result<TValue> : Result, IResult<TValue>
{
    private readonly TValue? _value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException($"The value of a failure result can not be accessed. Type '{typeof(TValue).FullName}'.");

    protected internal Result(TValue? value, Error error)
        : base(error)
    {
        _value = value;
    }

    //
    // 타입 변환
    //
    // - ValidationResult<TValue>
    //
    public ValidationResult<TValue> ToValidationResult()
    {
        Error.ThrowIfErrorNone();
        return ValidationResult<TValue>.WithErrors(Error);
    }
}

public class Result : IResult
{
    private static readonly Result _success = new(Error.None);

    private protected Result(Error error)
    {
        Error = error;
    }

    public bool IsSuccess => Error == Error.None;

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    //
    // 생성
    // 
    // - 성공
    //   - 값이 없을 때: Success()
    //   - 값이 있을 때: Success<TValue>(TValue value)
    // - 실패
    //   - 값이 없을 때: Failure(Error error)
    //   - 값이 있을 때: Failure<TValue>(Error error)
    //   - 값이 있을 때: Failure<TValue>()
    //
    public static Result Success()
    {
        return _success;
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new(value, Error.None);
    }

    public static Result Failure(Error error)
    {
        error.ThrowIfErrorNone();
        return new(error);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        error.ThrowIfErrorNone();
        return new(default, error);
    }

    public Result<TValue> Failure<TValue>()
    {
        return Failure<TValue>(Error);
    }

    //public static Result<TValue> BatchFailure<TValue>(TValue value)
    //{
    //    return new(value, Error.None);
    //}

    //
    // 타입 변환
    //
    // - 실패일 때 & 값이 있을 때: ValidationResult<TValue>
    // - 실패일 때 & 값이 없을 때: ???
    //
    public ValidationResult<TValue> ToValidationResult<TValue>()
    {
        Error.ThrowIfErrorNone();
        return ValidationResult<TValue>.WithErrors(Error);
    }
}
