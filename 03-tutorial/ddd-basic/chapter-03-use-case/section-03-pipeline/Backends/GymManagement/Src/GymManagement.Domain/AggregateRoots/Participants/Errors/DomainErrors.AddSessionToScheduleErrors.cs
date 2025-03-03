using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Participants.Errors;

public static partial class DomainErrors
{
    public static class AddToScheduleErrors
    {
        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
            code: $"{nameof(Domain)}.{nameof(Participant)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
            description: "A participant cannot have two or more overlapping sessions");
    }
}