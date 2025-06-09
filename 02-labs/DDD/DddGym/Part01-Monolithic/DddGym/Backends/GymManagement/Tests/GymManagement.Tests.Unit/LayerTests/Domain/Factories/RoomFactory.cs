using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal class RoomFactory
{
    public static Room CreateRoom(
        string name = DomainConstants.Room.Name,
        int maxDailySessions = DomainConstants.Room.MaxDailySessions,
        Option<Guid> gymId = default,
        Option<Guid> id = default)
    {
        return Room.Create(
            name,
            maxDailySessions: maxDailySessions,
            gymId: gymId.IfNone(DomainConstants.Gym.Id),
            id: id.IfNone(DomainConstants.Room.Id));
    }
}