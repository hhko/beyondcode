using GymDdd.Framework.BaseTypes.Errors;
using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static partial class ScheduleErrors
    {
        public static Error TimeSlotNotFound(DateOnly date, TimeSlot timeSlot) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(TimeSlotNotFound)}",
                $"The timeslot can not be found in the schedule '{date}', '{timeSlot}'");
    }
}
