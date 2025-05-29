using FluentValidation;
using GymDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using GymManagement.Domain.AggregateRoots.Rooms;
using GymManagement.Domain.AggregateRoots.Sessions;
using GymManagement.Domain.AggregateRoots.Trainers;

namespace GymManagement.Application.Usecases.Sessions.Commands;

public static class CreateSessionCommand
{
    public sealed record Request(
        Guid RoomId,
        string Name,
        string Description,
        int MaxParticipants,
        DateTime StartDateTime,
        DateTime EndDateTime,
        Guid TrainerId,
        List<string> Categories)
        //List<SessionCategory> Categories)
        : ICommand<Response>;

    public sealed record Response(Session Session)
        : IResponse;

    internal sealed class Validator
        : AbstractValidator<Request>
    {
        public Validator()
        {
        }
    }

    internal sealed class Usecase
        : ICommandUsecase<Request, Response>
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly ITrainersRepository _trainersRepository;

        public Usecase(
            IRoomsRepository roomsRepository,
            ITrainersRepository trainersRepository)
        {
            _roomsRepository = roomsRepository;
            _trainersRepository = trainersRepository;
        }

        public async Task<Fin<Response>> Handle(Request command, CancellationToken cancellationToken)
        {
            var usecase =
                from room in _roomsRepository.GetByIdAsync(command.RoomId)
                from trainer in _trainersRepository.GetByIdAsync(command.TrainerId)
                from timeSlot in TimeSlot.Create(TimeOnly.FromDateTime(command.StartDateTime), TimeOnly.FromDateTime(command.EndDateTime))
                from _1 in Prelude.guardIO(trainer.IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), timeSlot))
                let session = Session.Create(
                        name: command.Name,
                        description: command.Description,
                        maxParticipants: command.MaxParticipants,
                        roomId: command.RoomId,
                        trainerId: command.TrainerId,
                        date: DateOnly.FromDateTime(command.StartDateTime),
                        timeSlot: timeSlot,
                        categories: command.Categories)
                from _2 in room.ScheduleSession(session)
                from _3 in _roomsRepository.UpdateAsync(room)
                select session;

            Fin<Session> result = await usecase
                .Run()
                .RunAsync();

            return new Response((Session)result);

            //Fin<Room> roomResult = await _roomsRepository.GetByIdAsync(command.RoomId);
            //if (roomResult.IsFail)
            //{
            //    return (Error)roomResult;
            //}

            //Fin<Trainer> trainerResult = await _trainersRepository.GetByIdAsync(command.TrainerId);
            //if (trainerResult.IsFail)
            //{
            //    return (Error)trainerResult;
            //}

            //Fin<TimeSlot> timeRangeResult = TimeSlot.Create(
            //    TimeOnly.FromDateTime(command.StartDateTime),
            //    TimeOnly.FromDateTime(command.EndDateTime));
            //if (timeRangeResult.IsFail)
            //{
            //    return (Error)timeRangeResult;
            //}

            //if (!((Trainer)trainerResult).IsTimeSlotFree(DateOnly.FromDateTime(command.StartDateTime), (TimeSlot)timeRangeResult))
            //{
            //    return Error.New("Trainer's calendar is not free for the entire session duration");
            //}

            //Session session = Session.Create(
            //    name: command.Name,
            //    description: command.Description,
            //    maxParticipants: command.MaxParticipants,
            //    roomId: command.RoomId,
            //    trainerId: command.TrainerId,
            //    date: DateOnly.FromDateTime(command.StartDateTime),
            //    timeSlot: (TimeSlot)timeRangeResult,
            //    categories: command.Categories);

            //var scheduleSessionResult = ((Room)roomResult).ScheduleSession(session);
            //if (scheduleSessionResult.IsFail)
            //{
            //    return (Error)scheduleSessionResult;
            //}

            //await _roomsRepository.UpdateAsync((Room)roomResult);
            //return new Response((Session)result);
        }
    }
}