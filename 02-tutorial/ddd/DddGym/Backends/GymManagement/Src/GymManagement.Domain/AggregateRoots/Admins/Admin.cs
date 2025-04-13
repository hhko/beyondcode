using DddGym.Framework.BaseTypes;

using GymManagement.Domain.AggregateRoots.Admins.Events;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using LanguageExt;
using LanguageExt.Common;
using static GymManagement.Domain.AggregateRoots.Admins.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Admins;

public sealed class Admin : AggregateRoot
{
    public Guid UserId { get; }

    public Guid? SubscriptionId { get; private set; }

    private Admin(
        Guid userId,
        Guid? subscriptionId,
        Guid? id) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    public static Admin Create(
        Guid userId,
        Guid? subscriptionId = null,
        Guid? id = null)
    {
        return new Admin(
            userId,
            subscriptionId,
            id);
    }

    // TODO: 존재 이유 ???
    private Admin()
    {
    }

    public Fin<Unit> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
        {
            return AdminErrors.AlreadyExitSubscription;
        }

        SubscriptionId = subscription.Id;

        _domainEvents.Add(new SubscriptionSetEvent(this, subscription));

        return Unit.Default;
    }
}