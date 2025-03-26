using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Queries.Login;

public sealed record LoginResponse(
    User User,
    string Token)
    : IResponse;