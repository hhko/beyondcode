﻿using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Trainers;
using static GymManagement.Domain.AggregateRoots.Sessions.Events.DomainEvents;

namespace GymManagement.Application.Usecases.Trainers.Events;

public static class SessionCanceledEvent
{
    //// TODO: LanguageExt
    internal sealed class Usecase
        : IDomainEventUsecase<SessionEvents.SessionCanceledEvent>
    {
        private readonly ITrainersRepository _trainersRepository;

        public Usecase(ITrainersRepository trainersRepository)
        {
            _trainersRepository = trainersRepository;
        }

        public Task Handle(SessionEvents.SessionCanceledEvent notification, CancellationToken cancellationToken)
        {
            //Trainer trainer = await _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
            //    ?? throw new DomainEventException(SessionCanceledEventErrors.TrainerNotFound);

            //ErrorOr<Success> removeFromScheduleResult = trainer.RemoveFromSchedule(domainEvent.Session);

            //if (removeFromScheduleResult.IsError)
            //{
            //    throw new DomainEventException(
            //        SessionCanceledEventErrors.TrainerScheduleUpdateFailed,
            //        removeFromScheduleResult.Errors);
            //}

            //await _trainersRepository.UpdateAsync(trainer);
        }

        //public async Task Handle(SessionCanceledEvent domainEvent, CancellationToken cancellationToken)
        //{
        //    Trainer trainer = await _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
        //        ?? throw new DomainEventException(SessionCanceledEventErrors.TrainerNotFound);

        //    ErrorOr<Success> removeFromScheduleResult = trainer.RemoveFromSchedule(domainEvent.Session);

        //    if (removeFromScheduleResult.IsError)
        //    {
        //        throw new DomainEventException(
        //            SessionCanceledEventErrors.TrainerScheduleUpdateFailed,
        //            removeFromScheduleResult.Errors);
        //    }

        //    await _trainersRepository.UpdateAsync(trainer);
        //}
    }
}