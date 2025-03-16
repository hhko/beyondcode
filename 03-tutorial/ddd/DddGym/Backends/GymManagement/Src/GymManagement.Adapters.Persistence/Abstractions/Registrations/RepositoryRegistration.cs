using GymManagement.Adapters.Persistence.Repositories;
using GymManagement.Application.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Adapters.Persistence.Abstractions.Registrations;

internal static class RepositoryRegistration
{
    internal static IServiceCollection RegisterRepository(this IServiceCollection services)
    {
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        services.AddScoped<IRoomsRepository, RoomsRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<ITrainersRepository, TrainersRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}