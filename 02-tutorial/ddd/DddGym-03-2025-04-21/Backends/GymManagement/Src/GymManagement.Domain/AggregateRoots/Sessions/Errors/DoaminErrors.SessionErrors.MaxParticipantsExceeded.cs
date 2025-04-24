using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Sessions.Errors;
public static partial class DomainErrors
{
    public static partial class SessionErrors
    {
        public static Error MaxParticipantsExceeded(Guid sessionId, int numParticipants, int maxParticipants) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(SessionErrors)}.{nameof(MaxParticipantsExceeded)}",
                $"A session '{sessionId}' cannot have more participants '{numParticipants}' than the subscription allows '{maxParticipants}'");
    }
}
