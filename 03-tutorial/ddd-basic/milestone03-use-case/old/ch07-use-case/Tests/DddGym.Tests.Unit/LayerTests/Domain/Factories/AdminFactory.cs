using DddGym.Domain.AggregateRoots.Admins;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain.Factories;

internal static class AdminFactory
{
    public static Admin CreateAdmin(
        Guid? userId = null,
        Guid? subscriptionId = null,
        Guid? id = null)
    {
        return new Admin(
            userId: userId ?? DomainConstants.User.Id,
            //subscriptionId ?? DomainConstants.Subscription.Id,
            subscriptionId: subscriptionId,
            id: id ?? DomainConstants.Admin.Id);
    }
}
