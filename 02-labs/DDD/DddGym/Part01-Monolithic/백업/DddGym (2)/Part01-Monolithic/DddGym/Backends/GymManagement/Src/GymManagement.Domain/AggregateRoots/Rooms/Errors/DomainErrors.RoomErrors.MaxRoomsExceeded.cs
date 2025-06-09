using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainErrors
{
    public static partial class RoomErrors
    {
        public static Error MaxRoomsExceeded(Guid roomId, int numSessions, int maxSessions) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(RoomErrors)}.{nameof(MaxRoomsExceeded)}",
                $"A room '{roomId}' cannot have more sessions '{numSessions}' than the subscription allows '{maxSessions}'");
    }
}
