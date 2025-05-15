//using FunctionalDdd.Framework.BaseTypes.Cqrs;
//using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
//using GymManagement.Domain.AggregateRoots.Rooms;
//using GymManagement.Domain.AggregateRoots.Sessions;
//using GymManagement.Domain.AggregateRoots.Trainers;
//using LanguageExt;
//using LanguageExt.Common;
//using System.Diagnostics.Contracts;

//namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;


//internal sealed class CreateSessionCommandUsecase_Case04_LINQ
//    : ICommandUsecase<CreateSessionCommand, CreateSessionResponse>
//{
//    private readonly IRoomsRepository _roomsRepository;
//    private readonly ITrainersRepository _trainersRepository;

//    public CreateSessionCommandUsecase_Case04_LINQ(
//        IRoomsRepository roomsRepository,
//        ITrainersRepository trainersRepository)
//    {
//        _roomsRepository = roomsRepository;
//        _trainersRepository = trainersRepository;
//    }

//    [Pure]
//    private Fin<TimeSlot> ValidateTrainerAvailability(Trainer trainer, CreateSessionCommand command, TimeSlot timeRange)
//    {
//        return trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), timeRange)
//            ? timeRange
//            : Error.New("Trainer's calendar is not free for the entire session duration");
//    }

//    [Pure]
//    private Session CreateSession(CreateSessionCommand command, TimeSlot timeRange)
//    {
//        Session session = Session.Create(
//            name: command.Name,
//            description: command.Description,
//            maxParticipants: command.MaxParticipants,
//            roomId: command.RoomId,
//            trainerId: command.TrainerId,
//            date: DateOnly.FromDateTime(command.StartDateTime),
//            timeSlot: timeRange,
//            categories: command.Categories);

//        return session;
//    }

//    private async Task<Fin<CreateSessionResponse>> PersistAndRespondAsync(Room room, Session session)
//    {
//        await _roomsRepository.UpdateAsync(room);
//        return new CreateSessionResponse(session);
//    }

//    //private FinT<IO, CreateSessionResponse> PersistAndRespond(Room room, Session session)
//    //{
//    //    return IO.liftAsync(async () =>
//    //    {
//    //        await _roomsRepository.UpdateAsync(room);
//    //        return Fin<CreateSessionResponse>.Succ(new CreateSessionResponse(session));
//    //    });
//    //}

//    public async Task<Fin<CreateSessionResponse>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
//    {
//        Fin<Room> roomResult = await _roomsRepository.GetByIdAsync(command.RoomId);

//        Fin<Trainer> trainerResult = await _trainersRepository.GetByIdAsync(command.TrainerId);

//        Fin<TimeSlot> timeRangeResult = TimeSlot.Create(
//            TimeOnly.FromDateTime(command.StartDateTime),
//            TimeOnly.FromDateTime(command.EndDateTime));

//        //
//        // Case 1: 연속 함수 Bind
//        //  - TODO: IO.liftAsync 개선 필요
//        //

//        //return
//        //    from room in roomResult
//        //    from trainer in trainerResult
//        //    from timeRange in timeRangeResult
//        //    from _ in ValidateTrainerAvailability(trainer, command, timeRange)
//        //    let session = CreateSession(command, timeRange)
//        //    from response in IO.liftAsync(async () => await PersistAndRespondAsync(room, session)).Run()
//        //    select response;

//        //
//        // Case 2: 연속 함수 Bind + 불순 함수(async/await)
//        //

//        Fin<(Room, Session)> result =
//            from room in roomResult
//            from trainer in trainerResult
//            from timeRange in timeRangeResult
//            from _ in ValidateTrainerAvailability(trainer, command, timeRange)
//            let session = CreateSession(command, timeRange)
//            select (room, session);
//        //select (room, CreateSession(command, timeRange));

//        return await result.MapAsync(
//            async tuple =>
//            {
//                var (room, session) = tuple;
//                await _roomsRepository.UpdateAsync(room);
//                return new CreateSessionResponse(session);
//            });
//    }
//}

//public static class FinExtension
//{
//    public static async Task<Fin<B>> MapAsync<A, B>(
//        this Fin<A> fin,
//        Func<A, Task<B>> mapAsync)
//    {
//        if (fin.IsFail)
//            return Fin<B>.Fail((Error)fin);

//        try
//        {
//            var b = await mapAsync((A)fin);
//            return Fin<B>.Succ(b);
//        }
//        catch (Exception ex)
//        {
//            return Fin<B>.Fail(Error.New(ex));
//        }
//    }

//    public static async Task<Fin<TResult>> MatchAsync<T, TResult>(
//        this Fin<T> fin,
//        Func<T, Task<TResult>> Succ,
//        Func<Error, Error> Fail)
//    {
//        try
//        {
//            return fin.IsFail
//                ? Fin<TResult>.Fail(Fail((Error)fin))
//                : Fin<TResult>.Succ(await Succ((T)fin));
//        }
//        catch (Exception ex)
//        {
//            if (fin.IsSucc)
//            {
//                return Fin<TResult>.Fail(Error.New(ex));
//            }
//            else
//            {
//                return Fin<TResult>.Fail((Error)fin + Error.New(ex));
//            }
//        }
//    }
//}