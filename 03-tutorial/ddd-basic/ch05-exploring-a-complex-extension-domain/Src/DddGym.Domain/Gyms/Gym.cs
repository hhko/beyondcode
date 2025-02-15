using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.Rooms;
using DddGym.Domain.Trainers;
using ErrorOr;
using static DddGym.Domain.Gyms.Errors.DomainErrors;

namespace DddGym.Domain.Gyms;

public sealed class Gym : AggregateRoot
{
    // TODO: _maxTrainers
    private readonly int _maxRooms;

    private readonly List<Guid> _roomIds = [];
    private readonly List<Guid> _trainerIds = [];

    public string Name { get; } = null!;
    public IReadOnlyList<Guid> RoomIds => _roomIds;
    public Guid SubscriptionId { get; }

    public Gym(
        string name,
        int maxRooms,
        Guid subscriptionId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
    }

    // TODO: 존재 이유 ???
    private Gym()
    {

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

    public ErrorOr<Success> RemoveRoom(Room room)
    {
        if (!_roomIds.Contains(room.Id))
        {
            return Error.NotFound(description: "Room not found");
        }

        _roomIds.Remove(room.Id);

        return Result.Success;
    }

    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Success> AddTrainer(Trainer trainer)
    {
        if (_trainerIds.Contains(trainer.Id))
        {
            return Error.Conflict(description: "Trainer already assigned to gym");
        }

        _trainerIds.Add(trainer.Id);

        return Result.Success;
    }

    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    // TODO: RemoveTrainer
}