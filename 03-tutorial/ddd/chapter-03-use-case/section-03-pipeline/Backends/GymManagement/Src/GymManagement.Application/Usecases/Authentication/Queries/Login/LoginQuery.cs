using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Authentication.Queries.Login;

public sealed record LoginQuery(
    string Email,
    string Password) : IQuery<LoginResponse>;