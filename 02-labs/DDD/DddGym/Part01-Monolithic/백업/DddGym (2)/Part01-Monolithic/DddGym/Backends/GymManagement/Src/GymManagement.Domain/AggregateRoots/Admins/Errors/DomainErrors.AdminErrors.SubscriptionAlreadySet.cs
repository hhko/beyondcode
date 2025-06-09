﻿using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Admins.Errors;

public static partial class DomainErrors
{
    public static class AdminErrors
    {
        public static Error SubscriptionAlreadySet(Guid adminId, Guid subscriptionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(AdminErrors)}.{nameof(SubscriptionAlreadySet)}",
                $"Admin '{adminId}' already has an active subscription ''{subscriptionId}");
    }
}
