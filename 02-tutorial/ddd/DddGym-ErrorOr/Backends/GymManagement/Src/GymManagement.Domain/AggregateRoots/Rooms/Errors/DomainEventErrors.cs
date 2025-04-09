using DddGym.Framework.BaseTypes.Events;
using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainEventErrors
{
    public static class SessionScheduledEventErrors
    {
        public static readonly Error TrainerNotFound = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Room)}.{nameof(TrainerNotFound)}",
            description: "Trainer not found");

        public static readonly Error TrainerScheduleUpdateFailed = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Room)}.{nameof(TrainerScheduleUpdateFailed)}",
            description: "Adding session to trainer's schedule failed");
    }
}
