using DddGym.Domain.AggregateRoots.Rooms;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

internal class RoomFactory
{
    public static Room CreateRoom(
        string name = DomainConstants.Room.Name,
        int maxDailySessions = DomainConstants.Room.MaxDailySessions,
        Guid? gymId = null,
        Guid? id = null)
    {
        return new Room(
            name,
            maxDailySessions: maxDailySessions,
            gymId: gymId ?? DomainConstants.Gym.Id,
            id: id ?? DomainConstants.Room.Id);
    }
}