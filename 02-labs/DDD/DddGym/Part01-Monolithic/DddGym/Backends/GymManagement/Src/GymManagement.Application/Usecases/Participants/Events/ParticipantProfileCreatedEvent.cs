using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Participants;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Participants.Events;

public static class ParticipantProfileCreatedEvent
{
    internal sealed class Usecase(IParticipantsRepository participantsRepository)
        : IDomainEventUsecase<UserEvents.ParticipantProfileCreatedEvent>
    {
        private readonly IParticipantsRepository _participantsRepository = participantsRepository;

        public async Task Handle(UserEvents.ParticipantProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            var usecase = from newParticipant in Participant.Create(userId: domainEvent.UserId, id: domainEvent.ParticipantId)
                          from _ in _participantsRepository.AddParticipantAsync(newParticipant)
                          select unit;

            await usecase
                .Run()
                .RunAsync();
        }
    }
}
