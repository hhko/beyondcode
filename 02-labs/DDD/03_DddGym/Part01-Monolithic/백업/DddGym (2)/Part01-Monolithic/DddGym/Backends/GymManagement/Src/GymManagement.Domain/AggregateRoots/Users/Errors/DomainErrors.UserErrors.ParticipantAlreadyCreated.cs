using GymDdd.Framework.BaseTypes.Errors;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

internal static partial class DomainErrors
{
    internal static partial class UserErrors
    {
        internal static Error ParticipantAlreadyCreated(Guid userId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(ParticipantAlreadyCreated)}",
                $"User '{userId}' already has a participant profile '{participantId}'");
    }
}