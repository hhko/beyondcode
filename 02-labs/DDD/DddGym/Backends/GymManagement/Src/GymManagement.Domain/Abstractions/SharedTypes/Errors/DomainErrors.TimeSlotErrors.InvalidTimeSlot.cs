using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static class TimeSlotErrors
    {
        public static Error InvalidTimeSlot(TimeOnly start, TimeOnly end) =>
            ErrorCodeFactory.Create(
                $"",
                $"Start time '{start}' must be earlier than the end time '{end}'.");
    }
}
