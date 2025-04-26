using FunctionalDdd.Framework.WebApi.Utilities;
using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Profiles.Commands.CreateAdminProfiles;
using GymManagement.Application.Usecases.Profiles.Commands.CreateParticipantProfiles;
using GymManagement.Application.Usecases.Profiles.Commands.CreateTrainerProfiles;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfiles;
using LanguageExt;
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
    public async Task<Results<Ok<GetProfile.Response>, ProblemHttpResult>> GetProfiles(Guid userId)
    {
        GetProfile.Query query = new(userId);
        Fin<GetProfile.Response> response = await Sender.Send(query);
        return response.ToResult();
    }

    [HttpPost("admin")]
    public async Task<Results<Ok<CreateAdminProfile.Response>, ProblemHttpResult>> CreateAdminProfile(Guid userId)
    {
        CreateAdminProfile.Command command = new(userId);
        Fin<CreateAdminProfile.Response> response = await Sender.Send(command);
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
    public async Task<Results<Ok<CreateTrainerProfile.Response>, ProblemHttpResult>> CreateTrainerProfile(Guid userId)
    {
        Fin<CreateTrainerProfile.Response> response = await Sender.Send(new CreateTrainerProfile.Command(userId));
        return response.ToResult();
    }

    [HttpPost("participant")]
    public async Task<Results<Ok<CreateParticipantProfile.Response>, ProblemHttpResult>> CreateParticipantProfile(Guid userId)
    {
        Fin<CreateParticipantProfile.Response> response = await Sender.Send(new CreateParticipantProfile.Command(userId));
        return response.ToResult();
    }
}
