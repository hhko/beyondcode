using DddGym.Domain.Subscriptions.Enumerations;

namespace DddGym.Tests.Unit.LayerTests.Domain.Constants;

public static partial class DomainConstants
{
    public static class Subscription
    {
        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public static readonly Guid Id = Guid.NewGuid();

        public const int MaxDailySessionsFreeTier = 4;
        public const int MaxRoomsFreeTier = 1;
        public const int MaxGymsFreeTier = 1;
    }
}