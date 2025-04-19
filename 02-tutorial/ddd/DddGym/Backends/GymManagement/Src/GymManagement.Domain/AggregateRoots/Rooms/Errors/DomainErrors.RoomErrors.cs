using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Sessions;
using LanguageExt.Common;
using static GymManagement.Domain.AggregateRoots.Gyms.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainErrors
{
    public static class RoomErrors
    {
        public static Error SessionAlreadyExist(Guid roomId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(RoomErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' exists in Room '{roomId}'");

        public static Error MaxRoomsExceeded(Guid roomId, int numSessions, int maxSessions) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(RoomErrors)}.{nameof(MaxRoomsExceeded)}",
                $"A room '{roomId}' cannot have more sessions '{numSessions}' than the subscription allows '{maxSessions}'");
    }
}
