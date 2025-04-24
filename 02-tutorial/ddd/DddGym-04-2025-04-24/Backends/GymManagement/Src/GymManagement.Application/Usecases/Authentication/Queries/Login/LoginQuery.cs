using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Authentication.Queries.Login;

public sealed record LoginQuery(
    string Email,
    string Password) : IQuery2<LoginResponse>;