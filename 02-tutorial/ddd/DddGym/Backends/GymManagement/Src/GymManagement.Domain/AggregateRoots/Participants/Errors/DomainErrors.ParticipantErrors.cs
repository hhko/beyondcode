using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Participants.Errors;

public static partial class DomainErrors
{
    public static class ParticipantErrors
    {
        public static Error SessionNotFound(Guid participantId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(SessionNotFound)}",
                $"Session '{sessionId}' not found in participant's schedule '{participantId}'");

        public static Error SessionAlreadyExist(Guid participantId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' already exists in participant's schedule '{participantId}'");

        //public static Error CannotHaveTwoOrMoreOverlappingSessions(DateOnly date, TimeRange timeRange) =>
        //    ErrorCode.Operation(
        //        $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
        //        $"A participant cannot have two or more overlapping sessions '{date}', '{timeRange}'");

        ////public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.Validation(
        ////    code: $"{nameof(DomainErrors)}.{nameof(Participant)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
        ////    description: "A participant cannot have two or more overlapping sessions");

        //public static readonly Error CannotHaveTwoOrMoreOverlappingSessions = Error.New(
        //    "A participant cannot have two or more overlapping sessions");
    }
}
