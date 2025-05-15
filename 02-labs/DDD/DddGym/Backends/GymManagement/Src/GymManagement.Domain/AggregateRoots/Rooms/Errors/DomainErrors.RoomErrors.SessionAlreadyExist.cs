using FunctionalDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainErrors
{
    public static partial class RoomErrors
    {
        public static Error SessionAlreadyExist(Guid roomId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(RoomErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' exists in Room '{roomId}'");
    }
}
