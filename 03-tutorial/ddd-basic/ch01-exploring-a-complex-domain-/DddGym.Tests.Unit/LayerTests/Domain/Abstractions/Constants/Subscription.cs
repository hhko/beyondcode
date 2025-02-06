using DddGym.Domain.Subscriptions.Enumerations;

namespace DddGym.Tests.Unit.LayerTests.Domain.Abstractions.Constants;

internal static class Subscription
{
    public static readonly Grade DefaultSubscriptionType = Grade.Free;
    public static readonly Guid Id = Guid.NewGuid();

    public const int MaxSessionsFreeTier = 3;
    public const int MaxRoomsFreeTier = 1;
    public const int MaxGymsFreeTier = 1;
}