using GymDdd.Framework.BaseTypes;

namespace GymManagement.Domain.AggregateRoots.Users.Errors;

public static partial class DomainErrors
{
    public static partial class UserErrors
    {
        public static Error ParticipantAlreadyCreated(Guid userId, Guid participantId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(UserErrors)}.{nameof(ParticipantAlreadyCreated)}",
                $"User '{userId}' already has a participant profile '{participantId}'");
    }
}