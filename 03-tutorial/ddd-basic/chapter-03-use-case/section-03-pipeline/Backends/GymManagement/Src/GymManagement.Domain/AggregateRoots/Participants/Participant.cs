using DddGym.Framework.BaseTypes.Domain;
using ErrorOr;
using GymManagement.Domain.Abstractions.Entities;
using GymManagement.Domain.Abstractions.ValueObjects;
using GymManagement.Domain.AggregateRoots.Sessions;
using static GymManagement.Domain.AggregateRoots.Participants.Errors.DomainErrors;

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
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    // ---------------------

    // 변경: private readonly Guid _userId;
    public Guid UserId { get; }

    // 추가
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    // ---------------------

    public Participant(
        Guid userId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }

    // TODO: 존재 이유 ???
    private Participant()
    {
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

    // 추가
    public ErrorOr<Success> RemoveFromSchedule(Session session)
    {
        if (!_sessionIds.Contains(session.Id))
        {
            return Error.NotFound(description: "Session not found");
        }

        var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
        if (removeBookingResult.IsError)
        {
            return removeBookingResult.Errors;
        }

        _sessionIds.Remove(session.Id);

        return Result.Success;
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