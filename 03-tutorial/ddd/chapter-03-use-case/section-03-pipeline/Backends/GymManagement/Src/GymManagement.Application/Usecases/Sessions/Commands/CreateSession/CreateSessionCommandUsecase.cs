using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.Abstractions.ValueObjects;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

internal sealed class CreateSessionCommandUsecase
    : ICommandUsecase<CreateSessionCommand, CreateSessionResponse>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly ITrainersRepository _trainersRepository;

    public CreateSessionCommandUsecase(
        IRoomsRepository roomsRepository,
        ITrainersRepository trainersRepository)
    {
        _roomsRepository = roomsRepository;
        _trainersRepository = trainersRepository;
    }

    public async Task<IErrorOr<CreateSessionResponse>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetByIdAsync(command.RoomId);
        if (room is null)
        {
            return Error
                .NotFound(description: "Room not found")
                .ToErrorOr<CreateSessionResponse>();
        }

        Trainer? trainer = await _trainersRepository.GetByIdAsync(command.TrainerId);
        if (trainer is null)
        {
            return Error
                .NotFound(description: "Trainer not found")
                .ToErrorOr<CreateSessionResponse>();
        }

        ErrorOr<TimeRange> createTimeRangeResult = TimeRange.FromDateTimes(command.StartDateTime, command.EndDateTime);
        //if (createTimeRangeResult.IsError)
        //        if (createTimeRangeResult.IsError && createTimeRangeResult.FirstError.Type == ErrorType.Validation)
        //        {
        //            return Error.Validation(description: "Invalid date and time");
        //        }
        if (createTimeRangeResult.IsError)
        {
            return createTimeRangeResult
                .Errors
                .ToErrorOr<CreateSessionResponse>();
            //return createTimeRangeResult.FirstError.Type == ErrorType.Validation
            //    ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
            //    : createTimeRangeResult.Errors;
        }

        if (!trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), createTimeRangeResult.Value))
        {
            return Error
                .Conflict(description: "Trainer's calendar is not free for the entire session duration")
                .ToErrorOr<CreateSessionResponse>();
        }

        Session session = new(
            name: command.Name,
            description: command.Description,
            maxParticipants: command.MaxParticipants,
            roomId: command.RoomId,
            trainerId: command.TrainerId,
            date: DateOnly.FromDateTime(command.StartDateTime),
            time: createTimeRangeResult.Value,
            categories: command.Categories);

        var scheduleSessionResult = room.ScheduleSession(session);
        if (scheduleSessionResult.IsError)
        {
            return scheduleSessionResult
                .Errors
                .ToErrorOr<CreateSessionResponse>();
        }

        await _roomsRepository.UpdateAsync(room);

        return session
            .ToResponseCreated()
            .ToErrorOr();
    }
}
