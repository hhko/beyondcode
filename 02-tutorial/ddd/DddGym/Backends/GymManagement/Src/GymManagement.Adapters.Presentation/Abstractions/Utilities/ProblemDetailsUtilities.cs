using DddGym.Framework.BaseTypes;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Adapters.Presentation.Abstractions.Utilities;

internal static class ProblemDetailsUtilities
{
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

    public static ProblemDetails CreateProblemDetails
    (
        //string type,
        string title,
        int status,
        Error error
    )
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
