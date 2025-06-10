using GymManagement.Adapters.Infrastructure.Authentication.PasswordHasher;
using GymManagement.Adapters.Infrastructure.Authentication.TokenGenerator;
using GymManagement.Application.Abstractions.TokenGenerator;
using GymManagement.Application.Usecases.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Infrastructure.Abstractions.Registrations;

internal static class AuthenticationRegistration
{
    internal static IServiceCollection RegisterAuthentication(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
