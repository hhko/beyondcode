using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

public static partial class DomainErrors
{
    public static class UserErrors
    {
        public static Error AdminAlreadyPromoted(Guid adminId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AdminAlreadyPromoted)}",
                $"User '{adminId}' already has a admin profile");

        public static Error TrainerAlreadyPromoted(Guid trainerId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(TrainerAlreadyPromoted)}",
                $"User '{trainerId}' already has a trainer profile");

        public static Error ParticipantAlreadyPromoted(Guid participantId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(ParticipantAlreadyPromoted)}",
                $"User '{participantId}' already has a participant profile");
    }
}
