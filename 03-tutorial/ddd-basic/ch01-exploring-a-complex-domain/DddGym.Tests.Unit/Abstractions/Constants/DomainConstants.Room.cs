namespace DddGym.Tests.Unit.Abstractions.Constants;

public static partial class DomainConstants
{
    public static class Room
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySessions = Subscription.MaxDailySessionsFreeTier;
    }
}