using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListSessions;

public sealed record ListSessionsQuery(
    Guid GymId,
    DateTime? StartDateTime = null,
    DateTime? EndDateTime = null,
    List<SessionCategory>? Categories = null)
    : IQuery2<ListSessionsResponse>;