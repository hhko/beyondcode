using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Participants;
using GymManagement.Domain.AggregateRoots.Sessions.Events;
using static GymManagement.Domain.AggregateRoots.Sessions.Errors.DomainEventErrors;

namespace GymManagement.Application.Usecases.Participants.Events.SessionCanceled;

internal sealed class SessionCanceledEventUsecase
    : IDomainEventUsecase<SessionCanceledEvent>
{
    private readonly IParticipantsRepository _participantsRepository;

    public SessionCanceledEventUsecase(IParticipantsRepository participantsRepository)
    {
        _participantsRepository = participantsRepository;
    }

    public async Task Handle(SessionCanceledEvent domainEvent, CancellationToken cancellationToken)
    {
        List<Participant> participants = await _participantsRepository.ListByIds(domainEvent.Session.GetParticipantIds());

        participants.ForEach(participant =>
        {
            var removeFromScheduleResult = participant.RemoveFromSchedule(domainEvent.Session);
            if (removeFromScheduleResult.IsError)
            {
                throw new DomainEventException(
                    SessionCanceledEventErrors.ParticipantScheduleUpdateFailed,
                    removeFromScheduleResult.Errors);
            }
        });

        await _participantsRepository.UpdateRangeAsync(participants);
    }
}
