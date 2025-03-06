using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Queries.GetGym;

public sealed record GetGymQuery(
    Guid SubscriptionId,
    Guid GymId)
    : IQuery<GetGymResponse>;