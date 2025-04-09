using GymManagement.Adapters.Infrastructure.Abstractions.Registrations;
using GymManagement.Adapters.Persistence.Abstractions.Registrations;
using GymManagement.Adapters.Presentation.Abstractions.Registrations;
using GymManagement.Application.Abstractions.Registrations;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

//builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

builder.Services
    .RegisterAdapterInfrastructure()
    .RegisterAdapterPersistence()
    .RegisterAdapterPresentation()
    .RegisterApplication();

//builder.Services
//    .AddOptions<ExampleOptions>()
//    .BindConfiguration(ExampleOptions.SectionName)
//    //.Bind(builder.Configuration.GetSection(ExampleOptions.SectionName))
//    .ValidateFluently()
//    .ValidateOnStart();

WebApplication webApplication = builder.Build();

webApplication.MapControllers();

webApplication.Run();

//sealed partial class Program;





