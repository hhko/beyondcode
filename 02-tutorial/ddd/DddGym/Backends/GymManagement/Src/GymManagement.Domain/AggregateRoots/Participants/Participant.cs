using DddGym.Framework.BaseTypes;
//
using GymManagement.Domain.Abstractions.Entities;
using GymManagement.Domain.Abstractions.ValueObjects;
using GymManagement.Domain.AggregateRoots.Sessions;
using LanguageExt;
using LanguageExt.Common;
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
    private readonly Abstractions.Entities.Schedule _schedule = Abstractions.Entities.Schedule.Empty();
    private readonly List<Guid> _sessionIds = [];

    // ---------------------

    // 변경: private readonly Guid _userId;
    public Guid UserId { get; }

    // 추가
    public IReadOnlyList<Guid> SessionIds => _sessionIds;

    // ---------------------

    public Participant(
        Guid userId,
        Abstractions.Entities.Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? Abstractions.Entities.Schedule.Empty();
    }

    // TODO: 존재 이유 ???
    private Participant()
    {
    }

    //public Fin<Unit> AddToSchedule(Session session)
    public Fin<Unit> AddToSchedule(Session session)
    {
        // 규칙 생략: Id 중복
        if (_sessionIds.Contains(session.Id))
        {
            //return Error.New("Session already exists in participant's schedule");
            return Error.New("Session already exists in participant's schedule");
        }

        // 규칙
        //  참가자는 겹치는 세션을 예약할 수 없다.
        //  A participant cannot reserve overlapping sessions
        var bookTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTimeSlotResult.IsFail)
        {
            //return bookTimeSlotResult.FirstError.Type == ErrorType.Conflict
            //    ? AddToScheduleErrors.CannotHaveTwoOrMoreOverlappingSessions
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
            //return Error.New( "Session not found");
            return Error.New("Session not found");
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