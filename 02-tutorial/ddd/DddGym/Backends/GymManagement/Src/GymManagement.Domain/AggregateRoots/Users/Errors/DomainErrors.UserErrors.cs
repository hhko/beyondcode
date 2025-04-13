using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

public static partial class DomainErrors
{
    public static class UserErrors
    {
        public static readonly Error AlreadyExistAdminProfile = ErrorCode.Validation(
            $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AlreadyExistAdminProfile)}",
            "User already has a admin profile");

        public static readonly Error AlreadyExistTrainerProfile = ErrorCode.Validation(
            $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AlreadyExistTrainerProfile)}",
            "User already has a trainer profile");

        public static readonly Error AlreadyExistParticipantProfile = ErrorCode.Validation(
            $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(AlreadyExistParticipantProfile)}",
            "User already has a participant profile");
    }
}
