////using GymDdd.Framework.BaseTypes.Cqrs;
//////using ErrorOr;
////using GymManagement.Domain.Abstractions.ValueObjects;
////using GymManagement.Domain.AggregateRoots.Rooms;
////using GymManagement.Domain.AggregateRoots.Sessions;
////using GymManagement.Domain.AggregateRoots.Trainers;
////using LanguageExt;
////using LanguageExt.Common;
////using System.Collections.Frozen;
////using static LanguageExt.Fin;

////namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

////internal sealed class CreateSessionCommandUsecase
////    : ICommandUsecase2<CreateSessionCommand2, CreateSessionResponse>
////{
////    private readonly IRoomsRepository _roomsRepository;
////    private readonly ITrainersRepository _trainersRepository;

////    public CreateSessionCommandUsecase(
////        IRoomsRepository roomsRepository,
////        ITrainersRepository trainersRepository)
////    {
////        _roomsRepository = roomsRepository;
////        _trainersRepository = trainersRepository;
////    }

////    //public async Task<IErrorOr<CreateSessionResponse>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
////    public async Task<Fin<CreateSessionResponse>> Handle(CreateSessionCommand2 command, CancellationToken cancellationToken)
////    {
////        Room? room = await _roomsRepository.GetByIdAsync(command.RoomId);
////        if (room is null)
////        {
////            //return Error
////            //    .NotFound(description: "Room not found")
////            //    .ToErrorOr<CreateSessionResponse>();
////            return Error.New("Room not found");
////        }

////        Trainer? trainer = await _trainersRepository.GetByIdAsync(command.TrainerId);
////        if (trainer is null)
////        {
////            //return Error
////            //    .NotFound(description: "Trainer not found")
////            //    .ToErrorOr<CreateSessionResponse>();
////            return Error.New("Trainer not found");
////        }

////        // TimeOnly.FromDateTime
////        //ErrorOr<TimeRange> createTimeRangeResult = TimeRange
////        //    .FromDateTimes(command.StartDateTime, command.EndDateTime);

////        Fin<TimeRange> createTimeRangeResult = TimeRange.Create(
////            TimeOnly.FromDateTime(command.StartDateTime),
////            TimeOnly.FromDateTime(command.EndDateTime));
////        //if (createTimeRangeResult.IsError)
////        //        if (createTimeRangeResult.IsError && createTimeRangeResult.FirstError.Type == ErrorType.Validation)
////        //        {
////        //            return Error.Validation(description: "Invalid date and time");
////        //        }
////        //if (createTimeRangeResult.IsError)
////        //{
////        //    return createTimeRangeResult
////        //        .Errors
////        //        .ToErrorOr<CreateSessionResponse>();
////        //    //return createTimeRangeResult.FirstError.Type == ErrorType.Validation
////        //    //    ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
////        //    //    : createTimeRangeResult.Errors;
////        //}
////        if (createTimeRangeResult.IsFail)
////        {
////            return (Error)createTimeRangeResult;
////            //createTimeRangeResult.ToFin<CreateSessionResponse>();
////            //return createTimeRangeResult;
////            //Error x = (Error)createTimeRangeResult;
////            //return 
////            // Fin<A> 실패일 때 -?-> Fin<B>
////        }


////        if (!trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), (TimeRange)createTimeRangeResult))
////        {
////            //return Error
////            //    .Conflict(description: "Trainer's calendar is not free for the entire session duration")
////            //    .ToErrorOr<CreateSessionResponse>();

////            return Error.New("Trainer's calendar is not free for the entire session duration");
////        }

////        Session session = new(
////            name: command.Name,
////            description: command.Description,
////            maxParticipants: command.MaxParticipants,
////            roomId: command.RoomId,
////            trainerId: command.TrainerId,
////            date: DateOnly.FromDateTime(command.StartDateTime),
////            time: (TimeRange)createTimeRangeResult,          // TODO: 성공일 때 값 접근
////            categories: command.Categories);

////        var scheduleSessionResult = room.ScheduleSession(session);
////        if (scheduleSessionResult.IsFail)
////        {
////            //return scheduleSessionResult
////            //    .Errors
////            //    .ToErrorOr<CreateSessionResponse>();
////            return (Error)scheduleSessionResult;
////        }

////        await _roomsRepository.UpdateAsync(room);

////        return new CreateSessionResponse(session);
////        //return session
////        //    .ToResponseCreated()
////        //    .ToErrorOr();
////    }
////}


//using GymDdd.Framework.BaseTypes.Cqrs;
//using GymManagement.Domain.Abstractions.ValueObjects;
//using GymManagement.Domain.AggregateRoots.Rooms;
//using GymManagement.Domain.AggregateRoots.Sessions;
//using GymManagement.Domain.AggregateRoots.Trainers;
//using LanguageExt;
//using LanguageExt.Common;
//using LanguageExt.Traits;
//using System.Diagnostics.Contracts;
//using static LanguageExt.Prelude;

//namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

//public static class RepositoryExtensions
//{
//    public static async Task<Fin<T>> ToFin<T>(this Task<T?> task, string errorMessage) where T : class
//    {
//        var result = await task;
//        return result != null 
//            ? Fin<T>.Succ(result) 
//            : Fin<T>.Fail(Error.New(errorMessage));
//    }

//    //public static async Task<Fin<TResult>> BindAsync<T, TResult>(
//    //    this Fin<T> fin,
//    //    Func<T, Task<Fin<TResult>>> func)
//    //{
//    //    //if (fin.IsFail)
//    //    //    return Fin<TResult>.Fail((Error)fin);

//    //    //return await func((T)fin);

//    //    return fin.IsFail
//    //        ? Fin<TResult>.Fail((Error)fin)
//    //        : await func((T)fin);
//    //}

//    //public static async Task<Fin<TResult>> MatchAsync<T, TResult>(
//    //    this Fin<T> fin,
//    //    Func<T, Task<Fin<TResult>>> Succ,
//    //    Func<Error, Fin<TResult>> Fail)
//    //{
//    //    return fin.IsFail
//    //        ? Fail((Error)fin)
//    //        : await Succ((T)fin);
//    //}

//    //[Pure]
//    //public static async Fin<B> MapAsync<A, B>(this Fin<A> fin, Func<A, B> Succ)
//    //{
//    //    if
//    //    await Succ((A)Value);
//    //    new Succ<B>();
//    //}
//}

//internal sealed class CreateSessionCommandUsecase
//    : ICommandUsecase2<CreateSessionCommand2, CreateSessionResponse>
//{
//    private readonly IRoomsRepository _roomsRepository;
//    private readonly ITrainersRepository _trainersRepository;

//    public CreateSessionCommandUsecase(
//        IRoomsRepository roomsRepository,
//        ITrainersRepository trainersRepository)
//    {
//        _roomsRepository = roomsRepository;
//        _trainersRepository = trainersRepository;
//    }

//    // 트레이너의 가용 시간 체크를 Fin<T>로 래핑
//    private Fin<Unit> CheckTrainerAvailability(Trainer trainer, DateOnly date, TimeRange timeRange)
//    {
//        return trainer.IsTimeSlotFree(date, timeRange)
//            ? Fin<Unit>.Succ(Unit.Default)
//            : Fin<Unit>.Fail(Error.New("Trainer's calendar is not free for the entire session duration"));
//    }

//    [Pure]
//    private Fin<TimeRange> ValidateTrainerAvailability(Trainer trainer, CreateSessionCommand2 command, TimeRange timeRange)
//    {
//        return trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), timeRange)
//            ? Fin<TimeRange>.Succ(timeRange)
//            : Error.New("Trainer's calendar is not free for the entire session duration");
//    }

//    [Pure]
//    private Session CreateSession(CreateSessionCommand2 command, TimeRange timeRange)
//    {
//        var session = new Session(
//            name: command.Name,
//            description: command.Description,
//            maxParticipants: command.MaxParticipants,
//            roomId: command.RoomId,
//            trainerId: command.TrainerId,
//            date: DateOnly.FromDateTime(command.StartDateTime),
//            time: timeRange,
//            categories: command.Categories);

//        return session;
//    }

//    private async Task<Fin<CreateSessionResponse>> ScheduleAndSaveAsync(Room room, Session session)
//    {
//        var result = room.ScheduleSession(session);
//        if (result.IsFail)
//            return (Error)result;

//        await _roomsRepository.UpdateAsync(room);
//        return new CreateSessionResponse(session);
//    }

//    // 방 업데이트 및 결과 반환
//    //private async Task<CreateSessionResponse> PersistAndRespondAsync(Room room, Session session)
//    //{
//    //    await _roomsRepository.UpdateAsync(room);
//    //    return new CreateSessionResponse(session);
//    //}

//    private async Task<Fin<CreateSessionResponse>> PersistAndRespondAsync(Room room, Session session)
//    {
//        await _roomsRepository.UpdateAsync(room);
//        return new CreateSessionResponse(session);
//    }

//    private async Task<FinT<IO, CreateSessionResponse>> PersistAndRespondAsync2(Room room, Session session)
//    {
//        await _roomsRepository.UpdateAsync(room);
//        return FinT<IO, CreateSessionResponse>.Succ(new CreateSessionResponse(session));
//    }

//    public async Task<Fin<CreateSessionResponse>> Handle(CreateSessionCommand2 command, CancellationToken cancellationToken)
//    {
//        //await FinT<IO, string>.Bind(
//        //    FinT<IO, int>.Lift(Fin<int>.Succ(10)), // Start with a successful Fin<int>
//        //    async x =>
//        //    {
//        //        var finResult = await DoSomethingAsync(x); // Simulate async work that returns Fin<A>
//        //        return FinT<IO, string>.Lift(finResult.Map(val => val.ToString())); // project int to string, and lift to FinT<IO, string>
//        //    });

//        Room ? room = await _roomsRepository.GetByIdAsync(command.RoomId);
//        if (room is null)
//        {
//            return Error.New("Room not found");
//        }

//        Trainer? trainer = await _trainersRepository.GetByIdAsync(command.TrainerId);
//        if (trainer is null)
//        {
//            return Error.New("Trainer not found");
//        }

//        //K<Fin, Fin<CreateSessionResponse>> x = TimeRange
//        //.Bind(session => FinT<IO, FinT<IO, CreateSessionResponse>>.Lift(IO.liftAsync(async() => await PersistAndRespondAsync2(room, session))));
//        //.Bind(session => liftIO(PersistAndRespondAsync(room, session)));

//        Fin<CreateSessionResponse> x = TimeRange
//            .Create(
//                TimeOnly.FromDateTime(command.StartDateTime),
//                TimeOnly.FromDateTime(command.EndDateTime))
//            .Bind(timeRange => ValidateTrainerAvailability(trainer, command, timeRange))
//            .Map(timeRange => CreateSession(command, timeRange))
//            .Bind(session => IO.liftAsync(async () => await PersistAndRespondAsync(room, session)).Run());

//        return x;

//        //public static FinT<IO, A> Flatten<A>(this Task<FinT<IO, A>> tma) =>
//        //    FinT<IO, FinT<IO, A>>
//        //        .Lift(IO.liftAsync(async () => await tma.ConfigureAwait(false)))
//        //        .Flatten();

//        //public static FinT<IO, A> liftIO<A>(Task<Fin<A>> ma) =>
//        //    new(IO.liftAsync(async () => await ma.ConfigureAwait(false)));

//        //.BiBind<CreateSessionResponse>(
//        //Succ: session =>
//        //{
//        //    var x = await PersistAndRespondAsync(room, session);
//        //    return Fin<CreateSessionResponse>.Succ(new CreateSessionResponse(session));
//        //}, 
//        //Fail: async error =>
//        //{
//        //    await Task.CompletedTask;
//        //    return Fin< CreateSessionResponse>.Fail(error);
//        //});
//        //.Bind(session => room.ScheduleSession(session))

//        //x.IfSucc()
//        //if (x.IsSucc)
//        //{
//        //    await PersistAndRespondAsync(room, )
//        //}

//        //// Case 1: 기본
//        //Room? room = await _roomsRepository.GetByIdAsync(command.RoomId);
//        //if (room is null)
//        //{
//        //    return Error.New("Room not found");
//        //}

//        //Trainer? trainer = await _trainersRepository.GetByIdAsync(command.TrainerId);
//        //if (trainer is null)
//        //{
//        //    return Error.New("Trainer not found");
//        //}

//        //Fin<TimeRange> createTimeRangeResult = TimeRange.Create(
//        //    TimeOnly.FromDateTime(command.StartDateTime),
//        //    TimeOnly.FromDateTime(command.EndDateTime));
//        //if (createTimeRangeResult.IsFail)
//        //{
//        //    return (Error)createTimeRangeResult;
//        //}

//        //if (!trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), (TimeRange)createTimeRangeResult))
//        //{
//        //    return Error.New("Trainer's calendar is not free for the entire session duration");
//        //}

//        //Session session = new(
//        //    name: command.Name,
//        //    description: command.Description,
//        //    maxParticipants: command.MaxParticipants,
//        //    roomId: command.RoomId,
//        //    trainerId: command.TrainerId,
//        //    date: DateOnly.FromDateTime(command.StartDateTime),
//        //    time: (TimeRange)createTimeRangeResult,
//        //    categories: command.Categories);

//        //var scheduleSessionResult = room.ScheduleSession(session);
//        //if (scheduleSessionResult.IsFail)
//        //{
//        //    return (Error)scheduleSessionResult;
//        //}

//        //await _roomsRepository.UpdateAsync(room);
//        //return new CreateSessionResponse(session);


//        //// Case 2: 연속 함수 Bind
//        //Fin<Room> roomResult = await _roomsRepository.GetByIdAsync(command.RoomId)
//        //    .ToFin("Room not found");

//        //Fin<Trainer> trainerResult = await _trainersRepository.GetByIdAsync(command.TrainerId)
//        //    .ToFin("Trainer not found");

//        //Fin<TimeRange> timeRangeResult = TimeRange.Create(
//        //    TimeOnly.FromDateTime(command.StartDateTime),
//        //    TimeOnly.FromDateTime(command.EndDateTime));

//        //Fin<(Room, Session)> result = roomResult.Bind(room =>
//        //    trainerResult.Bind(trainer =>
//        //        TimeRange.Create(
//        //            TimeOnly.FromDateTime(command.StartDateTime),
//        //            TimeOnly.FromDateTime(command.EndDateTime)).Bind(timeRange =>
//        //        {
//        //            var date = DateOnly.FromDateTime(command.StartDateTime);
//        //            if (!trainer.IsTimeSlotFree(date, timeRange))
//        //                return Fin<(Room, Session)>.Fail(Error.New("Trainer's calendar is not free"));

//        //            var session = new Session(
//        //                name: command.Name,
//        //                description: command.Description,
//        //                maxParticipants: command.MaxParticipants,
//        //                roomId: command.RoomId,
//        //                trainerId: command.TrainerId,
//        //                date: date,
//        //                time: timeRange,
//        //                categories: command.Categories
//        //            );

//        //            return room.ScheduleSession(session)
//        //                .Bind(_ => Fin<(Room, Session)>.Succ((room, session)));
//        //        })
//        //    )
//        //);

//        //return await result.MatchAsync(
//        //    Succ: async tuple =>
//        //    {
//        //        var (room, session) = tuple;
//        //        await _roomsRepository.UpdateAsync(room);
//        //        return new CreateSessionResponse(session);
//        //    },
//        //    Fail: Fin<CreateSessionResponse>.Fail);


//        // Case 3: 연속 함수 LINQ
//        //Fin<Room> roomResult = await _roomsRepository.GetByIdAsync(command.RoomId)
//        //    .ToFin("Room not found");

//        //Fin<Trainer> trainerResult = await _trainersRepository.GetByIdAsync(command.TrainerId)
//        //    .ToFin("Trainer not found");

//        //Fin<TimeRange> timeRangeResult = TimeRange.Create(
//        //    TimeOnly.FromDateTime(command.StartDateTime),
//        //    TimeOnly.FromDateTime(command.EndDateTime));

//        //Fin<(Room, Session)> result =
//        //    from room in roomResult
//        //    from trainer in trainerResult
//        //    from timeRange in timeRangeResult
//        //    from session in ValidateAndCreateSession(room, trainer, command, timeRange)
//        //    from _ in room.ScheduleSession(session)
//        //    select (room, session);

//        //return await result.MatchAsync(
//        //    Succ: async tuple =>
//        //    {
//        //        var (room, session) = tuple;
//        //        await _roomsRepository.UpdateAsync(room);
//        //        return new CreateSessionResponse(session);
//        //    },
//        //    Fail: Fin<CreateSessionResponse>.Fail);

//        //// Case 4.
//        ////  - BindAsync 에러: Bind만 제공함
//        //return async(
//        //    from room in roomResult
//        //    from trainer in trainerResult
//        //    from timeRange in timeRangeResult
//        //    from session in ValidateAndCreateSession(room, trainer, command, timeRange)
//        //    from _ in room.ScheduleSession(session)
//        //        //select (room, session);
//        //    select await PersistAndRespondAsync(room, session));
//    }

//    [Pure]
//    private Fin<Session> ValidateAndCreateSession(
//        Room room,
//        Trainer trainer,
//        CreateSessionCommand2 command,
//        TimeRange timeRange)
//    {
//        var date = DateOnly.FromDateTime(command.StartDateTime);

//        if (!trainer.IsTimeSlotFree(date, timeRange))
//            return Fin<Session>.Fail(Error.New("Trainer's calendar is not free"));

//        return new Session(
//            name: command.Name,
//            description: command.Description,
//            maxParticipants: command.MaxParticipants,
//            roomId: command.RoomId,
//            //trainerId: command.TrainerId,
//            trainerId: trainer.Id,
//            date: date,
//            time: timeRange,
//            categories: command.Categories
//        );
//    }
//}

