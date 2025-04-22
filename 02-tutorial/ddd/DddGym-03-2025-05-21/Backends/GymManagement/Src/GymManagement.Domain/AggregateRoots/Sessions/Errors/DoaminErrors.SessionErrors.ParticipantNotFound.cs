using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;
public static partial class DomainErrors
{
    public static partial class SessionErrors
    {
        public static Error ParticipantNotFound(Guid sessionId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ParticipantNotFound)}",
                $"Participant '{participantId}' not found in session's reservation '{sessionId}'");
    }
}
