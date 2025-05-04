namespace GymManagement.Application.Usecases.Participants.Events.SessionCanceled;

public static class SessionCanceledEvent
{
    // TODO: LanguageExt
    //internal sealed class Usecase
    //    : IDomainEventUsecase<SessionCanceledEvent>
    //{
    //    private readonly IParticipantsRepository _participantsRepository;

    //    public Usecase(IParticipantsRepository participantsRepository)
    //    {
    //        _participantsRepository = participantsRepository;
    //    }

    //    public async Task Handle(SessionCanceledEvent domainEvent, CancellationToken cancellationToken)
    //    {
    //        List<Participant> participants = await _participantsRepository.ListByIds(domainEvent.Session.GetParticipantIds());

    //        participants.ForEach(participant =>
    //        {
    //            var removeFromScheduleResult = participant.RemoveFromSchedule(domainEvent.Session);
    //            if (removeFromScheduleResult.IsError)
    //            {
    //                throw new DomainEventException(
    //                    SessionCanceledEventErrors.ParticipantScheduleUpdateFailed,
    //                    removeFromScheduleResult.Errors);
    //            }
    //        });

    //        await _participantsRepository.UpdateRangeAsync(participants);
    //    }
    //}
}
