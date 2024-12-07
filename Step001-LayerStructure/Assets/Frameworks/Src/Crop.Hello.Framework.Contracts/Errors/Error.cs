using Crop.Hello.Framework.Contracts.Results;

namespace Crop.Hello.Framework.Contracts.Errors;

public sealed partial record class Error(string Code, string Message)
{
    private const string SerializationSeparator = ": ";
    private const int MaximalErrorMessageLenght = 500;

    public string Code { get; } = Code;
    public string Message { get; } = KeepMessageNotTooLongForSecurityReasons(Message);

    public static readonly Error None = new(string.Empty, string.Empty);

    //
    // 생성
    //
    // - New
    // - FromException

    public static Error New(string code, string message)
    {
        return new Error(code, message);
    }

    //public static Error FromException<TException>(TException exception)
    //    where TException : Exception
    //{
    //    if (exception is AggregateException || exception.InnerException is null)
    //    {
    //        return New(exception.GetType().Name, exception.Message);
    //    }

    //    return New(exception.GetType().Name, $"{exception.Message}. ({exception.InnerException.Message})");
    //}

    public static Error FromException<TException>(TException exception)
        where TException : Exception
    {
        // exception.Message 형식 有/無
        // 1. 형식 有: One or more errors occurred. ([0] Message 값) ([1] Message 값) ...
        //    - InnerException가 없는 예외
        //    - AggregateException 예외
        // 2. 형식 無
        //    - InnerException가 있는 예외

        // exception.Message 형식 有
        // - 일반 예외(InnerException가 없는 예외)
        // - AggregateException 예외
        if (exception.InnerException is null || exception is AggregateException)
        {
            // 일반 예외(InnerException가 없는 예외)
            //  .Message: 입력 값 
            //
            // AggregateException일 때
            //  exception.InnerExceptions
            //      - [0] This was invalid operation
            //      - [1] Invalid argument
            //  exception.Message: One or more errors occurred. ([0] Message 값) ([1] Message 값)
            //      - One or more errors occurred. (This was invalid operation) (Invalid argument)
            return New(
                //$"{nameof(Exception)}.{exception.GetType().Name}",
                $"{exception.GetType().Name}",
                exception.Message);
        }

        // exception.Message 형식 無
        // - InnerException이 있는 예외
        //   exception.InnerException.Message
        //      - Invalid argument
        //   exception.Message(사용자 정의 형식): 입력 값 (InnerException Message 값)
        //      - This was invalid operation (Invalid argument)
        return New(
            //$"{nameof(Exception)}.{exception.GetType().Name}",
            $"{exception.GetType().Name}",
            $"{exception.Message}. ({exception.InnerException.Message})");
    }

    //public static Error FromException<TException>(TException exception)
    //    where TException : Exception
    //{
    //    if (exception is AggregateException || exception.InnerException is null)
    //    {
    //        return New(exception.GetType().Name, exception.Message);
    //    }

    //    return New(exception.GetType().Name, $"{exception.Message}. ({exception.InnerException.Message})");
    //}

    //
    // 타입 변환
    //
    // - string
    //   - Code
    //   - Message
    // - ValidationResult
    //   - ValidationResult
    //   - ValidationResult<TValue>
    // - Result
    //   - Result
    //   - Result<TValue>
    //

    // 암시적 string 타입 변환 시에는 Code를 반환합니다.
    public static implicit operator string(Error error)
    {
        return error.Code;
    }

    // 명시적 string 타입 변환 시에는 Message를 반환합니다.
    public override string ToString()
    {
        return Message;
    }

    public ValidationResult<TValue> ToValidationResult<TValue>()
    {
        return ValidationResult<TValue>.WithErrors(this);
    }

    public ValidationResult ToValidationResult()
    {
        return ValidationResult.WithErrors(this);
    }

    public Result<TValue> ToResult<TValue>()
    {
        return Result.Failure<TValue>(this);
    }

    public Result ToResult()
    {
        return Result.Failure(this);
    }

    //
    // 직렬화
    //

    public string Serialize()
    {
        return $"{Code}{SerializationSeparator}{Message}";
    }

    public static Error Deserialize(string serializedError)
    {
        var splitted = serializedError.Split(SerializationSeparator);
        return New(splitted[0], splitted[1]);
    }

    //
    // Helper 메서드
    //

    public void ThrowIfErrorNone()
    {
        if (this == None)
        {
            throw new InvalidOperationException("Provided error is Error.None");
        }
    }

    public string? MessageOrNullIfErrorNone()
    {
        if (this == None)
        {
            return null;
        }

        return Message;
    }

    public static string KeepMessageNotTooLongForSecurityReasons(string message)
    {
        return message.Length > MaximalErrorMessageLenght
            ? message[..MaximalErrorMessageLenght]
            : message;
    }
}
