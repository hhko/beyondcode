using FunctionalDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;
public static partial class DomainErrors
{
    public static partial class SessionErrors
    {
        public static Error ParticipantAlreadyExist(Guid sessionId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(ParticipantAlreadyExist)}",
                $"Participant '{participantId}' already exists in session's reservation '{sessionId}'");
    }
}
