using DddGym.Domain.Rooms;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public class RoomFactory
{
    public static Room CreateRoom(
        int maxDailySessions = DomainConstants.Room.MaxDailySessions,
        Guid? gymId = null,
        Guid? id = null)
    {
        return new Room(
            maxDailySessions: maxDailySessions,
            gymId: gymId ?? DomainConstants.Gym.Id,
            id: id ?? DomainConstants.Room.Id);
    }
}
