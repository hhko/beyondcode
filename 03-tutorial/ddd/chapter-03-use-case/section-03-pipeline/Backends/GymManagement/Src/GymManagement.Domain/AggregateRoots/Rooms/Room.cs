using DddGym.Framework.BaseTypes.Domain;
using ErrorOr;
using GymManagement.Domain.Abstractions.Entities;
using GymManagement.Domain.AggregateRoots.Rooms.Events;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Rooms.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Rooms;

// Room
//  [ ] Create: 생성자
//  [ ] Create: Factory 패턴 + Error 연동
// _sessionIdsByDate
//  [x] Add
//  [ ] Can
//  [x] Has
//  [ ] Remove
//  [ ] Get_1
//  [ ] Get_N
public sealed class Room : AggregateRoot
{
    private readonly int _maxDailySessions;
    private readonly Schedule _schedule = Schedule.Empty();

    public Guid GymId { get; }

    // ---------------------

    // 변경: private readonly List<Guid> _sessionIds = [];
    private readonly Dictionary<DateOnly, List<Guid>> _sessionIdsByDate = [];

    // 추가
    public string Name { get; }

    // 추가
    public int MaxDailySessions 
    {  
        get { return _maxDailySessions; } 
    }

    // ---------------------

    public IReadOnlyList<Guid> SessionIds => _sessionIdsByDate.Values
        .SelectMany(sessionIds => sessionIds)
        .ToList()
        .AsReadOnly();

    public Room(
        string name,
        int maxDailySessions,
        Guid gymId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxDailySessions = maxDailySessions;
        GymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
    }

    // TODO: 존재 이유 ???
    private Room()
    {
    }

    //public ErrorOr<Success> ScheduleSession(Session session)
    //{
    //    // 규칙 생략: Id 중복
    //    if (_sessionIds.Any(id => id == session.Id))
    //    {
    //        return Error.Conflict(description: "Session already exists in room");
    //    }

    //    // 규칙
    //    //  방은 구독(구독 등급)이 허용하는 개수보다 더 많은 세션을 가질 수 없다.
    //    //  A room cannot have more sessions than the subscription allows
    //    if (_sessionIds.Count >= _maxDailySessions)
    //    {
    //        return ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows;
    //    }

    //    // 규칙
    //    //  방은 두 개 이상의 겹치는 세션을 가질 수 없다.
    //    //  A room cannot have two or more overlapping sessions
    //    var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
    //    if (bookTimeSlotResult.IsError)
    //    {
    //        return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
    //            ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
    //            : bookTimeSlotResult.Errors;
    //    }

    //    _sessionIds.Add(session.Id);

    //    return Result.Success;
    //}

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        // 규칙 생략: Id 중복
        if (SessionIds.Any(id => id == session.Id))
        {
            return Error.Conflict(description: "Session already exists in room");
        }

        if (!_sessionIdsByDate.ContainsKey(session.Date))
        {
            _sessionIdsByDate[session.Date] = [];
        }

        // 규칙
        //  방은 구독(구독 등급)이 허용하는 개수보다 더 많은 세션을 가질 수 없다.
        //  A room cannot have more sessions than the subscription allows
        var dailySessions = _sessionIdsByDate[session.Date];
        if (dailySessions.Count >= _maxDailySessions)
        {
            return ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }

        // 규칙
        //  방은 두 개 이상의 겹치는 세션을 가질 수 없다.
        //  A room cannot have two or more overlapping sessions
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsError)
        {
            return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
                ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
                : bookTimeSlotResult.Errors;
        }

        dailySessions.Add(session.Id);

        _domainEvents.Add(new SessionScheduledEvent(Id, session));

        return Result.Success;
    }

    public bool HasSession(Guid sessionId)
    {
        return SessionIds.Contains(sessionId);
    }

    // TODO: RemoveSession
}