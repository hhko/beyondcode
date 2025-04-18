using DddGym.Framework.BaseTypes;
using GymManagement.Domain.SharedTypes.ValueObjects;
using LanguageExt.Common;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static class ScheduleErrors
    {
        public static Error CannotHaveTwoOrMoreOverlappingTimeSlot(DateOnly date, TimeRange timeRange) =>
            ErrorCode.Operation(
                $"{nameof(DomainErrors)}.{nameof(Schedule)}.{nameof(CannotHaveTwoOrMoreOverlappingTimeSlot)}",
                $"Schedule cannot have two or more overlapping sessions '{date}', '{timeRange}'");

        public static Error CannotFindTheTimeSlot(DateOnly date, TimeRange timeRange) =>
            ErrorCode.Operation(
                $"{nameof(DomainErrors)}.{nameof(Schedule)}.{nameof(CannotFindTheTimeSlot)}",
                $"The timeslot can not be found in the schedule '{date}', '{timeRange}'");
    }
}
