using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Gyms;

namespace DddGym.Application.Usecases.Gyms.Queries.ListGyms;

public sealed record ListGymsResponse(
    List<Gym> gyms)
    : IResponse;
