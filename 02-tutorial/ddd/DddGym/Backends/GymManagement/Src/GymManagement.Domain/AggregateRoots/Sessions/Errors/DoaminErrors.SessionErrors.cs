using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;
public static partial class DomainErrors
{
    public static class SessionErrors
    {
        public static Error ParticipantAlreadyExist(Guid sessionId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ParticipantAlreadyExist)}",
                $"Participant '{participantId}' already exists in session's reservation '{sessionId}'");

        public static Error ParticipantNotFound(Guid sessionId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ParticipantNotFound)}",
                $"Participant '{participantId}' not found in session's reservation '{sessionId}'");

        public static Error MaxParticipantsExceeded(Guid sessionId, int numParticipants, int maxParticipants) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(MaxParticipantsExceeded)}",
                $"A session '{sessionId}' cannot have more participants '{numParticipants}' than the subscription allows '{maxParticipants}'");

        public static Error ReservationInPast(Guid sessionId, DateTime reservationDateTime, DateTime utcNow) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ReservationInPast)}",
                $"A participant cannot cancel the reservation '{reservationDateTime}' for a session '{sessionId}' that has completed '{utcNow}'");

        public static Error ReservationTooClose(Guid sessionId, DateTime reservationDateTime, DateTime utcNow) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ReservationTooClose)}",
                $"A participant Cannot cancel the reservation '{reservationDateTime}' too close to session '{sessionId}' start time '{utcNow}'");
    }
}
