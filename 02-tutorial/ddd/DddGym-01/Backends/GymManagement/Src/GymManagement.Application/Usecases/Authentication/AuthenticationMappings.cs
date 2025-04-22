using GymManagement.Application.Usecases.Authentication.Commands.Register;
using GymManagement.Application.Usecases.Authentication.Queries.Login;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication;

internal static class AuthenticationMappings
{
    public static LoginResponse ToResponse(this User user, string token)
    {
        return new LoginResponse(user, token);
    }

    public static RegisterResponse ToResponseRegistered(this User user, string token)
    {
        return new RegisterResponse(user, token);
    }
}
