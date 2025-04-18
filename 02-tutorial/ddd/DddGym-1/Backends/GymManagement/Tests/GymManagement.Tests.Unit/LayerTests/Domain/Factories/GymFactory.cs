using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class GymFactory
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