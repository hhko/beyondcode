using DddGym.Framework.BaseTypes.Events;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Sessions.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Sessions.Errors.DomainEventErrors;

namespace GymManagement.Application.Usecases.Trainers.Events.SessionCanceled;

internal sealed class SessionCanceledEventUsecase
    : IDomainEventUsecase<SessionCanceledEvent>
{
    private readonly ITrainersRepository _trainersRepository;

    public SessionCanceledEventUsecase(ITrainersRepository trainersRepository)
    {
        _trainersRepository = trainersRepository;
    }

    public async Task Handle(SessionCanceledEvent domainEvent, CancellationToken cancellationToken)
    {
        Trainer trainer = await _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
            ?? throw new DomainEventException(SessionCanceledEventErrors.TrainerNotFound);

        ErrorOr<Success> removeFromScheduleResult = trainer.RemoveFromSchedule(domainEvent.Session);

        if (removeFromScheduleResult.IsError)
        {
            throw new DomainEventException(
                SessionCanceledEventErrors.TrainerScheduleUpdateFailed,
                removeFromScheduleResult.Errors);
        }

        await _trainersRepository.UpdateAsync(trainer);
    }
}