using GymManagement.Application.Usecases.Users.Commands.Register;
using GymManagement.Application.Usecases.Users.Queries.Login;
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
