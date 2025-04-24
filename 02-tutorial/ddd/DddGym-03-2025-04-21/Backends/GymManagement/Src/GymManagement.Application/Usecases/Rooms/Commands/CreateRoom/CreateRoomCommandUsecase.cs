namespace GymManagement.Application.Usecases.Rooms.Commands.CreateRoom;

//// TODO: LanguageExt
//internal sealed class CreateRoomCommandUsecase
//    : ICommandUsecase<CreateRoomCommand, CreateRoomResponse>
//{
//    private readonly ISubscriptionsRepository _subscriptionsRepository;
//    private readonly IGymsRepository _gymsRepository;

//    public CreateRoomCommandUsecase(
//        ISubscriptionsRepository subscriptionsRepository, IGymsRepository gymsRepository)
//    {
//        _subscriptionsRepository = subscriptionsRepository;
//        _gymsRepository = gymsRepository;
//    }

//    public async Task<IErrorOr<CreateRoomResponse>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
//    {
//        if (await _gymsRepository.GetByIdAsync(command.GymId) is not Gym gym)
//        {
//            return Error
//                .NotFound(description: "Gym not found")
//                .ToErrorOr<CreateRoomResponse>();
//        }

//        if (await _subscriptionsRepository.GetByIdAsync(gym.SubscriptionId) is not Subscription subscription)
//        {
//            return Error
//                .NotFound(description: "Subscription not found")
//                .ToErrorOr<CreateRoomResponse>();
//        }

//        var room = new Room(
//            name: command.RoomName,
//            gymId: gym.Id,
//            maxDailySessions: subscription.GetMaxDailySessions());

//        var addRoomResult = gym.AddRoom(room);
//        if (addRoomResult.IsError)
//        {
//            return addRoomResult
//                .Errors
//                .ToErrorOr<CreateRoomResponse>();
//        }

//        await _gymsRepository.UpdateAsync(gym);

//        return room
//            .ToResponseCreated()
//            .ToErrorOr();
//    }
//}