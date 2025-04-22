using DddGym.Framework.BaseTypes.Cqrs;

namespace GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record ListSubscriptionsQuery()
    : IQuery2<ListSubscriptionsResponse>;