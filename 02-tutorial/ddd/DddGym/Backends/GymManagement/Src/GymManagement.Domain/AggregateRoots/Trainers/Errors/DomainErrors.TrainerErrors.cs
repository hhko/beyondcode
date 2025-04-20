using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static class TrainerErrors
    {
        public static Error SessionNotFound(Guid trainerId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionNotFound)}",
                $"Session '{sessionId}' not found in trainer's schedule '{trainerId}'");

        public static Error SessionAlreadyExist(Guid trainerId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' already exists in trainer's schedule '{trainerId}'");

        public static Error SessionNotScheduled() =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionNotScheduled)}",
                $"A trainer cannot schedule a session");
    }
}
