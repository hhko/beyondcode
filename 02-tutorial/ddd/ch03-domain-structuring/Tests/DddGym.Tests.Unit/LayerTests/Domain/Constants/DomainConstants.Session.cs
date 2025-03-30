using DddGym.Domain.Abstractions.ValueObjects;

namespace DddGym.Tests.Unit.LayerTests.Domain.Constants;

public static partial class DomainConstants
{
    public static class Session
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);

        public static readonly TimeRange Time = new(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9));

        public const int MaxParticipants = 10;
    }
}