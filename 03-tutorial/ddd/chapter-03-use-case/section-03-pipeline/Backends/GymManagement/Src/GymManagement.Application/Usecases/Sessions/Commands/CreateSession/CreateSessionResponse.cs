using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

public sealed record CreateSessionResponse(
    Session Session)
    : IResponse;