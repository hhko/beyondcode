using DddGym.Framework.BaseTypes.Events;
using LanguageExt.Common;


namespace GymManagement.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainEventErrors
{
    public static class SessionScheduledEventErrors
    {
        //public static readonly Error TrainerNotFound = DomainEventError.From(
        //    code: $"{nameof(DomainEventErrors)}.{nameof(Room)}.{nameof(TrainerNotFound)}",
        //    description: "Trainer not found");

        //public static readonly Error TrainerScheduleUpdateFailed = DomainEventError.From(
        //    code: $"{nameof(DomainEventErrors)}.{nameof(Room)}.{nameof(TrainerScheduleUpdateFailed)}",
        //    description: "Adding session to trainer's schedule failed");

        public static readonly Error TrainerNotFound = DomainEventError.From(
            "Trainer not found");

        public static readonly Error TrainerScheduleUpdateFailed = DomainEventError.From(
            "Adding session to trainer's schedule failed");
    }
}
