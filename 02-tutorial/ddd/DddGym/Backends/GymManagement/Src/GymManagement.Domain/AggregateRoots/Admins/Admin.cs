using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using LanguageExt;
using System.Diagnostics.Contracts;
using static GymManagement.Domain.AggregateRoots.Admins.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Admins.Events.DomainEvents;
using static LanguageExt.Prelude;

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
        return new Admin(userId, subscriptionId, id);
    }

    private Admin()
    {
    }

    public Fin<Unit> SetSubscription(Subscription subscription)
    {
        return from _1 in EnsureSubscriptionNotSet(SubscriptionId)
               from _2 in ApplySubscription(subscription)
               select unit;

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (SubscriptionId.HasValue)
        //{
        //    return AdminErrors.AlreadyExitSubscription;
        //}
        //
        //SubscriptionId = subscription.Id;
        //
        //_domainEvents.Add(new SubscriptionSetEvent(this, subscription));
        //
        //return Unit.Default;
    }

    [Pure]
    private Fin<Unit> EnsureSubscriptionNotSet(Guid? subscriptionId) =>
        !subscriptionId.HasValue
            ? unit
            : AdminErrors.SubscriptionAlreadySet(Id, subscriptionId.Value);

    private Fin<Unit> ApplySubscription(Subscription newSubscription)
    {
        SubscriptionId = newSubscription.Id;
        _domainEvents.Add(new AdminEvents.SubscriptionSetEvent(this, newSubscription));

        return unit;
    }
}