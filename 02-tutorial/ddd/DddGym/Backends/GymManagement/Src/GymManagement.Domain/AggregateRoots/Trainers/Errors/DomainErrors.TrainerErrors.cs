using DddGym.Framework.BaseTypes;
using LanguageExt.Common;
using static GymManagement.Domain.AggregateRoots.Users.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static class TrainerErrors
    {
        public static readonly Error SessionNotFound = ErrorCode.Validation(
            $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionNotFound)}",
            "Session not found in trainer's schedule");
    }
}
