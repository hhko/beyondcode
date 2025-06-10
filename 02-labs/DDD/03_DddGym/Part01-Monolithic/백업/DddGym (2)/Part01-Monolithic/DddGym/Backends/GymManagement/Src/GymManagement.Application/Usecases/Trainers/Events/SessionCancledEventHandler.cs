using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Sessions.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Trainers.Events;

public static class SessionCanceledEventHandler
{
    internal sealed class Usecase(ITrainersRepository trainersRepository)
        : IDomainEventUsecase<SessionEvents.SessionCanceledEvent>
    {
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        public async Task Handle(SessionEvents.SessionCanceledEvent domainEvent, CancellationToken cancellationToken)
        {
            var usecase = from trainer in _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
                          from _1 in trainer.UnscheduleSession(domainEvent.Session)
                          from _2 in _trainersRepository.UpdateAsync(trainer)
                          select unit;

            await usecase
                .Run()
                .RunAsync();
        }
    }
}