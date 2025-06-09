using GymDdd.Framework.BaseTypes;
using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Participants.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Participants;

// Participant
//  [ ] Create: 생성자
//  [ ] Create: Factory 패턴 + Error 연동
// _sessionIds
//  [x] Add
//  [ ] Can
//  [x] Has
//  [x] Remove
//  [ ] Get_1
//  [ ] Get_N
// _schedule
//  [x] Add
//  [x] Can
//  [ ] Has
//  [x] Remove
//  [ ] Get_1
//  [ ] Get_N

public sealed class Participant : AggregateRoot
{
    private readonly Abstractions.SharedTypes.Schedule _schedule = Abstractions.SharedTypes.Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Guid UserId { get; }

    // 추가
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    private Participant(
        Guid userId,
        Option<Abstractions.SharedTypes.Schedule> schedule,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        UserId = userId;
        _schedule = schedule.IfNone(Abstractions.SharedTypes.Schedule.Empty());
    }

    public static Fin<Participant> Create(
        Guid userId,
        Option<Abstractions.SharedTypes.Schedule> schedule = default,
        Option<Guid> id = default)
    {
        return new Participant(userId, schedule, id);
    }

    private Participant()
    {
    }

    public Fin<Unit> ScheduleSession(Session session)
    {
        return from _1 in EnsureSessionNotFound(session.Id)
               from _2 in _schedule.BookTimeSlot(session.Date, session.TimeSlot)
               from _3 in ApplySessionAddition(session.Id)
               select unit;

        Fin<Unit> EnsureSessionNotFound(Guid sessionId) =>
            _sessionIds.Contains(sessionId)
                ? ParticipantErrors.SessionAlreadyExist(Id, sessionId)
                : unit;

        Fin<Unit> ApplySessionAddition(Guid sessionId)
        {
            _sessionIds.Add(sessionId);
            return unit;
        }

        // =========================================
        // Monad 스타일
        // =========================================

        //return EnsureSessionNotScheduled(session.Id)
        //    .Bind(_ => _schedule.AddTimeSlot(session.Date, session.Time))
        //    .Bind(_ => RegisterSession(session.Id));


        // =========================================
        // Imperative Guard 스타일
        // =========================================

        // 규칙 생략: Id 중복
        //if (_sessionIds.Contains(session.Id))
        //{
        //    return Error.New("Session already exists in participant's schedule");
        //}
        //
        // 규칙
        //  참가자는 겹치는 세션을 예약할 수 없다.
        //  A participant cannot reserve overlapping sessions
        //var bookTimeSlotResult = _schedule.AddTimeSlot(session.Date, session.Time);
        //if (bookTimeSlotResult.IsFail)
        //{
        //    //return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
        //    //    ? AddToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions
        //    //    : bookTimeSlotResult.Errors;
        //    return (Error)bookTimeSlotResult;
        //}
        //
        //_sessionIds.Add(session.Id);
        //
        //return unit;
    }

    // 추가
    //public Fin<Unit> RemoveFromSchedule(Session session)
    public Fin<Unit> UnscheduleSession(Session session)
    {
        return from _1 in EnsureSessionAlreadyExist(session.Id)
               from _2 in _schedule.UnbookTimeSlot(session.Date, session.TimeSlot)
               from _3 in ApplySessionRemoval(session.Id)
               select unit;

        Fin<Unit> EnsureSessionAlreadyExist(Guid sessionId) =>
            !_sessionIds.Contains(sessionId)
                ? ParticipantErrors.SessionNotFound(Id, sessionId)
                : unit;

        Fin<Unit> ApplySessionRemoval(Guid sessionId)
        {
            _sessionIds.Remove(sessionId);
            return unit;
        }

        // =========================================
        // Monad 스타일
        // =========================================

        //return EnsureSessionScheduled(session.Id)
        //    .Bind(_ => _schedule.RemoveTimeSlot(session.Date, session.Time))
        //    .Bind(_ => UnregisterSession(session.Id));

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (!_sessionIds.Contains(session.Id))
        //{
        //    //return Error.New( "Session not found");
        //    return Error.New("Session not found");
        //}
        //
        //var removeBookingResult = _schedule.RemoveTimeSlot(session.Date, session.Time);
        //if (removeBookingResult.IsFail)
        //{
        //    //return removeBookingResult.Errors;
        //    return (Error)removeBookingResult;
        //}
        //
        //_sessionIds.Remove(session.Id);
        //
        //return unit;
    }

    public bool HasReservationForSession(Guid sessionId)
    {
        return _sessionIds.Contains(sessionId);
    }

    public bool IsTimeShotFree(DateOnly date, TimeSlot timeSlot)
    {
        return _schedule.CanBookTimeSlot(date, timeSlot);
    }
}
