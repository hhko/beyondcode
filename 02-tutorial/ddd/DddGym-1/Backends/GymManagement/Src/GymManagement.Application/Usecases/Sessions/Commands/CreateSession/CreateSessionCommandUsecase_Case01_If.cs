using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.Abstractions.ValueObjects;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;
using LanguageExt;
using LanguageExt.Common;
using System.Diagnostics.Contracts;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;


internal sealed class CreateSessionCommandUsecase_Case01_If
    : ICommandUsecase2<CreateSessionCommand2, CreateSessionResponse>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ITrainersRepository _trainersRepository;

    public CreateSessionCommandUsecase_Case01_If(
        IRoomsRepository roomsRepository,
        ITrainersRepository trainersRepository)
    {
        _roomsRepository = roomsRepository;
        _trainersRepository = trainersRepository;
    }

    public async Task<Fin<CreateSessionResponse>> Handle(CreateSessionCommand2 command, CancellationToken cancellationToken)
    {
        Fin<Room> roomResult = await _roomsRepository.GetByIdAsync(command.RoomId);
        if (roomResult.IsFail)
        {
            return (Error)roomResult;
        }

        Fin<Trainer> trainerResult = await _trainersRepository.GetByIdAsync(command.TrainerId);
        if (trainerResult.IsFail)
        {
            return (Error)trainerResult;
        }

        Fin<TimeRange> timeRangeResult = TimeRange.Create(
            TimeOnly.FromDateTime(command.StartDateTime),
            TimeOnly.FromDateTime(command.EndDateTime));
        if (timeRangeResult.IsFail)
        {
            return (Error)timeRangeResult;
        }

        if (!((Trainer)trainerResult).IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), (TimeRange)timeRangeResult))
        {
            return Error.New("Trainer's calendar is not free for the entire session duration");
        }

        Session session = new(
            name: command.Name,
            description: command.Description,
            maxParticipants: command.MaxParticipants,
            roomId: command.RoomId,
            trainerId: command.TrainerId,
            date: DateOnly.FromDateTime(command.StartDateTime),
            time: (TimeRange)timeRangeResult,
            categories: command.Categories);

        var scheduleSessionResult = ((Room)roomResult).ScheduleSession(session);
        if (scheduleSessionResult.IsFail)
        {
            return (Error)scheduleSessionResult;
        }

        await _roomsRepository.UpdateAsync((Room)roomResult);
        return new CreateSessionResponse(session);
    }
}
