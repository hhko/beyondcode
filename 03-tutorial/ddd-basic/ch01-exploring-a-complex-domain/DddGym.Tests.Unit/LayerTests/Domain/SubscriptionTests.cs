using DddGym.Domain.Subscriptions;
using DddGym.Tests.Unit.LayerTests.Domain.Abstractions.Factories;
using Shouldly;
using static DddGym.Domain.Subscriptions.Errors.DomainErrors;
using static DddGym.Tests.Unit.Abstractions.Constants.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Subscription sut = SubscriptionFactory.CreateSubscription();

        var gyms = Enumerable.Range(0, sut.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // Act
        var addGymResults = gyms.ConvertAll(sut.AddGym);

        // Assert: 마지막 추가를 제외한 결과
        var allButLastAddGymResults = addGymResults.Take(..^1);
        allButLastAddGymResults.ShouldAllBe(result => !result.IsError);

        // Assert: 마지막 추가 결과
        var lastAddGymResult = addGymResults.Last();
        lastAddGymResult.IsError.ShouldBeTrue();
        lastAddGymResult.FirstError.ShouldBe(SubscriptionError.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}
