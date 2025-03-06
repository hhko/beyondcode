using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Users.Queries.Login;

public sealed record LoginResponse(
    User User,
    string Token)
    : IResponse;