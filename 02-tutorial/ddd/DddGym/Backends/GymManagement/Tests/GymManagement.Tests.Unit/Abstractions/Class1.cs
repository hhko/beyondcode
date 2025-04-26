using FunctionalDdd.Framework.BaseTypes;
using LanguageExt;
using LanguageExt.Common;

namespace GymManagement.Tests.Unit.Abstractions;

public static class FinFluent
{
    public static void ShouldBeErrorCode<TSuccess>(
        this Fin<TSuccess> fin,
        string errorCode)
    {
        fin.Match(
            Succ: _ => throw new ShouldAssertException("Expected failure, but operation was successful."),
            Fail: _ =>
            {
                if (_ is ManyErrors)
                {
                    _.AsIterable()
                        .Any(error => ((ExpectedErrorCode)error).ErrorCode == errorCode)
                        .ShouldBeTrue($"Expected error code '{errorCode}' was not found in the error list.");
                }
                else
                {
                    ((ExpectedErrorCode)_).ErrorCode.ShouldBe(errorCode);
                }
            });
    }

    public static void ShouldBeErrorCodes<TSuccess>(
        this Fin<TSuccess> fin,
        params string[] expectedErrorCodes)
    {
        fin.Match(
            Succ: _ => throw new ShouldAssertException("Expected failure, but operation was successful."),
            Fail: errors =>
            {
                if (errors is not ManyErrors manyErrors)
                    throw new ShouldAssertException("Expected ManyErrors, but got a single error.");

                var actualErrorCodes = manyErrors
                    .AsIterable()
                    .OfType<ExpectedErrorCode>()
                    .Select(e => e.ErrorCode)
                    .ToList();

                foreach (var expectedErrorCode in expectedErrorCodes)
                {
                    actualErrorCodes.ShouldContain(
                        expectedErrorCode,
                        $"Expected error code '{expectedErrorCode}' was not found in the actual error list.\nActual: [{string.Join(", ", actualErrorCodes)}]");
                }
            });
    }
}
