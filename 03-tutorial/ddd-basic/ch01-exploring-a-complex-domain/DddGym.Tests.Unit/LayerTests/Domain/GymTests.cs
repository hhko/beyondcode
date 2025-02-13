using DddGym.Domain.Gyms;
using DddGym.Tests.Unit.LayerTests.Domain.Factories;
using ErrorOr;
using Shouldly;
using static DddGym.Domain.Gyms.Errors.DomainErrors;
using static DddGym.Tests.Unit.Abstractions.Constants.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public class GymTests
{
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
        ErrorOr<Success> lastAddRoomResult = addRoomResults[addRoomResults.Count - 1];
        lastAddRoomResult.IsError.ShouldBeTrue();
        lastAddRoomResult.FirstError.ShouldBe(AddRoomErrors.CannotHaveMoreRoomsThanSubscriptionAllows);
    }
}