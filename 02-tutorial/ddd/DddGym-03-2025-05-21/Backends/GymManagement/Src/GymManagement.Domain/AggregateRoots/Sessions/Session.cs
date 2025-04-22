using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;
using GymManagement.Domain.AggregateRoots.Sessions.Events;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using static GymManagement.Domain.AggregateRoots.Sessions.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Sessions.Events.DomainEvents.SessionEvents;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.AggregateRoots.Sessions;

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

    public TimeSlot TimeSlot { get; }

    public Guid TrainerId { get; }

    // TODO: List<Guid> _reservationIds = [];
    //  왜????
    //  Id가 아닌 객체로 참조할까???
    private readonly List<Reservation> _reservations = [];

    public int NumParticipants => _reservations.Count;

    private readonly List<SessionCategory> _categories = [];

    public IReadOnlyList<SessionCategory> Categories => _categories;

    public Guid RoomId { get; }

    public int MaxParticipants { get; }

    public string Name { get; }

    public string Description { get; }

    private Session(
        string name,
        string description,
        int maxParticipants,

        Guid roomId,
        Guid trainerId,

        DateOnly date,
        TimeSlot timeSlot,

        List<SessionCategory> categories,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        Name = name;
        Description = description;
        MaxParticipants = maxParticipants;

        RoomId = roomId;
        TrainerId = trainerId;

        Date = date;
        TimeSlot = timeSlot;

        _categories = categories;
    }

    private Session()
    {
    }

    public static Session Create(
        string name,
        string description,
        int maxParticipants,

        Guid roomId,
        Guid trainerId,

        DateOnly date,
        TimeSlot timeSlot,

        List<SessionCategory> categories,
        Option<Guid> id = default)
    {
        return new Session(name, description, maxParticipants, roomId, trainerId, date, timeSlot, categories, id);
    }

    //public Fin<Unit> ReserveSpot(Participant participant)
    //{
    //    if (_participantIds.Contains(participant.Id))
    //    {
    //        //return Error.New("Participant already exists in session");
    //        return Error.New("Participants cannot reserve twice to the same session");
    //    }

    //    // 규칙
    //    //  세션은 최대 참가자 수를 초과할 수 없다.
    //    //  A session cannot contain more than the maximum number of participants
    //    if (_participantIds.Count >= _maxParticipants)
    //    {
    //        return ReserveSpotErrors.CannotHaveMoreReservationsThanParticipants;
    //    }

    //    _participantIds.Add(participant.Id);

    //    return Unit.Default;
    //}

    public Fin<Unit> ReserveSpot(Participant participant)
    {
        return from _1 in EnsureParticipantNotFounc(participant.Id)
               from _2 in EnsureMaxParticipantsNotExceeded()
               from _3 in ApplyParticipantAddition(participant.Id)
               select unit;

        Fin<Unit> EnsureParticipantNotFounc(Guid participantId) =>
            _reservations.Any(reservation => reservation.Id == participantId)
                ? SessionErrors.ParticipantAlreadyExist(Id, participantId)
                : unit;

        Fin<Unit> EnsureMaxParticipantsNotExceeded() =>
            _reservations.Count >= MaxParticipants
                ? SessionErrors.MaxParticipantsExceeded(Id, _reservations.Count, MaxParticipants)
                : unit;

        Fin<Unit> ApplyParticipantAddition(Guid participantId)
        {
            var reservation = Reservation.Create(participantId);
            _reservations.Add(reservation);
            _domainEvents.Add(new SessionSpotReservedEvent(this, reservation));

            return unit;
        }

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        // 규칙
        //  세션은 최대 참가자 수를 초과할 수 없다.
        //  A session cannot contain more than the maximum number of participants
        //if (_reservations.Count >= MaxParticipants)
        //{
        //    return ReserveSpotErrors.CannotHaveMoreReservationsThanParticipants;
        //}
        //
        //if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
        //{
        //    //return Error.New("Participant already exists in session");
        //    return Error.New("Participants cannot reserve twice to the same session");
        //}
        //
        //var reservation = Reservation.Create(participant.Id);
        //_reservations.Add(reservation);
        //
        //_domainEvents.Add(new SessionSpotReservedEvent(this, reservation));
        //
        //return Unit.Default;
    }

    // TODO?: 취소하기 위해서는 participantId가 sessionId로 변경해야 하지 않을까?
    //      - 하나의 Participant가 여러 Session을 예약할 수 있기 때문에
    //      - 예. 같은 날에 다른 Session 여러개?
    public Fin<Unit> CancelReservation(Guid participantId, DateTime utcNow)
    {
        return from _1 in EnsureParticipantAlreadyExist(participantId)
               from _2 in EnsureReservationInFuture(utcNow)
               from _3 in EnsureReservationNotTooClose(utcNow)
               from _4 in ApplyReservationRemoval(participantId)
               select unit;

        Fin<Unit> EnsureParticipantAlreadyExist(Guid participantId) =>
            !_reservations.Any(reservation => reservation.ParticipantId == participantId)
                ? SessionErrors.ParticipantNotFound(Id, participantId)
                : unit;

        Fin<Unit> EnsureReservationInFuture(DateTime utcNow) =>
            (Date.ToDateTime(TimeSlot.End) - utcNow).TotalHours < 0
                ? SessionErrors.ReservationInPast(Id, Date.ToDateTime(TimeSlot.End), utcNow)
                : unit;

        Fin<Unit> EnsureReservationNotTooClose(DateTime utcNow)
        {
            const int MinHours = 24;
            return (Date.ToDateTime(TimeSlot.Start) - utcNow).TotalHours < MinHours
                ? SessionErrors.ReservationTooClose(Id, Date.ToDateTime(TimeSlot.End), utcNow)
                : unit;
        }

        Fin<Unit> ApplyReservationRemoval(Guid participantId)
        {
            Reservation reservation = _reservations.First(reservation => reservation.ParticipantId == participantId);
            _reservations.Remove(reservation);
            _domainEvents.Add(new ReservationCanceledEvent(this, reservation));

            return unit;
        }

        // 규칙
        //  세션 시작 24시간 이내에는 무료로 예약을 취소할 수 없다.
        //  A reservation cannot be canceled for free less than 24 hours before the session starts
        //if (!_reservations.Any(reservation => reservation.ParticipantId == participantId))
        //{
        //    return CancelReservationErrors.ReservationNotFound;
        //}
        //
        // 숨겨진 규칙
        //  지난 예약은 취소할 수 없다.
        //  Past reservations cannot be canceled.
        //if (IsPastSession(dateTimeProvider.UtcNow))
        //{
        //    return CancelReservationErrors.CannotCancelPastSession;
        //}
        //
        // 규칙
        //  세션 시작 24시간 이내에는 무료로 예약을 취소할 수 없다.
        //  A reservation cannot be canceled for free less than 24 hours before the session starts
        //if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        //{
        //    return CancelReservationErrors.CannotCancelReservationTooCloseToSession;
        //}
        //
        //var reservation = _reservations.First(reservation => reservation.ParticipantId == participantId);
        //_reservations.Remove(reservation);
        //
        //_domainEvents.Add(new ReservationCanceledEvent(this, reservation));
        //
        //return Unit.Default;
    }

    //private bool IsPastSession(DateTime utcNow)
    //{
    //    return (Date.ToDateTime(TimeSlot.End) - utcNow).TotalHours < 0;
    //}

    //private bool IsTooCloseToSession(DateTime utcNow)
    //{
    //    const int MinHours = 24;

    //    return (Date.ToDateTime(TimeSlot.Start) - utcNow).TotalHours < MinHours;
    //}


    // HasReservationForParticipant
    public bool HasReservationBy(Guid participantId)
    {
        return _reservations.Any(reservation => reservation.ParticipantId == participantId);
    }

    // 추가: 왜 필요할까???
    public IReadOnlyList<Guid> GetParticipantIds()
    {
        return _reservations.ConvertAll(reservation => reservation.ParticipantId);
    }

    public bool IsBetweenDates(DateTime startDateTime, DateTime endDateTime)
    {
        var sessionDateTime = Date.ToDateTime(TimeSlot.Start);

        return sessionDateTime >= startDateTime && sessionDateTime <= endDateTime;
    }

    public void Cancel()
    {
        _domainEvents.Add(new SessionCanceledEvent(this));
    }

    //public Fin<Unit> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
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
    //        return Error.New( "Participant not found");
    //    }

    //    return Unit.Default;
    //}
}