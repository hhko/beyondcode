using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

internal static partial class DomainErrors
{
    internal static partial class UserErrors
    {
        internal static Error TrainerAlreadyCreated(Guid userId, Guid trainerId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(TrainerAlreadyCreated)}",
                $"User '{userId}' already has a trainer profile '{trainerId}'");
    }
}