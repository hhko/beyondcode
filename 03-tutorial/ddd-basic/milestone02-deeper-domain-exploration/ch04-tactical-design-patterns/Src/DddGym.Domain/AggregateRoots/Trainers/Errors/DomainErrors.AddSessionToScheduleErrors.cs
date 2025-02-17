using ErrorOr;

namespace DddGym.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static class AddSessionToScheduleErrors
    {
        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
            $"{nameof(DomainErrors)}.{nameof(Trainer)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
            "A trainer cannot have two or more overlapping sessions");
    }
}