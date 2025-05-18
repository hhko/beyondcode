using GymManagement.Application.Usecases.Authentication.Commands;
using GymManagement.Application.Usecases.Authentication.Queries;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication;

internal static class AuthenticationMappings
{
    public static LoginQuery.Response ToLoginResponse(this User user, string token)
    {
        return new LoginQuery.Response(user, token);
    }

    public static RegisterCommand.Response ToRegisterResponse(this User user, string token)
    {
        return new RegisterCommand.Response(user, token);
    }
}
