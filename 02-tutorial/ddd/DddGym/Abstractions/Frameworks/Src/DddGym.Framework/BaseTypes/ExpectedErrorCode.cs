using LanguageExt.Common;
using LanguageExt;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

namespace DddGym.Framework.BaseTypes;

// TODO?: 
public static class ErrorCodeFactory
{
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Error New(string errorCode, string message) =>
        new ExpectedErrorCode(errorCode, -1000, message);
}

public static class ErrorCode
{
    // -                    -1000
    // Failure,
    // Unexpected,
    // Validation,          -1001
    // Conflict,
    // NotFound,
    // Unauthorized,
    // Forbidden,

    public static Error Validation(string errorCode, string message) =>
        new ExpectedErrorCode(errorCode, -1001, message);
}

[DataContract]
public record ExpectedErrorCode(string ErrorCode, int Code, string Message, Option<Error> Inner = default) : Error
{
    /// <summary>
    /// Error message
    /// </summary>
    [Pure]
    [DataMember]
    public override string Message { get; } =
        Message;

    /// <summary>
    /// Error code
    /// </summary>
    [Pure]
    [DataMember]
    public override int Code { get; } =
        Code;

    [Pure]
    [DataMember]
    public string ErrorCode { get; } =
        ErrorCode;


    /// <summary>
    /// Inner error
    /// </summary>
    [Pure]
    [IgnoreDataMember]
    public override Option<Error> Inner { get; } =
        Inner;

    [Pure]
    public override string ToString() =>
        Message;

    /// <summary>
    /// Generates a new `ErrorException` that contains the `Code`, `Message`, and `Inner` of this `Error`.
    /// </summary>
    [Pure]
    public override ErrorException ToErrorException() =>
        new WrappedErrorExpectedException(this);

    /// <summary>
    /// Returns false because this type isn't exceptional
    /// </summary>
    [Pure]
    public override bool HasException<E>() =>
        false;

    /// <summary>
    /// True if the error is exceptional
    /// </summary>
    [Pure]
    [IgnoreDataMember]
    public override bool IsExceptional =>
        false;

    /// <summary>
    /// True if the error is expected
    /// </summary>
    [Pure]
    [IgnoreDataMember]
    public override bool IsExpected =>
        true;
}
