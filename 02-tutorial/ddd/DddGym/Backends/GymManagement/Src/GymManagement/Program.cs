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

WebApplication webApplication = builder.Build();

webApplication.MapControllers();
await webApplication.RunAsync();


