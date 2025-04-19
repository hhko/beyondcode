using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static class GymErrors
    {
        public static Error TrainerAlreadyExist(Guid gymId, Guid trainerId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(TrainerAlreadyExist)}",
                $"Gym '{gymId}' already has a trainer '{trainerId}'");

        public static Error RoomAlreadyExist(Guid gymId, Guid roomId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(RoomAlreadyExist)}",
                $"Room '{roomId}' exists in Gym '{gymId}'");

        public static Error RoomNotFound(Guid gymId, Guid roomId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(RoomNotFound)}",
                $"Room '{roomId}' does not exist in Gym '{gymId}'");

        public static Error MaxRoomsExceeded(Guid gymId, int numRooms, int maxRooms) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(MaxRoomsExceeded)}",
                $"A gym '{gymId}' cannot have more rooms '{numRooms}' than the subscription allows '{maxRooms}'");
    }
}
