namespace GymManagement.Application.Usecases.Participants.Events;

public static class ReservationCanceledEvent
{
    // TODO: LanguageExt
    //internal sealed class Usecase
    //    : IDomainEventUsecase<ReservationCanceledEvent>
    //{
    //    private readonly IParticipantsRepository _participantsRepository;

    //    public Usecase(IParticipantsRepository participantsRepository)
    //    {
    //        _participantsRepository = participantsRepository;
    //    }

    //    public async Task Handle(ReservationCanceledEvent domainEvent, CancellationToken cancellationToken)
    //    {
    //        //Participant? participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId);
    //        //participant.ThrowIfNull(_ => new EventualConsistencyException(Error.NotFound(description: "Participant not found")));

    //        Participant participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId)
    //            ?? throw new DomainEventException(ReservationCanceledEventErrors.ParticipantNotFound);

    //        //ErrorOr<Success> removeBookingResult = participant.RemoveFromSchedule(domainEvent.Session);
    //        Fin<Unit> removeBookingResult = participant.RemoveFromSchedule(domainEvent.Session);

    //        if (removeBookingResult.IsFail)
    //        {
    //            //throw new DomainEventException(
    //            //    ReservationCanceledEventErrors.ParticipantScheduleUpdateFailed,
    //            //    removeBookingResult.Errors);
    //            throw new DomainEventException(
    //                ReservationCanceledEventErrors.ParticipantScheduleUpdateFailed,
    //                (Error)removeBookingResult);
    //        }

    //        await _participantsRepository.UpdateAsync(participant);
    //    }
    //}
}
