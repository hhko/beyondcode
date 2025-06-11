using GymDdd.Framework.BaseTypes.Errors;
using LanguageExt.Common;

namespace GymManagement.Adapters.Persistence.Repositories.UsersRepositories.Errors;

internal static partial class PersistenceErrors
{
    internal static partial class UsersRepositoryErrors
    {
        internal static Error EmailNotFound(string email) =>
            ErrorCodeFactory.Create(
                $"{nameof(PersistenceErrors)}.{nameof(UsersRepositoryErrors)}.{nameof(EmailNotFound)}",
                $"User '{email}' is not found");
    }
}
