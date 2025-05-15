using DddGym.Framework.BaseTypes.Cqrs;
//using ErrorOr;
using GymManagement.Domain.Abstractions.ValueObjects;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;
using LanguageExt;
using LanguageExt.Common;
using System.Collections.Frozen;
using static LanguageExt.Fin;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

internal sealed class CreateSessionCommandUsecase
    : ICommandUsecase2<CreateSessionCommand2, CreateSessionResponse>
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

    //public async Task<IErrorOr<CreateSessionResponse>> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
    public async Task<Fin<CreateSessionResponse>> Handle(CreateSessionCommand2 command, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetByIdAsync(command.RoomId);
        if (room is null)
        {
            //return Error
            //    .NotFound(description: "Room not found")
            //    .ToErrorOr<CreateSessionResponse>();
            return Error.New("Room not found");
        }

        Trainer? trainer = await _trainersRepository.GetByIdAsync(command.TrainerId);
        if (trainer is null)
        {
            //return Error
            //    .NotFound(description: "Trainer not found")
            //    .ToErrorOr<CreateSessionResponse>();
            return Error.New("Trainer not found");
        }

        // TimeOnly.FromDateTime
        //ErrorOr<TimeRange> createTimeRangeResult = TimeRange
        //    .FromDateTimes(command.StartDateTime, command.EndDateTime);

        Fin<TimeRange> createTimeRangeResult = TimeRange.Create(
            TimeOnly.FromDateTime(command.StartDateTime),
            TimeOnly.FromDateTime(command.EndDateTime));
        //if (createTimeRangeResult.IsError)
        //        if (createTimeRangeResult.IsError && createTimeRangeResult.FirstError.Type == ErrorType.Validation)
        //        {
        //            return Error.Validation(description: "Invalid date and time");
        //        }
        //if (createTimeRangeResult.IsError)
        //{
        //    return createTimeRangeResult
        //        .Errors
        //        .ToErrorOr<CreateSessionResponse>();
        //    //return createTimeRangeResult.FirstError.Type == ErrorType.Validation
        //    //    ? ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions
        //    //    : createTimeRangeResult.Errors;
        //}
        if (createTimeRangeResult.IsFail)
        {
            return (Error)createTimeRangeResult;
            //createTimeRangeResult.ToFin<CreateSessionResponse>();
            //return createTimeRangeResult;
            //Error x = (Error)createTimeRangeResult;
            //return 
            // Fin<A> 실패일 때 -?-> Fin<B>
        }


        if (!trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), (TimeRange)createTimeRangeResult))
        {
            //return Error
            //    .Conflict(description: "Trainer's calendar is not free for the entire session duration")
            //    .ToErrorOr<CreateSessionResponse>();

            return Error.New("Trainer's calendar is not free for the entire session duration");
        }

        Session session = new(
            name: command.Name,
            description: command.Description,
            maxParticipants: command.MaxParticipants,
            roomId: command.RoomId,
            trainerId: command.TrainerId,
            date: DateOnly.FromDateTime(command.StartDateTime),
            time: (TimeRange)createTimeRangeResult,          // TODO: 성공일 때 값 접근
            categories: command.Categories);

        var scheduleSessionResult = room.ScheduleSession(session);
        if (scheduleSessionResult.IsFail)
        {
            //return scheduleSessionResult
            //    .Errors
            //    .ToErrorOr<CreateSessionResponse>();
            return (Error)scheduleSessionResult;
        }

        await _roomsRepository.UpdateAsync(room);

        return new CreateSessionResponse(session);
        //return session
        //    .ToResponseCreated()
        //    .ToErrorOr();
    }
}
