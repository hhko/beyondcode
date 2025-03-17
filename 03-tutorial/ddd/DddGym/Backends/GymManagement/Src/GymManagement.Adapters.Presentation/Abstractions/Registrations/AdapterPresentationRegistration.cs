using GymManagement.Adapters.Presentation.Abstractions.JwtToken;
using GymManagement.Application.Abstractions.Tokens;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Users;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Presentation.Abstractions.Registrations;

public static class AdapterPresentationRegistration
{
    public static IServiceCollection RegisterAdapterPresentation(this IServiceCollection services)
    {
        services.RegisterControllers();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
