using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Sessions.Enumerations;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListSessionse;

public sealed record ListSessionsQuery(
    Guid GymId,
    DateTime? StartDateTime = null,
    DateTime? EndDateTime = null,
    List<SessionCategory>? Categories = null)
    : IQuery<ListSessionsResponse>;