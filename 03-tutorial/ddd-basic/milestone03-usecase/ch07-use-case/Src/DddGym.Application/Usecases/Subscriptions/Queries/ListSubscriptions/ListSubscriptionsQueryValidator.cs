using FluentValidation;

namespace DddGym.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

public sealed class ListSubscriptionsQueryValidator : AbstractValidator<ListSubscriptionsQuery>
{
    public ListSubscriptionsQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
