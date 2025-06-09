using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Rooms.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Trainers.Events;

public static class SessionScheduledEventHandler
{
    internal sealed class Usecase(ITrainersRepository trainersRepository)
        : IDomainEventUsecase<RoomEvents.SessionScheduledEvent>
    {
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        public async Task Handle(RoomEvents.SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
        {
            var usecase = from trainer in _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
                          from _1 in trainer.ScheduleSession(domainEvent.Session)
                          from _2 in _trainersRepository.UpdateAsync(trainer)
                          select Prelude.unit;

            await usecase
                .Run()
                .RunAsync();
        }
    }
}