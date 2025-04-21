using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Profiles.Queries.GetProfile;
using LanguageExt;
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
        var getProfilesQuery = new GetProfileQuery(userId);

        Fin<GetProfileResponse> getProfileResponse = await Sender.Send(getProfilesQuery);

        return Ok(getProfileResponse);
        //return listProfilesResult.Match(
        //    profiles => Ok(new ListProfilesResponse(
        //        profiles.AdminId,
        //        profiles.ParticipantId,
        //        profiles.TrainerId)),
        //    Problem);
    }
}
