using GymDdd.Framework.WebApi.Utilities;
using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Adapters.Presentation.Controllers;

[Route("[controller]")]
[AllowAnonymous]
internal class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("register")]
    public async Task<Results<Ok<RegisterCommand.Response>, ProblemHttpResult>> Register(RegisterCommand.Request request)
    {
        Fin<RegisterCommand.Response> response = await Sender.Send(request);
        return response.ToResult();
    }
}
