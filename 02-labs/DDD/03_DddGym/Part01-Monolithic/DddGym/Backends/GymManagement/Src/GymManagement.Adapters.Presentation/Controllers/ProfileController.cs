using GymDdd.Framework.WebApi.Utilities;
using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Profiles.Commands;
using GymManagement.Application.Usecases.Profiles.Queries;
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
    public async Task<Results<Ok<GetProfileQuery.Response>, ProblemHttpResult>> GetProfiles(Guid userId)
    {
        GetProfileQuery.Request query = new(userId);
        Fin<GetProfileQuery.Response> response = await Sender.Send(query);
        return response.ToResult();
    }

    [HttpPost("admin")]
    public async Task<Results<Ok<CreateAdminProfileCommand.Response>, ProblemHttpResult>> CreateAdminProfile(Guid userId)
    {
        CreateAdminProfileCommand.Request command = new(userId);
        Fin<CreateAdminProfileCommand.Response> response = await Sender.Send(command);
        return response.ToResult();
    }

    [HttpPost("trainer")]
    public async Task<Results<Ok<CreateTrainerProfileCommand.Response>, ProblemHttpResult>> CreateTrainerProfile(Guid userId)
    {
        Fin<CreateTrainerProfileCommand.Response> response = await Sender.Send(new CreateTrainerProfileCommand.Request(userId));
        return response.ToResult();
    }

    [HttpPost("participant")]
    public async Task<Results<Ok<CreateParticipantProfileCommand.Response>, ProblemHttpResult>> CreateParticipantProfile(Guid userId)
    {
        Fin<CreateParticipantProfileCommand.Response> response = await Sender.Send(new CreateParticipantProfileCommand.Request(userId));
        return response.ToResult();
    }
}
