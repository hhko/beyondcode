using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.SharedTypes.ValueObjects;
using static GymManagement.Domain.AggregateRoots.Participants.Errors.DomainErrors;
using LanguageExt;
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

    // ---------------------

    // 변경: private readonly Guid _userId;
    public Guid UserId { get; }

    // 추가
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    // ---------------------

    private Participant(
        Guid userId,
        SharedTypes.Schedule? schedule,
        Guid? id) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? SharedTypes.Schedule.Empty();
    }

    // TODO: 존재 이유 ???
    private Participant()
    {
    }

    public static Participant Create(
        Guid userId,
        SharedTypes.Schedule? schedule = null,
        Guid? id = null)
    {
        return new Participant(userId, schedule, id);
    }

    //public Fin<Unit> AddToSchedule(Session session)
    public Fin<Unit> ScheduleSession(Session session)
    {
        // 규칙
        //  - 참가자는 겹치는 세션을 예약할 수 없다.
        //    A participant cannot reserve overlapping sessions
        return from _1 in EnsureSessionNotScheduled(session.Id)
               from _2 in _schedule.BookTimeSlot(session.Date, session.Time)
                    .MapFail(error =>
                        error.Combine(
                            ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions(
                                session.Date,
                                session.Time)))
               from _3 in RegisterSession(session.Id)
               select unit;

        //// 규칙 생략: Id 중복
        //if (_sessionIds.Contains(session.Id))
        //{
        //    //return Error.New("Session already exists in participant's schedule");
        //    return Error.New("Session already exists in participant's schedule");
        //}
        //
        //// 규칙
        ////  참가자는 겹치는 세션을 예약할 수 없다.
        ////  A participant cannot reserve overlapping sessions
        //var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
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

    private Fin<Unit> EnsureSessionNotScheduled(Guid sessionId) =>
        !_sessionIds.Contains(sessionId)
            ? unit
            : ParticipantErrors.SessionAlreadyScheduled(sessionId);

    private Fin<Unit> RegisterSession(Guid sessionId)
    {
        _sessionIds.Add(sessionId);
        return unit;
    }

    // 추가
    //public Fin<Unit> RemoveFromSchedule(Session session)
    public Fin<Unit> RemoveFromSchedule(Session session)
    {
        return from _1 in EnsureSessionScheduled(session.Id)
               from _2 in _schedule.UnbookTimeSlot(session.Date, session.Time)
               from _3 in UnregisterSession(session.Id)
               select unit;

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (!_sessionIds.Contains(session.Id))
        //{
        //    return Error.New("Session not found");
        //}
        //
        //var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
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

    private Fin<Unit> EnsureSessionScheduled(Guid sessionId) =>
        _sessionIds.Contains(sessionId)
            ? unit
            : ParticipantErrors.SessionNotScheduled(sessionId);

    private Fin<Unit> UnregisterSession(Guid sessionId)
    {
        _sessionIds.Remove(sessionId);
        return unit;
    }

    // 추가
    public bool HasReservationForSession(Guid sessionId)
    {
        return _sessionIds.Contains(sessionId);
    }

    // 추가
    public bool IsTimeShotFree(DateOnly date, TimeRange time)
    {
        return _schedule.CanBookTimeSlot(date, time);
    }
}