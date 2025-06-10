using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using Throw;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class TimeRangeFactory
{
    public static TimeSlot CreateFromHours(int startHour, int endHour)
    {
        startHour.Throw()
            .IfGreaterThanOrEqualTo(endHour)
            .IfNegative()
            .IfGreaterThan(23);

        endHour.Throw()
            .IfLessThan(1)
            .IfGreaterThan(24);

        return (TimeSlot)TimeSlot.Create(
            start: TimeOnly.MinValue.AddHours(startHour),
            end: TimeOnly.MinValue.AddHours(endHour));
    }
}