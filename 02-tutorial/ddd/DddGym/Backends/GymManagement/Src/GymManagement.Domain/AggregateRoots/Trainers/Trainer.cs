using DddGym.Framework.BaseTypes;
//
using GymManagement.Domain.Abstractions.Entities;
using GymManagement.Domain.Abstractions.ValueObjects;
using GymManagement.Domain.AggregateRoots.Sessions;
using LanguageExt;
using LanguageExt.Common;
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
    private readonly Abstractions.Entities.Schedule _schedule = Abstractions.Entities.Schedule.Empty();

    // ---------------------

    // 변경: private readonly Guid _userId;
    public Guid UserId { get; }

    // ---------------------

    private Trainer(
        Guid userId,
        Abstractions.Entities.Schedule? schedule,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? Abstractions.Entities.Schedule.Empty();
    }

    public static Trainer Create(
        Guid userId,
        Abstractions.Entities.Schedule? schedule = null,
        Guid? id = null)
    {
        return new Trainer(userId, schedule, id);
    }

    // TODO: 존재 이유 ???
    private Trainer()
    {
    }

    //public Fin<Unit> AddSessionToSchedule(Session session)
    public Fin<Unit> AddSessionToSchedule(Session session)
    {
        // 규칙 생략
        if (_sessionIds.Contains(session.Id))
        {
            //return Error.New("Session already exists in trainer's schedule");
            return Error.New("Session already exists in trainer's schedule");
        }

        // 규칙
        //  트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
        //  A trainer cannot teach two or more overlapping sessions
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsFail)
        {
            //return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
            //    ? AddSessionToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions
            //    : bookTimeSlotResult.Errors;
            return (Error)bookTimeSlotResult;
        }

        _sessionIds.Add(session.Id);

        //return Unit.Default;
        return Unit.Default;
    }

    // 추가
    //public Fin<Unit> RemoveFromSchedule(Session session)
    public Fin<Unit> RemoveFromSchedule(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
        {
            //return Error.NotFound(description: "Session not found in trainer's schedule");
            //return Error.Conflict("Trainer already assigned to teach session");
            //return Error.New( "session not found");
            return Error.New("session not found");
        }

        var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
        if (removeBookingResult.IsFail)
        {
            //return removeBookingResult.Errors;
            return (Error)removeBookingResult;
        }

        _sessionIds.Remove(session.Id);

        //return Unit.Default;
        return Unit.Default;
    }

    // 추가
    public bool IsTimeSlotFree(DateOnly date, TimeRange timeRange)
    {
        return _schedule.CanBookTimeSlot(date, timeRange);
    }
}