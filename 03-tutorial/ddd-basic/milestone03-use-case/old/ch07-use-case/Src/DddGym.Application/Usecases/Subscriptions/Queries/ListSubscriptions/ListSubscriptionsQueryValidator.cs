using FluentValidation;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

internal sealed class ListSubscriptionsQueryValidator : AbstractValidator<ListSubscriptionsQuery>
{
    public ListSubscriptionsQueryValidator()
    {
    }
}
