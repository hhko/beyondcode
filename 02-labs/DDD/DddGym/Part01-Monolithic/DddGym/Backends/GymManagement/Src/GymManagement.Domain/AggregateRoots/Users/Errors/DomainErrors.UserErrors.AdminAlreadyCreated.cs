using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

internal static partial class DomainErrors
{
    internal static partial class UserErrors
    {
        internal static Error AdminAlreadyCreated(Guid userId, Guid adminId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AdminAlreadyCreated)}",
                $"User '{userId}' already has a admin profile '{adminId}'");
    }
}