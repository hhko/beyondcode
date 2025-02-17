using DddGym.Domain.Abstractions.BaseTypes;
using DddGym.Domain.Abstractions.ValueObjects;
using DddGym.Domain.AggregateRoots.Participants;
using ErrorOr;
using static DddGym.Domain.AggregateRoots.Sessions.Errors.DomainErrors;

namespace DddGym.Domain.AggregateRoots.Sessions;

public sealed class Session : AggregateRoot
{
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = [];
    private readonly int _maxParticipants;

    public DateOnly Date { get; }

    public TimeRange Time { get; }

    public Session(
        DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Date = date;
        Time = time;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Contains(participant.Id))
        {
            //return Error.Conflict(description: "Participant already exists in session");
            return Error.Conflict(description: "Participants cannot reserve twice to the same session");
        }

        // 규칙
        //  세션은 최대 참가자 수를 초과할 수 없다.
        //  A session cannot contain more than the maximum number of participants
        if (_participantIds.Count >= _maxParticipants)
        {
            return ReserveSpotErrors.CannotHaveMoreReservationsThanParticipants;
        }

        _participantIds.Add(participant.Id);

        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider dateTimeProvider)
    {
        // 규칙
        //  세션 시작 24시간 이내에는 무료로 예약을 취소할 수 없다.
        //  A reservation cannot be canceled for free less than 24 hours before the session starts
        if (IsTooCloseToSession(dateTimeProvider.UtcNow))
        {
            return CancelReservationErrors.CannotCancelReservationTooCloseToSession;
        }

        if (!_participantIds.Remove(participant.Id))
        {
            return Error.NotFound(description: "Participant not found");
        }

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int MinHours = 24;

        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHours;
    }
}