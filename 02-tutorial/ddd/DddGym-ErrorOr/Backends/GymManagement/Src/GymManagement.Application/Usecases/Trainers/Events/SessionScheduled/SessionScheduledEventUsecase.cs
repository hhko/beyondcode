using DddGym.Framework.BaseTypes.Events;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Rooms.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Rooms.Errors.DomainEventErrors;

namespace GymManagement.Application.Usecases.Trainers.Events.SessionScheduled;

internal sealed class SessionScheduledEventUsecase
    : IDomainEventUsecase<SessionScheduledEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public SessionScheduledEventUsecase(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
    {
        Trainer trainer = await _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
            ?? throw new DomainEventException(SessionScheduledEventErrors.TrainerNotFound);

        ErrorOr<Success> addSessionToScheduleResult = trainer.AddSessionToSchedule(domainEvent.Session);

        if (addSessionToScheduleResult.IsError)
        {
            throw new DomainEventException(
                SessionScheduledEventErrors.TrainerScheduleUpdateFailed,
                addSessionToScheduleResult.Errors);
        }

        await _trainersRepository.UpdateAsync(trainer);
    }
}