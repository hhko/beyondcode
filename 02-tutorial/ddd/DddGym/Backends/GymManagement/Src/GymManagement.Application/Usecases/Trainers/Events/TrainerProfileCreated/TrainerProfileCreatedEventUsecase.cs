using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Trainers.Events.TrainerProfileCreated;

internal sealed class TrainerProfileCreatedEventUsecase
    : IDomainEventUsecase<UserEvents.TrainerProfileCreatedEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public TrainerProfileCreatedEventUsecase(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(UserEvents.TrainerProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        //var trainer = new Trainer(
        Trainer trainer = Trainer.Create(
            userId: domainEvent.UserId,
            id: domainEvent.TrainerId);
        await _trainersRepository.AddTrainerAsync(trainer);
    }
}
