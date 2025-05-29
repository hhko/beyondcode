using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

public static partial class DomainErrors
{
    public static partial class UserErrors
    {
        public static Error TrainerAlreadyCreated(Guid userId, Guid trainerId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(TrainerAlreadyCreated)}",
                $"User '{userId}' already has a trainer profile '{trainerId}'");
    }
}