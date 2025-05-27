using GymDdd.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static partial class TrainerErrors
    {
        public static Error SessionNotFound(Guid trainerId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionNotFound)}",
                $"Session '{sessionId}' not found in trainer's schedule '{trainerId}'");
    }
}
