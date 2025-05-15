using DddGym.Framework.BaseTypes;
using ErrorOr;
using GymManagement.Domain.Abstractions.Entities;
using GymManagement.Domain.Abstractions.ValueObjects;
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
    private readonly Schedule _schedule = Schedule.Empty();

    // ---------------------

    // 변경: private readonly Guid _userId;
    public Guid UserId { get; }

    // ---------------------

    public Trainer(
        Guid userId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }

    // TODO: 존재 이유 ???
    private Trainer()
    {
    }

    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        // 규칙 생략
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description: "Session already exists in trainer's schedule");
        }

        // 규칙
        //  트레이너는 두 개 이상의 겹치는 세션을 가르칠 수 없다.
        //  A trainer cannot teach two or more overlapping sessions
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsError)
        {
            return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
                ? AddSessionToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions
                : bookTimeSlotResult.Errors;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    // 추가
    public ErrorOr<Success> RemoveFromSchedule(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
        {
            //return Error.Conflict("Trainer already assigned to teach session");
            return Error.NotFound(description: "session not found");
        }

        var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
        if (removeBookingResult.IsError)
        {
            //return removeBookingResult.Errors;
            return removeBookingResult;
        }

        _sessionIds.Remove(session.Id);

        return Result.Success;
    }

    // 추가
    public bool IsTimeSlotFree(DateOnly date, TimeRange timeRange)
    {
        return _schedule.CanBookTimeSlot(date, timeRange);
    }
}