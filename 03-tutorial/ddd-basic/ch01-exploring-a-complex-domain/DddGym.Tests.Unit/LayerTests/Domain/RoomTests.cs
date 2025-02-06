using DddGym.Domain.Rooms;
using DddGym.Tests.Unit.Abstractions.Factories;
using ErrorOr;
using Shouldly;
using static DddGym.Domain.Rooms.Errors.DomainErrors;
using static DddGym.Tests.Unit.Abstractions.Constants.Constants;

namespace DddGym.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public class RoomTests
{
    [Fact]
    public void ScheduleSession_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        int maxDailySessions = 1;
        Room sut = RoomFactory.CreateRoom(maxDailySessions: maxDailySessions);

        var sessions = Enumerable.Range(0, maxDailySessions + 1)
            .Select(_ => SessionFactory.CreateSession(id: Guid.NewGuid()))
            .ToList();

        // Act
        List<ErrorOr<Success>> scheduleSessionResults = sessions.ConvertAll(sut.ScheduleSession);

        // Assert: 추가 성공 검증(마지막 추가를 제외한 결과)
        IEnumerable<ErrorOr<Success>> allButLastScheduleSession = scheduleSessionResults.Take(..^1);
        allButLastScheduleSession.ShouldAllBe(result => !result.IsError);

        // Assert: 추가 실패 검증(마지막 추가 결과)
        ErrorOr<Success> lastScheduleSessionResult = scheduleSessionResults[scheduleSessionResults.Count - 1];
        lastScheduleSessionResult.IsError.ShouldBeTrue();
        lastScheduleSessionResult.FirstError.ShouldBe(ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows);
    }
}
