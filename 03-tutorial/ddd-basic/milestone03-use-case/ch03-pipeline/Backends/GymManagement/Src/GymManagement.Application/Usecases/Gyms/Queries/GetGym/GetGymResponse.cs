using DddGym.Framework.BaseTypes.Application.Cqrs;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Gyms.Queries.GetGym;

public sealed record GetGymResponse(
    Gym gym)
    : IResponse;