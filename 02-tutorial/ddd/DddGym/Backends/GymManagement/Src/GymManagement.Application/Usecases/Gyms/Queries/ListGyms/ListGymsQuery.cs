using FunctionalDdd.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Gyms.Queries.ListGyms;

public sealed record ListGymsQuery(
    Guid SubscriptionId)
    : IQuery2<ListGymsResponse>;