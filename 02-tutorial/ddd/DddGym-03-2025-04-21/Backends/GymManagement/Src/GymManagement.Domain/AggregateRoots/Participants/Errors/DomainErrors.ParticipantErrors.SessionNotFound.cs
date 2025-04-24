using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Participants.Errors;

public static partial class DomainErrors
{
    public static partial class ParticipantErrors
    {
        public static Error SessionNotFound(Guid participantId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(SessionNotFound)}",
                $"Session '{sessionId}' not found in participant's schedule '{participantId}'");
    }
}
