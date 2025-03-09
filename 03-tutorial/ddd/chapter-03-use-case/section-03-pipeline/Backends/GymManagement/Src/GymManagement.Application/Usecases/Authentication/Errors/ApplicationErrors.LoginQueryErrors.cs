using ErrorOr;
using GymManagement.Application.Usecases.Authentication.Queries.Login;

namespace GymManagement.Application.Usecases.Authentication.Errors;

public static partial class ApplicationErrors
{
    public static class LoginQueryErrors
    {
        public static readonly Error InvalidCredentials = Error.Validation(
            code: $"{nameof(ApplicationErrors)}.{nameof(LoginQuery)}.{nameof(InvalidCredentials)}",
            description: "Invalid credentials");
    }
}
