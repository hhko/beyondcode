using GymDdd.Framework.WebApi.Utilities;
using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Sessions.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace GymManagement.Adapters.Presentation.Controllers;


[Route("users/{userId:guid}/profiles")]
public sealed class SessionController : ApiController
{
    public SessionController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost()]
    public async Task<Results<Ok<CreateSessionCommand.Response>, ProblemHttpResult>> CreateSession(CreateSessionCommand.Request request, Guid roomId)
    {
        Fin<CreateSessionCommand.Response> response = await Sender.Send(new CreateSessionCommand.Request(
            roomId,
            "",
            "",
            10,
            DateTime.Now,
            DateTime.Now,
            Guid.NewGuid(),
            ["Kickboxing", "Pilates"]));
        return response.ToResult();
    }
}
