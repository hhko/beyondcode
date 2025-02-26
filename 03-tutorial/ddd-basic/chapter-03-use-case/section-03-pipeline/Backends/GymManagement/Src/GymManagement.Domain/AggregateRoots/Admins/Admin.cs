using GymManagement.Domain.AggregateRoots.Admins.Events;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using ErrorOr;
using DddGym.Framework.BaseTypes.Domain;

namespace GymManagement.Domain.AggregateRoots.Admins;

public sealed class Admin : AggregateRoot
{
    public Guid UserId { get; }

    public Guid? SubscriptionId { get; private set; }

    public Admin(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    // TODO: 존재 이유 ???
    private Admin()
    {
    }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
        {
            return Error.Conflict(description: "Admin already has an active subscription");
        }

        SubscriptionId = subscription.Id;

        _domainEvents.Add(new SubscriptionSetEvent(this, subscription));

        return Result.Success;
    }
}