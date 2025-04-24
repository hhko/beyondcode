using static GymManagement.Tests.Unit.Abstractions.Constants.Constants;

namespace GymManagement.Tests.Unit.LayerTests.Domain;

[Trait(nameof(UnitTest), UnitTest.Domain)]
public sealed class SessionTests
{
    // TODO: LanguageExt
    //// 규칙
    ////  세션은 최대 참가자 수를 초과할 수 없다.
    ////  A session cannot contain more than the maximum number of participants
    //[Fact]
    //public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    //{
    //    // Arrange
    //    int maxParticipants = 1;
    //    var session = SessionFactory.CreateSession(maxParticipants: maxParticipants);

    //    var participants = Enumerable.Range(0, maxParticipants + 1)
    //        .Select(_ => ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.NewGuid()))
    //        .ToList();

    //    // Act
    //    List<ErrorOr<Success>> reserveSpotnResults = participants.ConvertAll(session.ReserveSpot);

    //    // Assert: 추가 성공 검증(마지막 추가를 제외한 결과)
    //    IEnumerable<ErrorOr<Success>> allButLastReserveSpotnResults = reserveSpotnResults.Take(..^1);
    //    allButLastReserveSpotnResults.ShouldAllBe(result => !result.IsError);

    //    // Assert: 추가 실패 검증(마지막 추가 결과)
    //    ErrorOr<Success> lastReserveSpotnResult = reserveSpotnResults[^1];
    //    lastReserveSpotnResult.IsError.ShouldBeTrue();
    //    lastReserveSpotnResult.FirstError.ShouldBe(ReserveSpotErrors.CannotHaveMoreReservationsThanParticipants);
    //}

    //// 규칙
    ////  세션 시작 24시간 이내에는 무료로 예약을 취소할 수 없다.
    ////  A reservation cannot be canceled for free less than 24 hours before the session starts
    //[Fact]
    //public void CancelReservation_WhenCancellationIsTooCloseToSession_ShouldFailCancellation()
    //{
    //    // Arrange
    //    var sut = SessionFactory.CreateSession(
    //        date: DomainConstants.Session.Date,
    //        time: DomainConstants.Session.Time);    // today 08:00 ~ 09:00

    //    var participant = ParticipantFactory.CreateParticipant();
    //    // today 00:00
    //    var cancellationDateTime = DomainConstants.Session.Date.ToDateTime(TimeOnly.MinValue);

    //    // Act
    //    var reserveSpotResult = sut.ReserveSpot(participant);
    //    var cancelReservationResult = sut.CancelReservation(
    //        participant.Id,
    //        new TestDateTimeProvider(fixedDateTime: cancellationDateTime));

    //    // Assert
    //    reserveSpotResult.IsError.ShouldBeFalse();

    //    cancelReservationResult.IsError.ShouldBeTrue();
    //    cancelReservationResult.FirstError.ShouldBe(CancelReservationErrors.CannotCancelReservationTooCloseToSession);
    //}

    //// 추가: 숨겨진 규칙
    ////  지난 예약은 취소할 수 없다.
    ////  "Past reservations cannot be canceled.
    //[Fact]
    //public void CancelReservation_WhenCancellationIsForPastReservation_ShouldFailCancellation()
    //{
    //    // Arrange
    //    DateOnly yesterday = DomainConstants.Session.Date.AddDays(-1);
    //    DateOnly today = DomainConstants.Session.Date;

    //    var sut = SessionFactory.CreateSession(date: yesterday);
    //    var participant = ParticipantFactory.CreateParticipant();

    //    // Act
    //    var reserveSpotResult = sut.ReserveSpot(participant);
    //    var cancelReservationResult = sut.CancelReservation(
    //        participant.Id,
    //        new TestDateTimeProvider(fixedDateTime: today.ToDateTime(DomainConstants.Session.Time.Start)));

    //    // Assert
    //    reserveSpotResult.IsError.ShouldBeFalse();
    //    cancelReservationResult.IsError.ShouldBeTrue();
    //    cancelReservationResult.FirstError.ShouldBe(CancelReservationErrors.CannotCancelPastSession);
    //}
}