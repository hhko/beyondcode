using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Users.Commands.Register;

public sealed record RegisterResponse(
    User User,
    string Token)
    : IResponse;