using GymManagement.Adapters.Persistence.Repositories;
using GymManagement.Domain.AggregateRoots.Admins;
using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Domain.AggregateRoots.Users;
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