using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Commands.CreateSession;

public sealed record CreateSessionResponse(
    Session Session)
    : IResponse;