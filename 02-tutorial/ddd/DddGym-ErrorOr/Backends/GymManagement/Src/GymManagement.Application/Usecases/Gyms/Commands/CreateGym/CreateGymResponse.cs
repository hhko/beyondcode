using DddGym.Framework.BaseTypes.Cqrs;
using GymManagement.Domain.AggregateRoots.Gyms;

namespace GymManagement.Application.Usecases.Gyms.Commands.CreateGym;

internal sealed record CreateGymResponse(
    Gym gym)
    : IResponse;