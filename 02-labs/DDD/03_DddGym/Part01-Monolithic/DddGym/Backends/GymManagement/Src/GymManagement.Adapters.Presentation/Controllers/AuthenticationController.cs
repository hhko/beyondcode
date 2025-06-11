using GymDdd.Framework.WebApi.Utilities;
using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Authentication.Commands;
using GymManagement.Application.Usecases.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Adapters.Presentation.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    public AuthenticationController(ISender sender)
        : base(sender)
    {
    }

    //{
    //    "user": {
    //        "firstName": "Lior",
    //        "lastName": "Dagan",
    //        "email": "lior@dagan.com",
    //        "passwordHash": "$2a$11$2nDs1sL9NpfZw2/T2rD1lO43Ixw4UYUtmiq.koC19q/TnKcIHT4Je",
    //        "adminId": [],
    //        "participantId": [],
    //        "trainerId": [],
    //        "id": "bfb34367-2238-4dad-b241-1928a52a77ca"
    //    },
    //    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiTGlvciIsImVtYWlsIjoibGlvckBkYWdhbi5jb20iLCJpZCI6ImJmYjM0MzY3LTIyMzgtNGRhZC1iMjQxLTE5MjhhNTJhNzdjYSIsIm5iZiI6MTc0OTYwOTA3NywiZXhwIjoxNzQ5NjEyNjc3LCJpc3MiOiJVc2VyTWFuYWdlbWVudCIsImF1ZCI6IkRvbWVHeW0ifQ.YGBW47O1K9aOhnVf_Z4xNdjfWAhayNPDcYmWca-Fock"
    //}
    [HttpPost("register")]
    public async Task<Results<Ok<RegisterCommand.Response>, ProblemHttpResult>> Register(RegisterCommand.Request request)
    {
        Fin<RegisterCommand.Response> response = await Sender.Send(request);
        return response.ToResult();
    }

    [HttpPost("login")]
    public async Task<Results<Ok<LoginQuery.Response>, ProblemHttpResult>> Login(LoginQuery.Request request)
    {
        Fin<LoginQuery.Response> response = await Sender.Send(request);
        return response.ToResult();
    }
}
