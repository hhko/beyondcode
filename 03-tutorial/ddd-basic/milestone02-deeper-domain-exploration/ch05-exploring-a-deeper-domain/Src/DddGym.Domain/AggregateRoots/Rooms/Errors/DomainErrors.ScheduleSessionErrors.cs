using ErrorOr;

namespace DddGym.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainErrors
{
    public static class ScheduleSessionErrors
    {
        // TODO: 현재 값. 기대 값
        public static readonly Error CannotHaveMoreSessionThanSubscriptionAllows = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Room)}.{nameof(CannotHaveMoreSessionThanSubscriptionAllows)}",
            description: "A room cannot have more scheduled sessions than the subscription allows");

        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
            code: $"{nameof(DomainErrors)}.{nameof(Room)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
            description: "A room cannot have two or more overlapping sessions");
    }
}