using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt.Common;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static class ScheduleErrors
    {
        public static Error TimeSlotOverlapped(DateOnly date, TimeSlot timeSlot) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(TimeSlotOverlapped)}",
                $"Schedule cannot have two or more overlapping sessions '{date}', '{timeSlot}'");

        public static Error DateNotFound(DateOnly date) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(DateNotFound)}",
                $"The timeslot can not be found in the schedule '{date}'");

        public static Error TimeSlotNotFound(DateOnly date, TimeSlot timeSlot) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(TimeSlotNotFound)}",
                $"The timeslot can not be found in the schedule '{date}', '{timeSlot}'");
    }
}
