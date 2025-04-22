using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfile;
using GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfile;
using GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfile;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Adapters.Presentation.Controllers;

[Route("users/{userId:guid}/profiles")]
public sealed class ProfileController : ApiController
{
    public ProfileController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetProfiles(Guid userId)
    {
        Fin<GetProfileResponse> response = await Sender.Send(new GetProfileQuery(userId));

        if (response.IsSucc)
            return Ok((GetProfileResponse)response);
        else
            return BadRequest((Error)response);
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
