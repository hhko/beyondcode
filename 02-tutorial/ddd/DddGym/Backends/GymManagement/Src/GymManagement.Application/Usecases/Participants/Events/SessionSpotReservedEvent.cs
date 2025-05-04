namespace GymManagement.Application.Usecases.Participants.Events;

public static class SessionSpotReservedEvent
{
    // TODO: LanguageExt
    //internal sealed class Usecase
    //    : IDomainEventUsecase<SessionSpotReservedEvent>
    //{
    //    private readonly IParticipantsRepository _participantsRepository;

    //    public Usecase(IParticipantsRepository participantsRepository)
    //    {
    //        _participantsRepository = participantsRepository;
    //    }

    //    public async Task Handle(SessionSpotReservedEvent domainEvent, CancellationToken cancellationToken)
    //    {
    //        var participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId);
    //        participant.ThrowIfNull();

    //        var addScheduleResult = participant.AddToSchedule(domainEvent.Session);

    //        if (addScheduleResult.IsError)
    //        {
    //            throw new DomainEventException(
    //                SessionSpotReservedEventErrors.ParticipantScheduleUpdateFailed,
    //                addScheduleResult.Errors);
    //        }

    //        await _participantsRepository.UpdateAsync(participant);
    //    }
    //}
}