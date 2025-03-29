using GymManagement.Adapters.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GymManagement.Adapters.Presentation.Controllers;

//[ApiController]
//[Route("[controller]")]

////[ApiController]
////[Route("api/[controller]")]
////[Produces("application/json")]
//public class WeatherForecastController : ControllerBase
//{

public sealed partial class WeatherForecastController : ApiController
{
    public WeatherForecastController(ISender sender, IOptions<ExampleOptions> exampleOptions)
        : base(sender)
    {
        ExampleOptions exampleOptions1 = exampleOptions.Value;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    //public WeatherForecastController()
    //{
    //}

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
}


