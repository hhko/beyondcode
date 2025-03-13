using GymManagement.Adapters.Presentation.Abstractions.Registrations;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

//builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

builder.Services
    .RegisterPresentation();

WebApplication webApplication = builder.Build();

webApplication.MapControllers();

webApplication.Run();