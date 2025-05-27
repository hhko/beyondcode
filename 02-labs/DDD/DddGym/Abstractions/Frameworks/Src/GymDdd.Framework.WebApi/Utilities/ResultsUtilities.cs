using GymDdd.Framework.BaseTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymDdd.Framework.WebApi.Utilities;

public static class ResultsUtilities
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
                    CreateProblemDetails(
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

    //    public static ProblemDetails CreateProblemDetails
    //    (
    //        string title,
    //        int status,
    //        Error error,
    //        Error[]? errors = null,
    //        HttpContext? context = null
    //    )
    //    {
    //        var problemDetails = new ProblemDetails()
    //        {
    //            Type = error.Code,
    //            Title = title,
    //            Detail = error.Message,
    //            Status = status,
    //            Extensions = { { nameof(errors), errors } }
    //        };

    //        if (context is not null)
    //        {
    //            problemDetails.Extensions.Add(RequestId, context.TraceIdentifier);
    //            problemDetails.Instance = context.Request.Path;
    //        }

    //        return problemDetails;
    //    }

    //    public static ProblemDetails CreateProblemDetails
    //    (
    //        string type,
    //        string title,
    //        int status,
    //        IList<string> errors
    //    )
    //    {
    //        var problemDetails = new ProblemDetails()
    //        {
    //            Type = type,
    //            Title = title,
    //            Status = status,
    //            Extensions = { { nameof(errors), errors } }
    //        };

    //        return problemDetails;
    //    }

    private static ProblemDetails CreateProblemDetails(
        //string type,
        string title,
        int status,
        Error error)
    {
        ExpectedErrorCode expectedErrorCode = (ExpectedErrorCode)error;

        var problemDetails = new ProblemDetails()
        {
            Type = expectedErrorCode.ErrorCode,
            Title = title,
            Status = status,
            Detail = expectedErrorCode.Message,
            //Extensions = { { nameof(error), error } }
        };

        return problemDetails;
    }
}