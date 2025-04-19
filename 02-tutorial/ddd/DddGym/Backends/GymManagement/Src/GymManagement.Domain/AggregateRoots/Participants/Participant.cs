using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using static GymManagement.Domain.AggregateRoots.Participants.Errors.DomainErrors;
using static LanguageExt.Prelude;

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
    private readonly SharedTypes.Schedule _schedule = SharedTypes.Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    public Guid UserId { get; }

    // 추가
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    private Participant(
        Guid userId,
        SharedTypes.Schedule? schedule,
        Guid? id) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? SharedTypes.Schedule.Empty();
    }

    public static Participant Create(
        Guid userId,
        SharedTypes.Schedule? schedule = null,
        Guid? id = null)
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


        // =========================================
        // Monadic 스타일
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

    private Fin<Unit> EnsureSessionNotFound(Guid sessionId) =>
        _sessionIds.Contains(sessionId)
            ? ParticipantErrors.SessionAlreadyExist(Id, sessionId)
            : unit;

    private Fin<Unit> ApplySessionAddition(Guid sessionId)
    {
        _sessionIds.Add(sessionId);
        return unit;
    }

    // 추가
    //public Fin<Unit> RemoveFromSchedule(Session session)
    public Fin<Unit> UnscheduleSession(Session session)
    {
        return from _1 in EnsureSessionAlreadyExist(session.Id)
               from _2 in _schedule.UnbookTimeSlot(session.Date, session.TimeSlot)
               from _3 in ApplySessionRemoval(session.Id)
               select unit;


        // =========================================
        // Monadic 스타일
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

    private Fin<Unit> EnsureSessionAlreadyExist(Guid sessionId) =>
        !_sessionIds.Contains(sessionId)
            ? ParticipantErrors.SessionNotFound(Id, sessionId)
            : unit;

    private Fin<Unit> ApplySessionRemoval(Guid sessionId)
    {
        _sessionIds.Remove(sessionId);
        return unit;
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
