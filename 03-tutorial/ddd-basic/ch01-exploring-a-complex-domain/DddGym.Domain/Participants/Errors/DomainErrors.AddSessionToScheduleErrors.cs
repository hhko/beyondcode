using ErrorOr;

namespace DddGym.Domain.Participants.Errors;

public static partial class DomainErrors
{
    public static class AddToScheduleErrors
    {
        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
            $"{nameof(DomainErrors)}.{nameof(Participant)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
            "A trainer cannot have two or more overlapping sessions");
    }
}
