using DddGym.Framework.BaseTypes;
using LanguageExt.Common;
using static GymManagement.Domain.AggregateRoots.Gyms.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Rooms.Errors;

public static partial class DomainErrors
{
    public static class RoomErrors
    {
        public static Error SessionAlreadyExist(Guid roomId, Guid sessionId) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(RoomErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' exists in Room '{roomId}'");
    }
}
