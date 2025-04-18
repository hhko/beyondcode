using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static class TrainerErrors
    {
        public static Error SessionNotScheduled(Guid sessionId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionNotScheduled)}",
                $"Session '{sessionId}' not found in trainer's schedule");

        public static Error SessionAlreadyScheduled(Guid sessionId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionAlreadyScheduled)}",
                $"Session '{sessionId}' already exists in trainer's schedule");

        public static Error CannotHaveTwoOrMoreOverlappingSessions(DateOnly date, TimeRange timeRange) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(CannotHaveTwoOrMoreOverlappingSessions)}",
                $"A trainer cannot have two or more overlapping sessions '{date}', '{timeRange}'");
    }
}
