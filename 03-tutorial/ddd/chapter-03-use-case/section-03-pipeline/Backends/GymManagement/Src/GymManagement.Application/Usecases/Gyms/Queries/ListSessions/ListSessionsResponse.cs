using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListSessions;

public sealed record ListSessionsResponse(
    List<Session> Sessions)
    : IResponse;