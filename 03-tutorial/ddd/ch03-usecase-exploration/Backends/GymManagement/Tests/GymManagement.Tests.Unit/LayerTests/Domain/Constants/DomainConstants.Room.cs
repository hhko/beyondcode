namespace GymManagement.Tests.Unit.LayerTests.Domain.Constants;

public static partial class DomainConstants
{
    public static class Room
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySessions = Subscription.MaxDailySessionsFreeTier;
        public const string Name = "Room 1";
    }
}