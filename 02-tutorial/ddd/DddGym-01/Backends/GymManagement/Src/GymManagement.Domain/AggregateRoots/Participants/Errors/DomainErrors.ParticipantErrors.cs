using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Participants.Errors;

public static partial class DomainErrors
{
    public static class ParticipantErrors
    {
        public static Error SessionNotScheduled(Guid sessionId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(SessionNotScheduled)}",
                $"Session '{sessionId}' not found in participant's schedule");

        public static Error SessionAlreadyScheduled(Guid sessionId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(SessionAlreadyScheduled)}",
                $"Session '{sessionId}' already exists in participant's schedule");

        public static Error CannotHaveTwoOrMoreOverlappingSessions(DateOnly date, TimeRange timeRange) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
                $"A participant cannot have two or more overlapping sessions '{date}', '{timeRange}'");
    }
}
