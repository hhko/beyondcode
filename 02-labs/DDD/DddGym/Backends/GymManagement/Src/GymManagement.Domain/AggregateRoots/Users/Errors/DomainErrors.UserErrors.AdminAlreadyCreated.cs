using GymDdd.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

public static partial class DomainErrors
{
    public static partial class UserErrors
    {
        public static Error AdminAlreadyCreated(Guid userId, Guid adminId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AdminAlreadyCreated)}",
                $"User '{userId}' already has a admin profile '{adminId}'");
    }
}