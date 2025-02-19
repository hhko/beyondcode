using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.Abstractions.ValueObjects;
using DddGym.Domain.AggregateRoots.Participants;
using DddGym.Domain.AggregateRoots.Sessions.Enumerations;
using ErrorOr;
using static DddGym.Domain.AggregateRoots.Sessions.Errors.DomainErrors;

namespace DddGym.Domain.AggregateRoots.Sessions;

// Session
//  [ ] Create: 생성자
//  [ ] Create: Factory 패턴 + Error 연동
// _participantIds
//  [x] Add
//  [ ] Can
//  [x] Has
//  [ ] Remove
//  [ ] Get_1
//  [ ] Get_N

// _reservations
// _categories
public sealed class Session : AggregateRoot
{
    public DateOnly Date { get; }

    public TimeRange Time { get; }

    // 제거: private readonly List<Guid> _participantIds = [];
    //  _participantIds -> _reservations

    // ---------------------

    // 변경: private readonly Guid _trainerId;
    public Guid TrainerId { get; }

    // TODO: List<Guid> _reservationIds = [];
    //  왜????
    //  Id가 아닌 객체로 참조할까???
    // 추가
    private readonly List<Reservation> _reservations = [];

    // 추가
    public int NumParticipants => _reservations.Count;

    // 추가
    private readonly List<SessionCategory> _categories = [];

    // 추가
    public IReadOnlyList<SessionCategory> Categories => _categories;

    // 추가
    public Guid RoomId { get; }

    // 변경: private readonly int _maxParticipants;
    public int MaxParticipants { get; }

    // 추가
    public string Name { get; }

    // 추가
    public string Description { get; }

    // ---------------------

    public Session(
        string name,
        string description,
        int maxParticipants,

        Guid roomId,
        Guid trainerId,

        DateOnly date,
        TimeRange time,

        List<SessionCategory> categories,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        MaxParticipants = maxParticipants;

        RoomId = roomId;
        TrainerId = trainerId;

        Date = date;
        Time = time;

        _categories = categories;
    }

    // TODO: 존재 이유 ???
    private Session()
    {
    }

    //public ErrorOr<Success> ReserveSpot(Participant participant)
    //{
    //    if (_participantIds.Contains(participant.Id))
    //    {
    //        //return Error.Conflict(description: "Participant already exists in session");
    //        return Error.Conflict(description: "Participants cannot reserve twice to the same session");
    //    }

    //    // 규칙
    //    //  세션은 최대 참가자 수를 초과할 수 없다.
    //    //  A session cannot contain more than the maximum number of participants
    //    if (_participantIds.Count >= _maxParticipants)
    //    {
    //        return ReserveSpotErrors.CannotHaveMoreReservationsThanParticipants;
    //    }

    //    _participantIds.Add(participant.Id);

    //    return Result.Success;
    //}

    // 변경
    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        // 규칙
        //  세션은 최대 참가자 수를 초과할 수 없다.
        //  A session cannot contain more than the maximum number of participants
        if (_reservations.Count >= MaxParticipants)
        {
            return ReserveSpotErrors.CannotHaveMoreReservationsThanParticipants;
        }

        if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
        {
            //return Error.Conflict(description: "Participant already exists in session");
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");
        }

        var reservation = new Reservation(participant.Id);
        _reservations.Add(reservation);

        return Result.Success;
    }

    // 추가
    public bool HasReservationForParticipant(Guid participantId)
    {
        return _reservations.Any(reservation => reservation.ParticipantId == participantId);
    }

    // 추가: 왜 필요할까???
    public List<Guid> GetParticipantIds()
    {
        return _reservations.ConvertAll(reservation => reservation.ParticipantId);
    }

    // 추가
    public bool IsBetweenDates(DateTime startDateTime, DateTime endDateTime)
    {
        var sessionDateTime = Date.ToDateTime(Time.Start);

        return sessionDateTime >= startDateTime && sessionDateTime <= endDateTime;
    }

    // 추가
    public void Cancel()
    {
    }

    //public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    //{
    //    // 규칙
    //    //  세션 시작 24시간 이내에는 무료로 예약을 취소할 수 없다.
    //    //  A reservation cannot be canceled for free less than 24 hours before the session starts
    //    if (IsTooCloseToSession(dateTimeProvider.UtcNow))
    //    {
    //        return CancelReservationErrors.CannotCancelReservationTooCloseToSession;
    //    }

    //    if (!_participantIds.Remove(participant.Id))
    //    {
    //        return Error.NotFound(description: "Participant not found");
    //    }

    //    return Result.Success;
    //}

    // 변경
    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        // 규칙
        //  세션 시작 24시간 이내에는 무료로 예약을 취소할 수 없다.
        //  A reservation cannot be canceled for free less than 24 hours before the session starts
        Reservation? reservation = _reservations.Find(reservation => reservation.ParticipantId == participant.Id);
        if (reservation is null)
        {
            return Error.NotFound(description: "Reservation not found");
        }

        // 숨겨진 규칙
        //  지난 예약은 취소할 수 없다.
        //  Past reservations cannot be canceled.
        if (IsPastSession(dateTimeProvider.UtcNow))
        {
            return CancelReservationErrors.CannotCancelPastSession;
        }

        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return CancelReservationErrors.CannotCancelReservationTooCloseToSession;
        }

        _reservations.Remove(reservation);

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int MinHours = 24;

        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHours;
    }

    // 추가
    private bool IsPastSession(DateTime utcNow)
    {
        return (Date.ToDateTime(Time.End) - utcNow).TotalHours < 0;
    }
}