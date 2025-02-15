using DddGym.Domain.Gyms;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public static class GymFactory
{
    public static Gym CreateGym(
        string name = DomainConstants.Gym.Name,
        int maxRooms = DomainConstants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            name,
            maxRooms,
            subscriptionId: DomainConstants.Subscription.Id,
            id: id ?? DomainConstants.Gym.Id);
    }
}