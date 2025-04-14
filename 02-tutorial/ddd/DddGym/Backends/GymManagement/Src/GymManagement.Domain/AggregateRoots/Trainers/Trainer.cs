using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using LanguageExt.Common;
using static GymManagement.Domain.AggregateRoots.Trainers.Errors.DomainErrors;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.AggregateRoots.Trainers;

// Trainer
//  [ ] Create: 생성자
//  [ ] Create: Factory 패턴 + Error 연동
// _sessionIds
//  [x] Add
//  [ ] Can
//  [ ] Has
//  [x] Remove
//  [ ] Get_1
//  [ ] Get_N
public sealed class Trainer : AggregateRoot
{
    private readonly List<Guid> _sessionIds = [];
    private readonly SharedTypes.Schedule _schedule = SharedTypes.Schedule.Empty();

    // ---------------------

    // 변경: private readonly Guid _userId;
    public Guid UserId { get; }

    // ---------------------

    private Trainer(
        Guid userId,
        SharedTypes.Schedule? schedule,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? SharedTypes.Schedule.Empty();
    }

    public static Trainer Create(
        Guid userId,
        SharedTypes.Schedule? schedule = null,
        Guid? id = null)
    {
        return new Trainer(userId, schedule, id);
    }

    // TODO: 존재 이유 ???
    private Trainer()
    {
    }

    // 기술적 메서드 이름           도메인적인 메서드 이름(도메인 행동과 의도를 표현)
    //  AddSessionToSchedule        ScheduleSession
    //  RemoveFromSchedule          UnscheduleSession
    //
    //  EnsureSessionExist          EnsureSessionScheduled
    //  EnsureSessionNotExist       EnsureSessionNotScheduled

    // ScheduleSession: 세션을 스케줄에 배정한다.
    // -> UnscheduleSession: 세션을 스케줄에서 해제한다.
    // -> CancelScheduledSession: 세션을 스케줄에서 취소한다.

    //public Fin<Unit> AddSessionToSchedule(Session session)
    // 세션을 스케줄에 배정하는 도메인 행동
    public Fin<Unit> ScheduleSession(Session session)
    {
        // =========================================
        // Monadic LINQ 스타일
        // =========================================

        // 규칙
        //  - 트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
        //    A trainer cannot teach two or more overlapping sessions
        return from _1 in EnsureSessionNotScheduled(session.Id)
               from _2 in _schedule.BookTimeSlot(session.Date, session.Time)
                    .MapFail(error => 
                        error.Combine(
                            TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions(
                                session.Date, 
                                session.Time)))
               from _3 in RegisterSession(session.Id)
               select unit;

        // =========================================
        // Case 1: Imperative Guard 스타일
        // =========================================

        //if (_sessionIds.Contains(session.Id))
        //{
        //    return Error.New("Session already exists in trainer's schedule");
        //}
        //
        // 규칙
        //  트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
        //  A trainer cannot teach two or more overlapping sessions
        //var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        //if (bookTimeSlotResult.IsFail)
        //{
        //    //return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
        //    //    ? AddSessionToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions
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
            : TrainerErrors.SessionAlreadyScheduled(sessionId);

    private Fin<Unit> RegisterSession(Guid sessionId)
    {
        _sessionIds.Add(sessionId);
        return unit;
    }

    // 추가
    //public Fin<Unit> RemoveFromSchedule(Session session)
    public Fin<Unit> UnscheduleSession(Session session)
    {
        // =========================================
        // Case 3. Monadic LINQ 스타일
        // =========================================

        return from _1 in EnsureSessionScheduled(session.Id)
               from _2 in _schedule.UnbookTimeSlot(session.Date, session.Time)
               from _3 in UnregisterSession(session.Id)
               select unit;

        // =========================================
        // Case 2: Monadic 스타일
        // =========================================

        //return EnsureSessionExists(session.Id)
        //    .Bind(_ => _schedule.UnbookTimeSlot(session.Date, session.Time))
        //    .Map(_ => RemoveSessionId(session.Id));

        // =========================================
        // Case 1: Imperative Guard 스타일
        // =========================================

        //if (!_sessionIds.Contains(session.Id))
        //{
        //    return TrainerErrors.SessionNotFound;
        //}
        //
        //var unbookTimeSlotResult = _schedule.UnbookTimeSlot(session.Date, session.Time);
        //if (unbookTimeSlotResult.IsFail)
        //{
        //    return (Error)unbookTimeSlotResult;
        //}
        //
        //_sessionIds.Remove(session.Id);
        //
        //return unit;
    }

    private Fin<Unit> EnsureSessionScheduled(Guid sessionId) =>
        _sessionIds.Contains(sessionId)
            ? unit
            : TrainerErrors.SessionNotScheduled(sessionId);

    private Fin<Unit> UnregisterSession(Guid sessionId)
    {
        _sessionIds.Remove(sessionId);
        return unit;
    }

    // 추가
    public bool IsTimeSlotFree(DateOnly date, TimeRange timeRange)
    {
        return _schedule.CanBookTimeSlot(date, timeRange);
    }
}

