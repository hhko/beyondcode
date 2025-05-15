using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using LanguageExt;
using static GymManagement.Domain.AggregateRoots.Rooms.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Rooms.Events.DomainEvents.RoomEvents;
using static LanguageExt.Prelude;

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
    private readonly Abstractions.SharedTypes.Schedule _schedule = Abstractions.SharedTypes.Schedule.Empty();

    public Guid GymId { get; }
    public int MaxDailySessions
    {
        get { return _maxDailySessions; }
    }

    // 변경: private readonly List<Guid> _sessionIds = [];
    private readonly Dictionary<DateOnly, List<Guid>> _sessionIdsByDate = [];

    public string Name { get; }

    public IReadOnlyList<Guid> SessionIds => _sessionIdsByDate.Values
        .SelectMany(sessionIds => sessionIds)
        .ToList()
        .AsReadOnly();

    private Room(
        string name,
        int maxDailySessions,
        Guid gymId,
        Option<Abstractions.SharedTypes.Schedule> schedule,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        Name = name;
        _maxDailySessions = maxDailySessions;
        GymId = gymId;
        _schedule = schedule.IfNone(Abstractions.SharedTypes.Schedule.Empty());
    }

    private Room()
    {
    }

    public static Room Create(
        string name,
        int maxDailySessions,
        Guid gymId,
        Option<Abstractions.SharedTypes.Schedule> schedule = default,
        Option<Guid> id = default)
    {
        return new Room(name, maxDailySessions, gymId, schedule, id);
    }

    public Fin<Unit> ScheduleSession(Session session)
    {
        return from _1 in EnsureSeesionNotFound(session.Id)
               from dailySessionIds in GetOrCreateDailySessionIds(session.Date)
               from _2 in EnsureMaxSessionsNotExceeded(dailySessionIds.Count)
               from _3 in _schedule.BookTimeSlot(session.Date, session.TimeSlot)
               from _4 in ApplaySessionAddition(dailySessionIds, session)
               select unit;

        Fin<Unit> EnsureSeesionNotFound(Guid sessionId) =>
            SessionIds.Contains(sessionId)
                ? RoomErrors.SessionAlreadyExist(Id, sessionId)
                : unit;

        Fin<List<Guid>> GetOrCreateDailySessionIds(DateOnly date)
        {
            if (!_sessionIdsByDate.TryGetValue(date, out List<Guid>? dailySessionIds))
            {
                dailySessionIds = [];
                _sessionIdsByDate[date] = dailySessionIds;
            }

            return dailySessionIds;
        }

        Fin<Unit> EnsureMaxSessionsNotExceeded(int numSessions) =>
            (numSessions >= _maxDailySessions)
                ? RoomErrors.MaxRoomsExceeded(Id, numSessions, _maxDailySessions)
                : unit;

        Fin<Unit> ApplaySessionAddition(List<Guid> dailySessionIds, Session session)
        {
            dailySessionIds.Add(session.Id);
            _domainEvents.Add(new SessionScheduledEvent(Id, session));

            return unit;
        }

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //// 규칙 생략: Id 중복
        //if (SessionIds.Any(id => id == session.Id))
        //{
        //    //return Error.New("Session already exists in room");
        //    return Error.New("Session already exists in room");
        //}
        //
        //if (!_sessionIdsByDate.TryGetValue(session.Date, out List<Guid>? dailySessions))
        //{
        //    dailySessions = [];
        //    _sessionIdsByDate[session.Date] = dailySessions;
        //}
        ////if (!_sessionIdsByDate.ContainsKey(session.Date))
        ////{
        ////    _sessionIdsByDate[session.Date] = [];
        ////}
        //
        ///규칙
        //  방은 구독(구독 등급)이 허용하는 개수보다 더 많은 세션을 가질 수 없다.
        //  A room cannot have more sessions than the subscription allows
        //var dailySessions = _sessionIdsByDate[session.Date];
        //if (dailySessions.Count >= _maxDailySessions)
        //{
        //    return ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        //}
        //
        // 규칙
        //  방은 두 개 이상의 겹치는 세션을 가질 수 없다.
        //  A room cannot have two or more overlapping sessions
        //var bookTimeSlotResult = _schedule.AddTimeSlot(session.Date, session.Time);
        //if (bookTimeSlotResult.IsFail)
        //{
        //    //return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
        //    //    ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
        //    //    : bookTimeSlotResult.Errors;
        //    return (Error)bookTimeSlotResult;
        //}
        //
        //dailySessions.Add(session.Id);
        //
        //_domainEvents.Add(new SessionScheduledEvent(Id, session));
        //return unit;
    }

    // ---------------------------
    // 원본 코드: Ch06: dailySessions 有
    // ---------------------------
    //public ErrorOr<Success> ScheduleSession(Session session)
    //{
    //    if (SessionIds.Any(id => id == session.Id))
    //    {
    //        return Error.Conflict(description: "Session already exists in room");
    //    }
    //
    //    if (!_sessionIdsByDate.ContainsKey(session.Date))
    //    {
    //        _sessionIdsByDate[session.Date] = [];
    //    }
    //
    //    var dailySessions = _sessionIdsByDate[session.Date];
    //
    //    if (dailySessions.Count >= _maxDailySessions)
    //    {
    //        return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
    //    }
    //
    //    var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);
    //
    //    if (addEventResult.IsError && addEventResult.FirstError.Type == ErrorType.Conflict)
    //    {
    //        return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;
    //    }
    //
    //    dailySessions.Add(session.Id);
    //
    //    _domainEvents.Add(new SessionScheduledEvent(this, session));
    //
    //    return Result.Success;
    //}

    // ---------------------------
    // 원본 코드: Ch10: dailySessions 無
    // ---------------------------
    //public ErrorOr<Success> ScheduleSession(Session session)
    //{
    //    _sessionIds.Throw()
    //        .IfTrue(ids => ids.Any(id => id == session.Id));
    //
    //    if (_sessionIds.Count >= _maxSessions)
    //    {
    //        return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
    //    }
    //
    //    var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);
    //
    //    if (addEventResult.IsError && addEventResult.FirstError.Type == ErrorType.Conflict)
    //    {
    //        return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;
    //    }
    //
    //    _sessionIds.Add(session.Id);
    //    _domainEvents.Add(new SessionScheduledEvent(this, session));
    //
    //    return Result.Success;
    //}

    // TODO: UnscheduleSession(RemoveSession)

    public bool HasSession(Guid sessionId)
    {
        return SessionIds.Contains(sessionId);
    }
}
