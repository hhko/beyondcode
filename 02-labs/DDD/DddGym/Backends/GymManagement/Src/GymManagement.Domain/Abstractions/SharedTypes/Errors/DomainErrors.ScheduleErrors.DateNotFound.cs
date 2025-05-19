using GymDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.SharedTypes.Errors;

public static partial class DomainErrors
{
    public static partial class ScheduleErrors
    {
        public static Error DateNotFound(DateOnly date) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ScheduleErrors)}.{nameof(DateNotFound)}",
                $"The timeslot can not be found in the schedule '{date}'");
    }
}