using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Admins.Errors;

public static partial class DomainErrors
{
    public static class AdminErrors
    {
        public static Error AlreadyExitSubscription(Guid subscriptionId) =>
            ErrorCode.Validation(
                $"{nameof(DomainErrors)}.{nameof(AdminErrors)}.{nameof(AlreadyExitSubscription)}",
                $"Admin '{subscriptionId}' already has an active subscription");
    }
}
