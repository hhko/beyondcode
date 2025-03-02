using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Users.Queries.Login;

public sealed record LoginQuery(
    string Email,
    string Password) : IQuery<LoginResponse>;