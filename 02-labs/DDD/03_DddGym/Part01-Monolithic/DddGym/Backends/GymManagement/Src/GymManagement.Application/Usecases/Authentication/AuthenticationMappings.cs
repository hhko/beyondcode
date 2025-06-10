using GymManagement.Application.Usecases.Authentication.Commands;
using GymManagement.Application.Usecases.Authentication.Queries;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication;

internal static class AuthenticationMappings
{
    //public static async ValueTask<Fin<RegisterCommand.Response>> ToRegisterResponse(this FinT<IO, (User User, string Token)> usecase)
    //{
    //    var result = await usecase
    //        .Run()
    //        .RunAsync();

    //    return result.Match(
    //        Succ: _ => new RegisterCommand.Response(_.User, _.Token),
    //        Fail: Fin<RegisterCommand.Response>.Fail);
    //}

    public static Fin<RegisterCommand.Response> ToRegisterResponse(this Fin<(User User, string Token)> result)
    {
        return result.Match(
            Succ: _ => new RegisterCommand.Response(_.User, _.Token),
            Fail: Fin<RegisterCommand.Response>.Fail);
    }

    public static async ValueTask<Fin<LoginQuery.Response>> ToLoginResponse(this FinT<IO, (User User, string Token)> usecase)
    {
        var result = await usecase
            .Run()
            .RunAsync();

        return result.Match(
            Succ: _ => new LoginQuery.Response(_.User, _.Token),
            Fail: Fin<LoginQuery.Response>.Fail);
    }
}
