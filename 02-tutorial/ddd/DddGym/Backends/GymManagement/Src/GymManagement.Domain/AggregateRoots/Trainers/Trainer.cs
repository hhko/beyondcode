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

    public Guid UserId { get; }

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

    private Trainer()
    {
    }

    public Fin<Unit> AddToSchedule(Session session)
    {
        // 규칙
        //  - 트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
        //    A trainer cannot teach two or more overlapping sessions
        return from _1 in EnsureSessionNotFound(session.Id)
               from _2 in _schedule.AddTimeSlot(session.Date, session.Time)
               from _3 in ApplySessionAddition(session.Id)
               select unit;

        // =========================================
        // Monadic 스타일
        // =========================================

        //return EnsureSessionNotScheduled(session.Id)
        //    .Bind(_ => _schedule.BookTimeSlot(session.Date, session.Time))
        //    .Bind(_ => RegisterSession(session.Id));

        // =========================================
        // Imperative Guard 스타일
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

    private Fin<Unit> EnsureSessionNotFound(Guid sessionId) =>
        !_sessionIds.Contains(sessionId)
            ? unit
            : TrainerErrors.SessionAlreadyExist(Id, sessionId);

    private Fin<Unit> ApplySessionAddition(Guid sessionId)
    {
        _sessionIds.Add(sessionId);
        return unit;
    }

    public Fin<Unit> RemoveFromSchedule(Session session)
    {
        return from _1 in EnsureSessionAlreadyExist(session.Id)
               from _2 in _schedule.RemoveTimeSlot(session.Date, session.Time)
               from _3 in ApplySessionRemoval(session.Id)
               select unit;


        // =========================================
        // Monadic 스타일
        // =========================================

        //return EnsureSessionExists(session.Id)
        //    .Bind(_ => _schedule.UnbookTimeSlot(session.Date, session.Time))
        //    .Map(_ => RemoveSessionId(session.Id));


        // =========================================
        // Imperative Guard 스타일
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

    private Fin<Unit> EnsureSessionAlreadyExist(Guid sessionId) =>
        _sessionIds.Contains(sessionId)
            ? unit
            : TrainerErrors.SessionNotFound(Id, sessionId);

    private Fin<Unit> ApplySessionRemoval(Guid sessionId)
    {
        _sessionIds.Remove(sessionId);
        return unit;
    }

    public bool IsTimeSlotFree(DateOnly date, TimeSlot timeRange)
    {
        return _schedule.CanBookTimeSlot(date, timeRange);
    }
}

