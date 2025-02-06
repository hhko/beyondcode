using DddGym.Domain.Rooms.ValueObjects;

namespace DddGym.Tests.Unit.Abstractions.Constants;

public static partial class DomainConstants
{
    public static class Session
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);
        public static readonly TimeRange Time = new(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9));

        //public const int MaxParticipants = 10;
    }
}