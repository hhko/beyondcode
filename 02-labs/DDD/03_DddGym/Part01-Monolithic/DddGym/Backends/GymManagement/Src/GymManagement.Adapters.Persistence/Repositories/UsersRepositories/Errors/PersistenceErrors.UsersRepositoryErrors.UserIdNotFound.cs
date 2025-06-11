using GymDdd.Framework.BaseTypes.Errors;
using LanguageExt.Common;

namespace GymManagement.Adapters.Persistence.Repositories.UsersRepositories.Errors;

internal static partial class PersistenceErrors
{
    internal static partial class UsersRepositoryErrors
    {
        internal static Error UserIdNotFound(Guid userId) =>
            ErrorCodeFactory.Create(
                $"{nameof(PersistenceErrors)}.{nameof(UsersRepositoryErrors)}.{nameof(UserIdNotFound)}",
                $"User '{userId}' is not found");
    }
}
