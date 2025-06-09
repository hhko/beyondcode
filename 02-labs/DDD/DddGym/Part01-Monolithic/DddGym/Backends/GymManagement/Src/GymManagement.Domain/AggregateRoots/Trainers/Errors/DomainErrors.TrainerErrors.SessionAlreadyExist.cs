using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static partial class TrainerErrors
    {
        public static Error SessionAlreadyExist(Guid trainerId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' already exists in trainer's schedule '{trainerId}'");
    }
}
