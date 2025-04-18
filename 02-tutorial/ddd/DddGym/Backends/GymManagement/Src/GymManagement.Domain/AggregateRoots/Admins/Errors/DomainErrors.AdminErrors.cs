using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Admins.Errors;

public static partial class DomainErrors
{
    public static class AdminErrors
    {
        public static Error SubscriptionAlreadySet(Guid adminId, Guid subscriptionId) =>
            ErrorCodeFactory.New(
                $"{nameof(DomainErrors)}.{nameof(AdminErrors)}.{nameof(SubscriptionAlreadySet)}",
                $"Admin '{adminId}' already has an active subscription ''{subscriptionId}");
    }
}
