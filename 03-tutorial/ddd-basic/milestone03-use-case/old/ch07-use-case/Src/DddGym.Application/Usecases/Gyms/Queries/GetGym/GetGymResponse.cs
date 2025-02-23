using DddGym.Application.Abstractions.BaseTypes.Cqrs;
using DddGym.Domain.AggregateRoots.Gyms;

namespace DddGym.Application.Usecases.Gyms.Queries.GetGym;

public sealed record GetGymResponse(
    Gym gym)
    : IResponse;