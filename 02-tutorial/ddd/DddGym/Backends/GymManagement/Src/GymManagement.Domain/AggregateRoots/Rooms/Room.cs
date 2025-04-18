using DddGym.Framework.BaseTypes;
//
using GymManagement.Domain.AggregateRoots.Rooms.Events;
using GymManagement.Domain.AggregateRoots.Sessions;
using LanguageExt;
using LanguageExt.Common;
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
    private readonly SharedTypes.Schedule _schedule = SharedTypes.Schedule.Empty();

    public Guid GymId { get; }
    public int MaxDailySessions
    {
        get { return _maxDailySessions; }
    }


    // 변경: private readonly List<Guid> _sessionIds = [];
    private readonly Dictionary<DateOnly, List<Guid>> _sessionIdsByDate = [];

    // 추가
    public string Name { get; }

    // 추가
    

    // ---------------------

    public IReadOnlyList<Guid> SessionIds => _sessionIdsByDate.Values
        .SelectMany(sessionIds => sessionIds)
        .ToList()
        .AsReadOnly();

    public Room(
        string name,
        int maxDailySessions,
        Guid gymId,
        SharedTypes.Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxDailySessions = maxDailySessions;
        GymId = gymId;
        _schedule = schedule ?? SharedTypes.Schedule.Empty();
    }

    // TODO: 존재 이유 ???
    private Room()
    {
    }

    //public Fin<Unit> ScheduleSession(Session session)
    //{
    //    // 규칙 생략: Id 중복
    //    if (_sessionIds.Any(id => id == session.Id))
    //    {
    //        return Error.New("Session already exists in room");
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

    //    return Unit.Default;
    //}


    //public Fin<Unit> ScheduleSession(Session session)
    public Fin<Unit> ScheduleSession(Session session)
    {
        // 규칙 생략: Id 중복
        if (SessionIds.Any(id => id == session.Id))
        {
            //return Error.New("Session already exists in room");
            return Error.New("Session already exists in room");
        }


        if (!_sessionIdsByDate.TryGetValue(session.Date, out List<Guid>? dailySessions))
        {
            dailySessions = [];
            _sessionIdsByDate[session.Date] = dailySessions;
        }
        //if (!_sessionIdsByDate.ContainsKey(session.Date))
        //{
        //    _sessionIdsByDate[session.Date] = [];
        //}

        ////규칙
        //// 방은 구독(구독 등급)이 허용하는 개수보다 더 많은 세션을 가질 수 없다.
        //// A room cannot have more sessions than the subscription allows
        //var dailySessions = _sessionIdsByDate[session.Date];
        if (dailySessions.Count >= _maxDailySessions)
        {
            return ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }

        // 규칙
        //  방은 두 개 이상의 겹치는 세션을 가질 수 없다.
        //  A room cannot have two or more overlapping sessions
        var bookTimeSlotResult = _schedule.AddTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsFail)
        {
            //return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
            //    ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
            //    : bookTimeSlotResult.Errors;
            return (Error)bookTimeSlotResult;
        }

        dailySessions.Add(session.Id);

        _domainEvents.Add(new SessionScheduledEvent(Id, session));

        //return Unit.Default;
        return Unit.Default;
        //return session;
    }

    public bool HasSession(Guid sessionId)
    {
        return SessionIds.Contains(sessionId);
    }

    // TODO: RemoveSession
}