using FunctionalDdd.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Sessions.Queries.GetSession;

public sealed record GetSessionResponse(
    Session Session) : IResponse;