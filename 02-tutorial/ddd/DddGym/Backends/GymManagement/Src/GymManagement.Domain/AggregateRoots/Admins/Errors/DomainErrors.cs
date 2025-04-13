using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Admins.Errors;

public static partial class DomainErrors
{
    public static class AdminErrors
    {
        public static readonly Error AlreadyExitSubscription = ErrorCode.Validation(
            $"{nameof(DomainErrors)}.{nameof(AdminErrors)}.{nameof(AlreadyExitSubscription)}",
            "Admin already has an active subscription");
    }
}
