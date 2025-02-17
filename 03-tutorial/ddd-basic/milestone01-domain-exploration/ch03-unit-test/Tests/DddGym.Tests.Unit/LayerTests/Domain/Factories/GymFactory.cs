using DddGym.Domain.AggregateRoots.Gyms;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxRooms = DomainConstants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            maxRooms,
            subscriptionId: DomainConstants.Subscription.Id,
            id: id ?? DomainConstants.Gym.Id);
    }
}