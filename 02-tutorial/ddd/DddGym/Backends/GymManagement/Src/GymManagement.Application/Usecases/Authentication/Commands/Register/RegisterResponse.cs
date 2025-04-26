using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Users;

namespace GymManagement.Application.Usecases.Authentication.Commands.Register;

public sealed record RegisterResponse(
    User User,
    string Token)
    : IResponse;