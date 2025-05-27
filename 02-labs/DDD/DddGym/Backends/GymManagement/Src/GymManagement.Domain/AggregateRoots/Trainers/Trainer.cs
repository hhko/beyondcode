using GymDdd.Framework.BaseTypes;
using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Trainers.Errors.DomainErrors;

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
    private readonly Abstractions.SharedTypes.Schedule _schedule = Abstractions.SharedTypes.Schedule.Empty();

    public Guid UserId { get; }

    private Trainer(
        Guid userId,
        Option<Abstractions.SharedTypes.Schedule> schedule,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        UserId = userId;
        _schedule = schedule.IfNone(Abstractions.SharedTypes.Schedule.Empty());
    }

    public static Trainer Create(
        Guid userId,
        Option<Abstractions.SharedTypes.Schedule> schedule = default,
        Option<Guid> id = default)
    {
        return new Trainer(userId, schedule, id);
    }

    private Trainer()
    {
    }

    public Fin<Unit> ScheduleSession(Session session)
    {
        // 규칙
        //  - 트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
        //    A trainer cannot teach two or more overlapping sessions
        return from _1 in EnsureSessionNotFound(session.Id)
               from _2 in _schedule
                            .BookTimeSlot(session.Date, session.TimeSlot)
                            .CombineErrors(TrainerErrors.SessionNotScheduled())
               from _3 in ApplySessionAddition(session.Id)
               select unit;

        Fin<Unit> EnsureSessionNotFound(Guid sessionId) =>
            _sessionIds.Contains(sessionId)
                ? TrainerErrors.SessionAlreadyExist(Id, sessionId)
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

    public Fin<Unit> UnscheduleSession(Session session)
    {
        return from _1 in EnsureSessionAlreadyExist(session.Id)
               from _2 in _schedule
                            .UnbookTimeSlot(session.Date, session.TimeSlot)
                            .CombineErrors(TrainerErrors.SessionNotFound(Id, session.Id))
               from _3 in ApplySessionRemoval(session.Id)
               select unit;

        Fin<Unit> EnsureSessionAlreadyExist(Guid sessionId) =>
            !_sessionIds.Contains(sessionId)
                ? TrainerErrors.SessionNotFound(Id, sessionId)
                : unit;

        Fin<Unit> ApplySessionRemoval(Guid sessionId)
        {
            _sessionIds.Remove(sessionId);
            return unit;
        }

        // =========================================
        // Monad 스타일
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

    public bool IsTimeSlotFree(DateOnly date, TimeSlot timeSlot)
    {
        return _schedule.CanBookTimeSlot(date, timeSlot);
    }
}

