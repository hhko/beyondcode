using DddGym.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Domain.AggregateRoots.Users.Events;

namespace GymManagement.Application.Usecases.Trainers.Events.TrainerProfileCreated;

internal sealed class TrainerProfileCreatedEventUsecase
    : IDomainEventUsecase<TrainerProfileCreatedEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public TrainerProfileCreatedEventUsecase(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(TrainerProfileCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var trainer = new Trainer(domainEvent.UserId, id: domainEvent.TrainerId);
        await _trainersRepository.AddTrainerAsync(trainer);
    }
}
