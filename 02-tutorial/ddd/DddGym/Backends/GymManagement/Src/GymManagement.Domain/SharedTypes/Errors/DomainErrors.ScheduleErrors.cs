using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt.Common;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static class ScheduleErrors
    {
        public static Error TimeSlotOverlapped(DateOnly date, TimeSlot timeRange) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(Schedule)}.{nameof(TimeSlotOverlapped)}",
                $"Schedule cannot have two or more overlapping sessions '{date}', '{timeRange}'");

        public static Error TimeSlotNotFound(DateOnly date, TimeSlot timeRange) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(Schedule)}.{nameof(TimeSlotNotFound)}",
                $"The timeslot can not be found in the schedule '{date}', '{timeRange}'");
    }
}
