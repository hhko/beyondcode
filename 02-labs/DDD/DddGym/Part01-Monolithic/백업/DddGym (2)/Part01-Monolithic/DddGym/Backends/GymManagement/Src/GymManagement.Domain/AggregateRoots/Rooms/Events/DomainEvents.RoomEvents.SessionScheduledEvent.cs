using GymDdd.Framework.BaseTypes.Events;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Domain.AggregateRoots.Rooms.Events;

//public sealed record SessionScheduledEvent(
//    Room Room,
//    Session Session) : IDomainEvent;

//public sealed record SessionScheduledEvent(
//    Guid RoomId,
//    Guid TrainerId) : IDomainEvent;

public static partial class DomainEvents
{
    public static partial class RoomEvents
    {
        public sealed record SessionScheduledEvent(
            Guid RoomId,
            Session Session) : IDomainEvent;
    }
}

//{
//    //public static readonly Error TrainerNotFound = EventualConsistencyError.From(
//    //    code: "SessionScheduledEvent.TrainerNotFound",
//    //    description: "Trainer not found");

//    //public static readonly Error TrainerScheduleUpdateFailed = EventualConsistencyError.From(
//    //    code: "SessionScheduledEvent.TrainerScheduleUpdateFailed",
//    //    description: "Adding session to trainer's schedule failed");

//    //public static readonly Error GymNotFound = EventualConsistencyError.From(
//    //    code: "SessionScheduledEvent.GymNotFound",
//    //    description: "Gym not found");
//}