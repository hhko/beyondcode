namespace GymManagement.Application.Usecases.Trainers.Events.SessionScheduled;

//// TODO: LanguageExt
//internal sealed class SessionScheduledEventUsecase
//    : IDomainEventUsecase<SessionScheduledEvent>
//{
//    private readonly ITrainersRepository _trainersRepository;

//    public SessionScheduledEventUsecase(ITrainersRepository trainersRepository)
//    {
//        _trainersRepository = trainersRepository;
//    }

//    public async Task Handle(SessionScheduledEvent domainEvent, CancellationToken cancellationToken)
//    {
//        Trainer trainer = await _trainersRepository.GetByIdAsync(domainEvent.Session.TrainerId)
//            ?? throw new DomainEventException(SessionScheduledEventErrors.TrainerNotFound);

//        ErrorOr<Success> addSessionToScheduleResult = trainer.AddSessionToSchedule(domainEvent.Session);

//        if (addSessionToScheduleResult.IsError)
//        {
//            throw new DomainEventException(
//                SessionScheduledEventErrors.TrainerScheduleUpdateFailed,
//                addSessionToScheduleResult.Errors);
//        }

//        await _trainersRepository.UpdateAsync(trainer);
//    }
//}