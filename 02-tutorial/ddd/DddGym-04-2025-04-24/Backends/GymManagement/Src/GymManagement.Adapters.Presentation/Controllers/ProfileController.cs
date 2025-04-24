using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfile;
using GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;
using GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfile;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DddGym.Framework.BaseTypes;
namespace GymManagement.Adapters.Presentation.Controllers;

public static class Xyz
{
    // IResult
    public static Ok<TValue> ToOkResult<TValue>(this Fin<TValue> result)
    {
        return result switch
        {
            { IsSucc: true } => TypedResults.Ok((TValue)result),
            _ => throw new InvalidOperationException("Result was failed")
        };
    }

    // IResult

    public static ProblemHttpResult ToProblemHttpResult<TValue>(this Fin<TValue> result)
    {
        return result switch
        {
            { IsSucc: true } => throw new InvalidOperationException("Result was successful"),

            Error error => TypedResults.Problem
            (
                ProblemDetailsUtilities.CreateProblemDetails
                (
                    "제목",
                    StatusCodes.Status400BadRequest,
                    error
                )
            ),

            //_ => TypedResults.Problem
            //(
            //    CreateProblemDetails
            //    (
            //        InvalidRequest,
            //        StatusCodes.Status400BadRequest,
            //        result.Error
            //    )
            //)
        };
    }
}

public static class ProblemDetailsUtilities
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
            //Extensions = { { nameof(errors), errors } }
        };

        return problemDetails;
    }
}

[Route("users/{userId:guid}/profiles")]
public sealed class ProfileController : ApiController
{
    public ProfileController(ISender sender)
        : base(sender)
    {
    }

    //[HttpPost("[action]")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<Results<Ok<GetProfileResponse>, ProblemHttpResult>> GetProfiles(Guid userId)
    {
        Fin<GetProfileResponse> response = await Sender.Send(new GetProfileQuery(userId));

        return response.Match<Results<Ok<GetProfileResponse>, ProblemHttpResult>>(
            Succ: _ => Xyz.ToOkResult(response),
            Fail: _ => Xyz.ToProblemHttpResult(response));
    }

    [HttpPost("admin")]
    public async Task<IActionResult> CreateAdminProfile(Guid userId)
    {
        // TODO: Json 구조 개선
        // [                        <- 불 필요
        //   {                      <- 불 필요
        //     "adminId": [
        //       "ae5ec89d-7c3a-46b3-bbb8-5c29acc08e17"
        //     ]
        //   }
        // ]
        // TODO: Json 실패 값
        //
        // TODO: 실패 처리(컴파일러 에러)
        // 
        //return from response in await Sender.Send(new CreateAdminProfileCommand(userId))
        //       select Ok(response);

        Fin<CreateAdminProfileResponse> response = await Sender.Send(new CreateAdminProfileCommand(userId));
        //return Ok(response);
        if (response.IsSucc)
            return Ok((CreateAdminProfileResponse)response);
        else
            return BadRequest((Error)response);

        //var x = Pure(userId)
        //    .Map(id => new CreateAdminProfileCommand(id))
        //    .Bind(command => liftIO(() => Sender.Send(command)))
        //    .Map(Ok);

        //var x = from command in Pure(new CreateAdminProfileCommand(userId))
        //        from response in liftIO(() => Sender.Send(command))
        //        select OK(response);

        //return await x.RunAsync();
    }

    [HttpPost("trainer")]
    public async Task<IActionResult> CreateTrainerProfile(Guid userId)
    {
        Fin<CreateTrainerProfileResponse> response = await Sender.Send(new CreateTrainerProfileCommand(userId));
        //return Ok(response);
        if (response.IsSucc)
            return Ok((CreateTrainerProfileResponse)response);
        else
            return BadRequest((Error)response);
    }

    [HttpPost("participant")]
    public async Task<IActionResult> CreateParticipantProfile(Guid userId)
    {
        Fin<CreateParticipantProfileResponse> response = await Sender.Send(new CreateParticipantProfileCommand(userId));
        //return Ok(response);
        if (response.IsSucc)
            return Ok((CreateParticipantProfileResponse)response);
        else
            return BadRequest((Error)response);
    }
}
