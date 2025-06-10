﻿using DddGym.Domain.AggregateRoots.Rooms;
using DddGym.Domain.AggregateRoots.Sessions;
using DddGym.Tests.Unit.LayerTests.Domain.Constants;
using DddGym.Tests.Unit.LayerTests.Domain.Factories;
using ErrorOr;
using Shouldly;
using static DddGym.Domain.AggregateRoots.Rooms.Errors.DomainErrors;
using static DddGym.Tests.Unit.Abstractions.Constants.AssemblyConstants;

namespace DddGym.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class RoomTests
{
    // 규칙
    //  방은 구독(구독 등급)이 허용하는 개수보다 더 많은 세션을 가질 수 없다.
    //  A room cannot have more sessions than the subscription allows
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
        ErrorOr<Success> lastScheduleSessionResult = scheduleSessionResults[^1];
        lastScheduleSessionResult.IsError.ShouldBeTrue();
        lastScheduleSessionResult.FirstError.ShouldBe(ScheduleSessionErrors.CannotHaveMoreSessionThanSubscriptionAllows);
    }

    // 규칙
    //  방은 두 개 이상의 겹치는 세션을 가질 수 없다.
    //  A room cannot have two or more overlapping sessions
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    [InlineData(1, 3, 0, 4)]
    public void ScheduleSession_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        Room sut = RoomFactory.CreateRoom();

        Session session1 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        Session session2 = SessionFactory.CreateSession(
            date: DomainConstants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var addSession1Result = sut.ScheduleSession(session1);
        var addSession2Result = sut.ScheduleSession(session2);

        // Assert
        addSession1Result.IsError.ShouldBeFalse();

        addSession2Result.IsError.ShouldBeTrue();
        addSession2Result.FirstError.ShouldBe(ScheduleSessionErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}