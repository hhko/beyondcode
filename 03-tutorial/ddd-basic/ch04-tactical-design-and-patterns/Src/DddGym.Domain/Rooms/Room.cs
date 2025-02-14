using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.Abstractions.Entities;
using DddGym.Domain.Sessions;
using ErrorOr;
using static DddGym.Domain.Rooms.Errors.DomainErrors;

namespace DddGym.Domain.Rooms;

public class Room : AggregateRoot
{
    private readonly List<Guid> _sessionIds = [];
    private readonly int _maxDailySessions;
    private readonly Guid _gymId;
    private readonly Schedule _schedule = Schedule.Empty();

    public Room(
        int maxDailySessions,
        Guid gymId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxDailySessions = maxDailySessions;
        _gymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        // 규칙 생략: Id 중복
        if (_sessionIds.Any(id => id == session.Id))
        {
            return Error.Conflict(description: "Session already exists in room");
        }

        // 규칙
        //  방은 구독(구독 등급)이 허용하는 개수보다 더 많은 세션을 가질 수 없다.
        //  A room cannot have more sessions than the subscription allows
        if (_sessionIds.Count >= _maxDailySessions)
        {
            return ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }

        // 규칙
        //  방은 두 개 이상의 겹치는 세션을 가질 수 없다.
        //  A room cannot have two or more overlapping sessions
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsError)
        {
            return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
                ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
                : bookTimeSlotResult.Errors;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}
