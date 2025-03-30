using DddGym.Domain.Abstractions.Entities;
using DddGym.Domain.AggregateRoots.Sessions;
using ErrorOr;
using static DddGym.Domain.AggregateRoots.Trainers.Errors.DomainErrors;

namespace DddGym.Domain.AggregateRoots.Trainers;

public sealed class Trainer
{
    private readonly Guid _id;                                  // TODO: public Guid Id { get; }
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = [];
    private readonly Schedule _schedule = Schedule.Empty();

    public Trainer(
        Guid userId,
        Schedule? schedule = null,
        Guid? id = null)
    {
        _userId = userId;
        _schedule = schedule ?? Schedule.Empty();
        _id = id ?? Guid.NewGuid();
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
}