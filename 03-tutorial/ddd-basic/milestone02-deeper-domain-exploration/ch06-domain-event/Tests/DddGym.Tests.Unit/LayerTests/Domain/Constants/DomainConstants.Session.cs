using DddGym.Domain.Abstractions.ValueObjects;
using DddGym.Domain.AggregateRoots.Sessions.Enumerations;

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

        public static readonly List<SessionCategory> Categories = [];

        public const int MaxParticipants = 10;
        public const string Name = "Zoomba Session";
        public const string Description = "The best zoomba yay";
    }
}