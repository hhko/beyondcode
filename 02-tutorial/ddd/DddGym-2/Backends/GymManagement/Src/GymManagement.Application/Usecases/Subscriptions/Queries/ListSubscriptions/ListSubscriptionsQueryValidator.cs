using FluentValidation;

namespace GymManagement.Application.Usecases.Subscriptions.Queries.ListSubscriptions;

internal sealed class ListSubscriptionsQueryValidator : AbstractValidator<ListSubscriptionsQuery>
{
    public ListSubscriptionsQueryValidator()
    {
    }
}