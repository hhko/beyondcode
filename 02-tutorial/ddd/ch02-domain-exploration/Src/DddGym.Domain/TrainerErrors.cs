using ErrorOr;

namespace DddGym.Domain;

public static partial class TrainerErrors
{
    public static class AddSessionToScheduleErrors
    {
        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
            $"{nameof(Trainer)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
            "A trainer cannot have two or more overlapping sessions");
    }
}
