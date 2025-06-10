using GymDdd.Framework.WebApi.Utilities;
using GymManagement.Adapters.Presentation.Abstractions;
using GymManagement.Application.Usecases.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

namespace GymManagement.Adapters.Presentation.Controllers;

//[Route("[controller]")]
[Route("api/[controller]")]
//[AllowAnonymous]
public class AuthenticationController : ApiController
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

    //[HttpPost("register")]
    //public IActionResult Register(RegisterRequest request)
    //{
    //    return Ok();
    //}

    [HttpGet]
    public IActionResult Register()
    {
        return Ok();
    }
}

//public record RegisterRequest(
//    string FirstName,
//    string LastName,
//    string Email,
//    string Password);
