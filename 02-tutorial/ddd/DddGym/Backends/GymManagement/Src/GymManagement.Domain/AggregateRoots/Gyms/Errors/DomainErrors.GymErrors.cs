using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static class GymErrors
    {
        public static Error TrainerAlreadyExist(Guid gymId, Guid trainerId) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(TrainerAlreadyExist)}",
                $"Gym '{gymId}' already has a trainer '{trainerId}'");

        public static Error RoomAlreadyExist(Guid gymId, Guid roomId) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(RoomAlreadyExist)}",
                $"Room '{roomId}' exists in Gym '{gymId}'");

        public static Error RoomNotFound(Guid gymId, Guid roomId) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(RoomNotFound)}",
                $"Room '{roomId}' does not exist in Gym '{gymId}'");

        public static Error MaxRoomsExceeded(Guid gymId, int maxRooms) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(MaxRoomsExceeded)}",
                $"A gym '{gymId}' cannot have more rooms than the subscription allows '{maxRooms}'");
    }
}
