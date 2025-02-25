using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Tests.Unit.LayerTests.Domain.Factories;
using ErrorOr;
using Shouldly;
using static GymManagement.Domain.AggregateRoots.Gyms.Errors.DomainErrors;
using static GymManagement.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace GymManagement.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class GymTests
{
    // 규칙
    //  헬스장은 구독(구독 등급)이 허용하는 개수보다 더 많은 방을 가질 수 없다.
    //  A gym cannot have more rooms than the subscription allows
    [Fact]
    public void AddRoom_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        int maxRooms = 1;
        Gym sut = GymFactory.CreateGym(maxRooms: maxRooms);

        var rooms = Enumerable.Range(0, maxRooms + 1)
            .Select(_ => RoomFactory.CreateRoom(id: Guid.NewGuid()))
            .ToList();

        // Act
        List<ErrorOr<Success>> addRoomResults = rooms.ConvertAll(sut.AddRoom);

        // Assert: 추가 성공 검증(마지막 추가를 제외한 결과)
        IEnumerable<ErrorOr<Success>> allButLastAddRoomResults = addRoomResults.Take(..^1);
        allButLastAddRoomResults.ShouldAllBe(result => !result.IsError);

        // Assert: 추가 실패 검증(마지막 추가 결과)
        ErrorOr<Success> lastAddRoomResult = addRoomResults[^1];
        lastAddRoomResult.IsError.ShouldBeTrue();
        lastAddRoomResult.FirstError.ShouldBe(AddRoomErrors.CannotHaveMoreRoomsThanSubscriptionAllows);
    }
}