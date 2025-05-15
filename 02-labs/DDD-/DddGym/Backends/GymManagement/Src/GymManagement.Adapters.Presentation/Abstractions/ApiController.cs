using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Adapters.Presentation.Abstractions;

[ApiController]
//[Route("api/[controller]")]
[Produces("application/json")]
public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}