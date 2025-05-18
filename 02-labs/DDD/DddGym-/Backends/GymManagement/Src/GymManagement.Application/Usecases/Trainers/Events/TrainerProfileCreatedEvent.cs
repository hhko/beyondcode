using FunctionalDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Trainers.Events.TrainerProfileCreated;

public static class TrainerProfileCreatedEvent
{
    internal sealed class Usecase
        : IDomainEventUsecase<UserEvents.TrainerProfileCreatedEvent>
    {
        private readonly ITrainersRepository _trainersRepository;

        public Usecase(ITrainersRepository trainersRepository)
        {
            _trainersRepository = trainersRepository;
        }

        public async Task Handle(UserEvents.TrainerProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            Trainer trainer = Trainer.Create(
                userId: domainEvent.UserId,
                id: domainEvent.TrainerId);

            await _trainersRepository.AddTrainerAsync(trainer);
        }
    }
}