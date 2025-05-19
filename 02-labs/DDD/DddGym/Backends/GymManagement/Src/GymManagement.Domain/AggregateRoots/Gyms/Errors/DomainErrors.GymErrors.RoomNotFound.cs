using GymDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static partial class GymErrors
    {
        public static Error RoomNotFound(Guid gymId, Guid roomId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(RoomNotFound)}",
                $"Room '{roomId}' does not exist in Gym '{gymId}'");
    }
}
