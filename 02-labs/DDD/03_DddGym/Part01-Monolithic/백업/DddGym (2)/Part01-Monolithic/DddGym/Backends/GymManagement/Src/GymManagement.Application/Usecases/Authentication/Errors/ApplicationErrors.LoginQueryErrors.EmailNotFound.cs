using GymDdd.Framework.BaseTypes.Errors;
using LanguageExt.Common;

namespace GymManagement.Application.Usecases.Authentication.Errors;

public static partial class ApplicationErrors
{
    public static partial class LoginQueryErrors
    {
        public static Error EmailNotFound(string email) =>
            ErrorCodeFactory.Create(
                $"{nameof(ApplicationErrors)}.{nameof(LoginQueryErrors)}.{nameof(EmailNotFound)}",
                $"User '{email}' is not found");
    }
}
