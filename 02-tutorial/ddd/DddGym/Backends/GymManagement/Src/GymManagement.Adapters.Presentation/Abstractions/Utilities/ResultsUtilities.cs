using LanguageExt;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Adapters.Presentation.Abstractions.Utilities;

internal static class ResultsUtilities
{
    public static Results<Ok<TValue>, ProblemHttpResult> ToResult<TValue>(this Fin<TValue> result)
    {
        return result.Match<Results<Ok<TValue>, ProblemHttpResult>>(
            Succ: _ => result.ToSucc(),
            Fail: _ => result.ToFail());
    }

    private static Ok<TValue> ToSucc<TValue>(this Fin<TValue> result)
    {
        return result switch
        {
            { IsSucc: true } => TypedResults.Ok((TValue)result),
            _ => throw new InvalidOperationException("Result was failed")
        };
    }

    private static ProblemHttpResult ToFail<TValue>(this Fin<TValue> result)
    {
        return result.Match(
            Succ: _ => throw new InvalidOperationException("Result was successful"),
            Fail: error =>
            {
                return TypedResults.Problem(
                    ProblemDetailsUtilities.CreateProblemDetails(
                        typeof(TValue).FullName ?? typeof(TValue).Name,
                        StatusCodes.Status400BadRequest,
                        error
                    )
                );
            });

        //return result switch
        //{
        //    { IsSucc: true } => throw new InvalidOperationException("Result was successful"),

        //    Error error => TypedResults.Problem
        //    (
        //        ProblemDetailsUtilities.CreateProblemDetails
        //        (
        //            "제목",
        //            StatusCodes.Status400BadRequest,
        //            error
        //        )
        //    ),

        //    //_ => TypedResults.Problem
        //    //(
        //    //    CreateProblemDetails
        //    //    (
        //    //        InvalidRequest,
        //    //        StatusCodes.Status400BadRequest,
        //    //        result.Error
        //    //    )
        //    //)
        //};
    }
}