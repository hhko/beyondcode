using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;
using LanguageExt;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class GymFactory
{
    public static Gym CreateGym(
        string name = DomainConstants.Gym.Name,
        int maxRooms = DomainConstants.Subscription.MaxRoomsFreeTier,
        Option<Guid> id = default)
    {
        return Gym.Create(
            name,
            maxRooms,
            subscriptionId: DomainConstants.Subscription.Id,
            id: id.IfNone(DomainConstants.Gym.Id));
    }
}