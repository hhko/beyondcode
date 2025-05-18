using GymManagement.Domain.AggregateRoots.Admins;
using GymManagement.Tests.Unit.LayerTests.Domain.Constants;
using LanguageExt;

namespace GymManagement.Tests.Unit.LayerTests.Domain.Factories;

internal static class AdminFactory
{
    public static Admin CreateAdmin(
        Option<Guid> userId = default,
        Option<Guid> subscriptionId = default,
        Option<Guid> id = default)
    {
        return Admin.Create(
            userId: userId.IfNone(DomainConstants.User.Id),
            subscriptionId: subscriptionId.IfNone(DomainConstants.Subscription.Id),
            id: id.IfNone(DomainConstants.Admin.Id));
    }
}