using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListGyms;

public sealed record ListGymsResponse(
    List<Gym> gyms)
    : IResponse;
