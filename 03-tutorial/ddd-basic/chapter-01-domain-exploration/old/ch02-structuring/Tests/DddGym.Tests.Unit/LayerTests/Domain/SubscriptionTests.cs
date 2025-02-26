using DddGym.Domain.Gyms;
using DddGym.Domain.Subscriptions;
using DddGym.Tests.Unit.LayerTests.Domain.Factories;
using ErrorOr;
using Shouldly;
using static DddGym.Domain.Subscriptions.Errors.DomainErrors;
using static DddGym.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace DddGym.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class SubscriptionTests
{
    // 규칙
    //  구독은 구독(구독 등급)이 허용된 개수보다 더 많은 헬스장을 가질 수 없다.
    //  A subscription cannot have more gyms than the subscription allows
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        Subscription sut = SubscriptionFactory.CreateSubscription();

        List<Gym> gyms = Enumerable.Range(0, sut.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid()))
            .ToList();

        // Act
        List<ErrorOr<Success>> addGymResults = gyms.ConvertAll(sut.AddGym);

        // Assert: 추가 성공 검증(마지막 추가를 제외한 결과)
        IEnumerable<ErrorOr<Success>> allButLastAddGymResults = addGymResults.Take(..^1);
        allButLastAddGymResults.ShouldAllBe(result => !result.IsError);

        // Assert: 추가 실패 검증(마지막 추가 결과)
        ErrorOr<Success> lastAddGymResult = addGymResults[^1];
        lastAddGymResult.IsError.ShouldBeTrue();
        lastAddGymResult.FirstError.ShouldBe(AddGymErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}