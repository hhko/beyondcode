using DddGym.Framework.BaseTypes.Application.Cqrs;

namespace GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed record ListSubscriptionsQuery() 
    : IQuery<ListSubscriptionsResponse>;