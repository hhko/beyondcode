using DddGym.Domain.Abstractions.Entities;
using DddGym.Domain.AggregateRoots.Sessions;
using ErrorOr;
using static DddGym.Domain.AggregateRoots.Participants.Errors.DomainErrors;

namespace DddGym.Domain.AggregateRoots.Participants;

public sealed class Participant
{
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = [];

    public Guid Id { get; }

    public Participant(Guid userId, Guid? id = null)
    {
        _userId = userId;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddToSchedule(Session session)
    {
        // 규칙 생략: Id 중복
        if (_sessionIds.Contains(session.Id))
        {
            return Error.Conflict(description: "Session already exists in participant's schedule");
        }

        // 규칙
        //  참가자는 겹치는 세션을 예약할 수 없다.
        //  A participant cannot reserve overlapping sessions
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsError)
        {
            return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
                ? AddToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions
                : bookTimeSlotResult.Errors;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}