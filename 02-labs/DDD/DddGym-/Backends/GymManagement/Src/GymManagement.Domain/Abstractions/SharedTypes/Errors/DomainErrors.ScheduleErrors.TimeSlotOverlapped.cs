using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Domain.Abstractions.SharedTypes.ValueObjects;
using LanguageExt.Common;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static partial class ScheduleErrors
    {
        public static Error TimeSlotOverlapped(DateOnly date, TimeSlot timeSlot) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(TimeSlotOverlapped)}",
                $"Schedule cannot have two or more overlapping sessions '{date}', '{timeSlot}'");
    }
}