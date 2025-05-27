using GymDdd.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Rooms;
using static GymManagement.Domain.AggregateRoots.Gyms.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Gyms.Events.DomainEvents;

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

    private readonly List<Guid> _roomIds = [];
    private readonly List<Guid> _trainerIds = [];
    private readonly int _maxRooms;

    public string Name { get; } = null!;
    public Guid SubscriptionId { get; }

    public IReadOnlyList<Guid> RoomIds => _roomIds;

    private Gym(
        string name,
        int maxRooms,
        Guid subscriptionId,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;
    }

    private Gym()
    {
    }

    public static Gym Create(
        string name,
        int maxRooms,
        Guid subscriptionId,
        Option<Guid> id = default)
    {
        return new Gym(name, maxRooms, subscriptionId, id);
    }

    public Fin<Unit> AddRoom(Room room)
    {
        return from _1 in EnsureRoomNotFound(room.Id)
               from _2 in EnsureMaxRoomsNotExceeded()
               from _3 in ApplayRoomAddition(room)
               select unit;

        Fin<Unit> EnsureRoomNotFound(Guid roomId) =>
            _roomIds.Contains(roomId)
                ? GymErrors.RoomAlreadyExist(Id, roomId)
                : unit;

        Fin<Unit> EnsureMaxRoomsNotExceeded() =>
            (_roomIds.Count >= _maxRooms)
                ? GymErrors.MaxRoomsExceeded(Id, _roomIds.Count, _maxRooms)
                : unit;

        Fin<Unit> ApplayRoomAddition(Room room)
        {
            _roomIds.Add(room.Id);

            _domainEvents.Add(new GymEvents.RoomAddedEvent(
                Name: room.Name,
                RoomId: room.Id,
                GymId: Id,
                MaxDailySessions: room.MaxDailySessions));

            return unit;
        }

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        // 규칙 생략: Id 중복
        //if (_roomIds.Contains(room.Id))
        //{
        //    return Error.New("Room already exists in gym");
        //}
        //
        // 규칙
        //  헬스장은 구독(구독 등급)이 허용하는 개수보다 더 많은 방을 가질 수 없다.
        //  A gym cannot have more rooms than the subscription allows
        //if (_roomIds.Count >= _maxRooms)
        //{
        //    return AddRoomErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        //}
        //
        //_roomIds.Add(room.Id);
        //
        //_domainEvents.Add(new RoomAddedEvent(
        //    Name: room.Name,
        //    RoomId: room.Id,
        //    GymId: Id,
        //    MaxDailySessions: room.MaxDailySessions));
        //
        //return unit;
    }

    public Fin<Unit> RemoveRoom(Guid roomId)
    {
        return from _1 in EnsureRoomAlreadyExist(roomId)
               from _2 in ApplyRoomRemoval(roomId)
               select unit;

        Fin<Unit> EnsureRoomAlreadyExist(Guid roomId) =>
            !_roomIds.Contains(roomId)
                ? GymErrors.RoomNotFound(Id, roomId)
                : unit;

        Fin<Unit> ApplyRoomRemoval(Guid roomId)
        {
            _roomIds.Remove(roomId);
            _domainEvents.Add(new GymEvents.RoomRemovedEvent(this, roomId));

            return unit;
        }

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (!_roomIds.Contains(roomId))
        //{
        //    return Error.New("Room not found");
        //}
        //
        //_roomIds.Remove(roomId);
        //
        //_domainEvents.Add(new RoomRemovedEvent(this, roomId));
        //
        //return unit;
    }

    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public Fin<Unit> AddTrainer(Guid trainerId)
    {
        return from _1 in EnsureTrainerNotFound(trainerId)
               from _2 in ApplyTrainerAddition(trainerId)
               select unit;

        Fin<Unit> EnsureTrainerNotFound(Guid trainerId) =>
            _trainerIds.Contains(trainerId)
                ? GymErrors.TrainerAlreadyExist(Id, trainerId)
                : unit;

        Fin<Unit> ApplyTrainerAddition(Guid trainerId)
        {
            _trainerIds.Add(trainerId);

            return unit;
        }

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (_trainerIds.Contains(trainerId))
        //{
        //    return Error.New("Trainer already assigned to gym");
        //}
        //
        //_trainerIds.Add(trainerId);
        //
        //return unit;
    }

    // TODO: RemoveTrainer

    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }
}