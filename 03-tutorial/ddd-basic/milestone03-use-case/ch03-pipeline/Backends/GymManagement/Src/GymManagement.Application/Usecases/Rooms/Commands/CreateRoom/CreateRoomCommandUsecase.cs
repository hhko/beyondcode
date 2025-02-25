using DddGym.Framework.BaseTypes.Application.Cqrs;
using ErrorOr;

namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

internal sealed class CreateRoomCommandUsecase
    : ICommandusecase<CreateRoomCommand, CreateRoomResponse>
{
    public Task<IErrorOr<CreateRoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
//    private readonly ISubscriptionsRepository _subscriptionsRepository;

//    public CreateRoomCommandUsecase(ISubscriptionsRepository subscriptionsRepository)
//    {
//        _subscriptionsRepository = subscriptionsRepository;
//    }

//    //public async Task<IErrorOr<CreateRoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
//    //{
//    //    if (await _subscriptionsRepository.GetByIdAsync(command.SubscriptionId) is not Subscription subscription)
//    //    {
//    //        return Error
//    //            .NotFound(description: "Subscription not found")
//    //            .ToErrorOr<CreateRoomResponse>();
//    //    }

//    //    // var gym = new Gym(
//    //    //     name: command.Name,
//    //    //     maxRooms: subscription.GetMaxRooms(),
//    //    //     subscriptionId: subscription.Id);

//    //    // var addGymResult = subscription.AddGym(gym);
//    //    // if (addGymResult.IsError)
//    //    // {
//    //    //     return addGymResult
//    //    //         .Errors
//    //    //         .ToErrorOr<CreateRoomResponse>();
//    //    // }

//    //    await _subscriptionsRepository.UpdateAsync(subscription);


//    //}
//}


//private readonly ISubscriptionsRepository _subscriptionsRepository;
//private readonly IGymsRepository _gymsRepository;

//public CreateRoomCommandHandler(ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
//{
//    _subscriptionsRepository = subscriptionsRepository;
//    _gymsRepository = gymsRepository;
//}

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