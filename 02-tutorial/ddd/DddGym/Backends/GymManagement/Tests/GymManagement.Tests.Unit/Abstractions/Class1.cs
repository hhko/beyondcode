using DddGym.Framework.BaseTypes;
using LanguageExt;

namespace GymManagement.Tests.Unit.Abstractions;

public static class FinFluent
{
    public static void ShouldBeErrorCode<TSuccess>(
        this Fin<TSuccess> @this,
        string errorCode)
    {
        @this.Match(
            Succ: ThrowIfSuccess,
            Fail: _ => ((ExpectedErrorCode)_).ErrorCode.ShouldBe(errorCode));
    }

    internal static void ThrowIfSuccess<T>(T _)
        => throw new Exception("Expected Fail, got Success instead.");
}
