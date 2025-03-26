using DddGym.Framework.BaseTypes.Events;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Sessions.Events;
using static GymManagement.Domain.AggregateRoots.Sessions.Errors.DomainEventErrors;

namespace GymManagement.Application.Usecases.Participants.Events.ReservationCanceled;

internal sealed class ReservationCanceledEventUsecase
    : IDomainEventUsecase<ReservationCanceledEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public ReservationCanceledEventUsecase(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(ReservationCanceledEvent domainEvent, CancellationToken cancellationToken)
    {
        //Participant? participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId);
        //participant.ThrowIfNull(_ => new EventualConsistencyException(Error.NotFound(description: "Participant not found")));

        Participant participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId)
            ?? throw new DomainEventException(ReservationCanceledEventErrors.ParticipantNotFound);

        ErrorOr<Success> removeBookingResult = participant.RemoveFromSchedule(domainEvent.Session);

        if (removeBookingResult.IsError)
        {
            throw new DomainEventException(
                ReservationCanceledEventErrors.ParticipantScheduleUpdateFailed,
                removeBookingResult.Errors);
        }

        await _participantsRepository.UpdateAsync(participant);
    }
}
