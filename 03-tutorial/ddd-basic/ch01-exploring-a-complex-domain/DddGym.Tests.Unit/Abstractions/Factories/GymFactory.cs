using DddGym.Domain.Gyms;
using DddGym.Tests.Unit.Abstractions.Constants;

namespace DddGym.Tests.Unit.Abstractions.Factories;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxRooms = DomainConstants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            maxRooms,
            //subscriptionId: Constants.Subscriptions.Id,
            id: id ?? DomainConstants.Gym.Id);
    }
}