using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt;
using LanguageExt.Common;
using System.Diagnostics.Contracts;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;


internal sealed class CreateSessionCommandUsecase_Case03_Bind_PrivateMethod
    : ICommandUsecase2<CreateSessionCommand2, CreateSessionResponse>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ITrainersRepository _trainersRepository;

    public CreateSessionCommandUsecase_Case03_Bind_PrivateMethod(
        IRoomsRepository roomsRepository,
        ITrainersRepository trainersRepository)
    {
        _roomsRepository = roomsRepository;
        _trainersRepository = trainersRepository;
    }

    [Pure]
    private Fin<TimeSlot> ValidateTrainerAvailability(Trainer trainer, CreateSessionCommand2 command, TimeSlot timeRange)
    {
        return trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), timeRange)
            ? timeRange
            : Error.New("Trainer's calendar is not free for the entire session duration");
    }

    [Pure]
    private Session CreateSession(CreateSessionCommand2 command, TimeSlot timeRange)
    {
        var session = new Session(
            name: command.Name,
            description: command.Description,
            maxParticipants: command.MaxParticipants,
            roomId: command.RoomId,
            trainerId: command.TrainerId,
            date: DateOnly.FromDateTime(command.StartDateTime),
            time: timeRange,
            categories: command.Categories);

        return session;
    }

    private async Task<Fin<CreateSessionResponse>> PersistAndRespondAsync(Room room, Session session)
    {
        await _roomsRepository.UpdateAsync(room);
        return new CreateSessionResponse(session);
    }

    //private FinT<IO, CreateSessionResponse> PersistAndRespond(Room room, Session session)
    //{
    //    return IO.liftAsync(async () =>
    //    {
    //        await _roomsRepository.UpdateAsync(room);
    //        return Fin<CreateSessionResponse>.Succ(new CreateSessionResponse(session));
    //    });
    //}

    public async Task<Fin<CreateSessionResponse>> Handle(CreateSessionCommand2 command, CancellationToken cancellationToken)
    {
        Fin<Room> roomResult = await _roomsRepository.GetByIdAsync(command.RoomId);

        Fin<Trainer> trainerResult = await _trainersRepository.GetByIdAsync(command.TrainerId);

        Fin<TimeSlot> timeRangeResult = TimeSlot.Create(
            TimeOnly.FromDateTime(command.StartDateTime),
            TimeOnly.FromDateTime(command.EndDateTime));

        //
        // Case 1: 연속 함수 Bind
        //  - TODO: IO.liftAsync 개선 필요
        //

        //return roomResult
        //    .Bind(room =>
        //        trainerResult
        //            .Bind(trainer =>
        //                timeRangeResult
        //                    .Bind(timeRange => ValidateTrainerAvailability(trainer, command, timeRange))
        //                    .Map(timeRange => CreateSession(command, timeRange))
        //                    .Bind(session => IO.liftAsync(async () => await PersistAndRespondAsync(room, session)).Run())
        //            //.Bind(session => liftIO(PersistAndRespondAsync(room, session).Result)
        //            )
        //    );

        //
        // Case 2: 연속 함수 Bind + 불순 함수(async/await)
        //

        Fin<(Room, Session)> result = roomResult
            .Bind(room =>
                trainerResult
                    .Bind(trainer =>
                        timeRangeResult
                            .Bind(timeRange => ValidateTrainerAvailability(trainer, command, timeRange))
                            .Map(timeRange => CreateSession(command, timeRange))
                            .Bind(session => Fin<(Room, Session)>.Succ((room, session)))
                    //.Bind(session => liftIO(PersistAndRespondAsync(room, session).Result)
                    )
            );

        return await result.MapAsync(
            async tuple =>
            {
                var (room, session) = tuple;
                await _roomsRepository.UpdateAsync(room);
                return new CreateSessionResponse(session);
            });
    }
}

