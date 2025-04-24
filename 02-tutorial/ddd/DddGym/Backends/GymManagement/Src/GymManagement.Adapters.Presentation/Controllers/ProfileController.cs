using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Adapters.Presentation.Abstractions.Utilities;
using GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfile;
using GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;
using GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfile;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace GymManagement.Adapters.Presentation.Controllers;


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
        return response.ToResult();
    }

    [HttpPost("admin")]
    public async Task<Results<Ok<CreateAdminProfileResponse>, ProblemHttpResult>> CreateAdminProfile(Guid userId)
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
        return response.ToResult();

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
    public async Task<Results<Ok<CreateTrainerProfileResponse>, ProblemHttpResult>> CreateTrainerProfile(Guid userId)
    {
        Fin<CreateTrainerProfileResponse> response = await Sender.Send(new CreateTrainerProfileCommand(userId));
        return response.ToResult();
    }

    [HttpPost("participant")]
    public async Task<Results<Ok<CreateParticipantProfileResponse>, ProblemHttpResult>> CreateParticipantProfile(Guid userId)
    {
        Fin<CreateParticipantProfileResponse> response = await Sender.Send(new CreateParticipantProfileCommand(userId));
        return response.ToResult();
    }
}
