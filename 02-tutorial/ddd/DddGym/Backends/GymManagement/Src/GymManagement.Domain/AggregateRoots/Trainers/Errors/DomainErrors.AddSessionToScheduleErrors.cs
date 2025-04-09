

using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static class AddSessionToScheduleErrors
    {
        //public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
        //    $"{nameof(DomainErrors)}.{nameof(Trainer)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
        //    "A trainer cannot have two or more overlapping sessions");

        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.New(
            "A trainer cannot have two or more overlapping sessions");
    }
}