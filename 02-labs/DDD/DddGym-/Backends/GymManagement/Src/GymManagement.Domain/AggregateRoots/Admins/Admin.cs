using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Subscriptions;
using LanguageExt;
using static GymManagement.Domain.AggregateRoots.Admins.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Admins.Events.DomainEvents;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.AggregateRoots.Admins;

public sealed class Admin : AggregateRoot
{
    public Guid UserId { get; }

    public Option<Guid> SubscriptionId { get; private set; }

    private Admin(
        Guid userId,
        Option<Guid> subscriptionId,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    public static Admin Create(
        Guid userId,
        Option<Guid> subscriptionId = default,
        Option<Guid> id = default)
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

        Fin<Unit> EnsureSubscriptionNotSet(Option<Guid> subscriptionId) =>
            subscriptionId.IsNone
                ? AdminErrors.SubscriptionAlreadySet(Id, (Guid)subscriptionId)
                : unit;

        Fin<Unit> ApplySubscription(Subscription newSubscription)
        {
            SubscriptionId = newSubscription.Id;
            _domainEvents.Add(new AdminEvents.SubscriptionSetEvent(this, newSubscription));

            return unit;
        }

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
}