using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Queries.GetGym;

public sealed record GetGymQuery(
    Guid SubscriptionId,
    Guid GymId)
    : IQuery2<GetGymResponse>;