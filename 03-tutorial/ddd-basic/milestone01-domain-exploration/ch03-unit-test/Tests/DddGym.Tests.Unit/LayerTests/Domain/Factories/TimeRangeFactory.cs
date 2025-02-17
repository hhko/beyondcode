using DddGym.Domain.Abstractions.ValueObjects;
using Throw;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

public static class TimeRangeFactory
{
    public static TimeRange CreateFromHours(int startHour, int endHour)
    {
        startHour.Throw()
            .IfGreaterThanOrEqualTo(endHour)
            .IfNegative()
            .IfGreaterThan(23);

        endHour.Throw()
            .IfLessThan(1)
            .IfGreaterThan(24);

        return new TimeRange(
            start: TimeOnly.MinValue.AddHours(startHour),
            end: TimeOnly.MinValue.AddHours(endHour));
    }
}