using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;
using GymManagement.Application.Abstractions.Repositories;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

internal sealed class CreateRoomCommandUsecase
    : ICommandusecase<CreateRoomCommand, CreateRoomResponse>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymsRepository _gymsRepository;

    public CreateRoomCommandUsecase(
        ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymsRepository = gymsRepository;
    }

    public async Task<IErrorOr<CreateRoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        if (await _gymsRepository.GetByIdAsync(command.GymId) is not Gym gym)
        {
            return Error
                .NotFound(description: "Gym not found")
                .ToErrorOr<CreateRoomResponse>();
        }


    }
}

//public async Task<ErrorOr<Room>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
//{
//    var gym = await _gymsRepository.GetByIdAsync(command.GymId);

//    if (gym is null)
//    {
//        return Error.NotFound(description: "Gym not found");
//    }

//    var subscription = await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId);

//    if (subscription is null)
//    {
//        return Error.Unexpected(description: "Subscription not found");
//    }

//    var room = new Room(
//        name: command.RoomName,
//        gymId: gym.Id,
//        maxDailySessions: subscription.GetMaxDailySessions());

//    var addGymResult = gym.AddRoom(room);

//    if (addGymResult.IsError)
//    {
//        return addGymResult.Errors;
//    }

//    await _gymsRepository.UpdateAsync(gym);

//    return room;
//}