using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListGyms;

public sealed record ListGymsQuery(
    Guid SubscriptionId)
    : IQuery<ListGymsResponse>;
