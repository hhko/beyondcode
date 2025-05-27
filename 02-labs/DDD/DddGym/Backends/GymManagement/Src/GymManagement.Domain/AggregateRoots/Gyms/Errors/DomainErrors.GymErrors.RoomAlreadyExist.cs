using GymDdd.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static partial class GymErrors
    {
        public static Error RoomAlreadyExist(Guid gymId, Guid roomId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(RoomAlreadyExist)}",
                $"Room '{roomId}' exists in Gym '{gymId}'");
    }
}
