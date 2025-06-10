using ErrorOr;

namespace DddGym.Domain;

public static partial class ParticipantErrors
{
    public static class AddToScheduleErrors
    {
        public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
            code: $"{nameof(Participant)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
            description: "A participant cannot have two or more overlapping sessions");
    }
}
