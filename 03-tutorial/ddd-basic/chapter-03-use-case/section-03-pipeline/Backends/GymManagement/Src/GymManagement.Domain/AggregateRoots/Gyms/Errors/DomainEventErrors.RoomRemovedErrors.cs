using DddGym.Framework.BaseTypes.Application.Events;
using ErrorOr;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static class DomainEventErrors
{
    public static class RoomRemovedErrors
    {
        public static readonly Error RoomNotFound = DomainEventError.From(
            code: $"{nameof(DomainEventErrors)}.{nameof(Gym)}.{nameof(RoomNotFound)}",
            description: "Room not found");
    }
}
