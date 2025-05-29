using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace GymDdd.Framework.BaseTypes.Errors;

public static class ErrorCodeFactory
{
    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Error Create(string errorCode, string message) =>
        new ExpectedErrorCode(errorCode, -1000, message);

    [Pure, MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Format(params string[] parts) =>
        string.Join('.', parts);
}
