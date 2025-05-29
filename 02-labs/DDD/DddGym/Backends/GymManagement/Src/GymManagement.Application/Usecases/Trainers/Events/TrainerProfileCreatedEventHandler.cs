using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Trainers.Events.TrainerProfileCreated;

public static class TrainerProfileCreatedEventHandler
{
    internal sealed class Usecase(ITrainersRepository trainersRepository)
        : IDomainEventUsecase<UserEvents.TrainerProfileCreatedEvent>
    {
        private readonly ITrainersRepository _trainersRepository = trainersRepository;

        public async Task Handle(UserEvents.TrainerProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            var usecase = from trainer in Trainer.Create(userId: domainEvent.UserId, id: domainEvent.TrainerId)
                          from _ in _trainersRepository.AddTrainerAsync(trainer)
                          select unit;

            await usecase
                .Run()
                .RunAsync();
        }
    }
}


