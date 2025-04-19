using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

public static partial class DomainErrors
{
    public static class UserErrors
    {
        public static Error AdminAlreadyCreated(Guid userId, Guid adminId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AdminAlreadyCreated)}",
                $"User '{userId}' already has a admin profile '{adminId}'");

        public static Error TrainerAlreadyCreated(Guid userId, Guid trainerId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(TrainerAlreadyCreated)}",
                $"User '{userId}' already has a trainer profile '{trainerId}'");

        public static Error ParticipantAlreadyCreated(Guid userId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(ParticipantAlreadyCreated)}",
                $"User '{userId}' already has a participant profile '{participantId}'");
    }
}

// AlreadyExist         vs    NotFound
// AlreadyCreated
//
// Max{N}sNotExceeded   vs    Max{N}xExceeded
// TimeSlotOverlapped