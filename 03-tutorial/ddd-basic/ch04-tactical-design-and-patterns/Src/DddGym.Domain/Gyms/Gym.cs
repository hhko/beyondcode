using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.Rooms;
using ErrorOr;
using static DddGym.Domain.Gyms.Errors.DomainErrors;

namespace DddGym.Domain.Gyms;

public sealed class Gym : AggregateRoot
{
    private readonly Guid _subscriptionId;
    private readonly int _maxRooms;
    private readonly List<Guid> _roomIds = [];

    public Gym(
        int maxRooms,
        Guid subscriptionId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxRooms = maxRooms;
        _subscriptionId = subscriptionId;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        // 규칙 생략: Id 중복
        if (_roomIds.Contains(room.Id))
        {
            return Error.Conflict(description: "Room already exists in gym");
        }

        // 규칙
        //  헬스장은 구독(구독 등급)이 허용하는 개수보다 더 많은 방을 가질 수 없다.
        //  A gym cannot have more rooms than the subscription allows
        if (_roomIds.Count >= _maxRooms)
        {
            return AddRoomErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        }

        _roomIds.Add(room.Id);

        return Result.Success;
    }
}