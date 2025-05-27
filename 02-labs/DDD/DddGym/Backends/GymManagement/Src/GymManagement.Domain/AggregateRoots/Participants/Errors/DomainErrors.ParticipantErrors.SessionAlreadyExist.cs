using GymDdd.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Participants.Errors;

public static partial class DomainErrors
{
    public static partial class ParticipantErrors
    {
        public static Error SessionAlreadyExist(Guid participantId, Guid sessionId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(ParticipantErrors)}.{nameof(SessionAlreadyExist)}",
                $"Session '{sessionId}' already exists in participant's schedule '{participantId}'");
    }
}
