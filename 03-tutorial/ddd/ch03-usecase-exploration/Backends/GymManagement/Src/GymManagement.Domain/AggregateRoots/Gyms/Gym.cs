using DddGym.Framework.BaseTypes;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Gyms.Events;
using GymManagement.Domain.AggregateRoots.Rooms;
using static GymManagement.Domain.AggregateRoots.Gyms.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Gyms;

// Gym
//  [ ] Create: 생성자
//  [ ] Create: Factory 패턴 + Error 연동
// _roomIds
//  [x] Add
//  [ ] Can
//  [x] Has
//  [x] Remove
//  [ ] Get_1
//  [x] Get_N
// _trainerIds
//  [x] Add
//  [ ] Can
//  [x] Has
//  [ ] Remove
//  [ ] Get_1
//  [ ] Get_N
public sealed class Gym : AggregateRoot
{
    // TODO: _maxTrainers

    private readonly int _maxRooms;

    private readonly List<Guid> _roomIds = [];

    // ---------------------

    // 추가
    private readonly List<Guid> _trainerIds = [];

    // 추가
    public string Name { get; } = null!;

    // 변경: private readonly List<Guid> _roomIds = [];
    public IReadOnlyList<Guid> RoomIds => _roomIds;

    // 변경: private readonly Guid _subscriptionId;
    public Guid SubscriptionId { get; }

    // ---------------------

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

        //_domainEvents.Add(new RoomAddedEvent(this, room));
        _domainEvents.Add(new RoomAddedEvent(
            Name: room.Name,
            RoomId: room.Id,
            GymId: Id,
            MaxDailySessions: room.MaxDailySessions));

        return Result.Success;
    }

    // 추가
    public ErrorOr<Success> RemoveRoom(Guid roomId)
    {
        if (!_roomIds.Contains(roomId))
        {
            return Error.NotFound(description: "Room not found");
        }

        _roomIds.Remove(roomId);

        _domainEvents.Add(new RoomRemovedEvent(this, roomId));

        return Result.Success;
    }

    // 추가
    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    // 추가
    //public ErrorOr<Success> AddTrainer(Trainer trainer)
    //{
    //    if (_trainerIds.Contains(trainer.Id))
    //    {
    //        return Error.Conflict(description: "Trainer already assigned to gym");
    //    }

    //    _trainerIds.Add(trainer.Id);

    //    return Result.Success;
    //}

    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        if (_trainerIds.Contains(trainerId))
        {
            return Error.Conflict(description: "Trainer already assigned to gym");
        }

        _trainerIds.Add(trainerId);

        return Result.Success;
    }

    // 추가
    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    // TODO: RemoveTrainer
}