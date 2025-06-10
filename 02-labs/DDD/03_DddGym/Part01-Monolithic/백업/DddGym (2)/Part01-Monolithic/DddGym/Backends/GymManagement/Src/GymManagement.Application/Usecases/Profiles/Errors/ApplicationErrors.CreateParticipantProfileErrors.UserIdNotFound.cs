using GymDdd.Framework.BaseTypes.Errors;
using LanguageExt.Common;

namespace GymManagement.Application.Usecases.Profiles.Errors;

internal static partial class ApplicationErrors
{
    internal static partial class CreateParticipantProfileErrors
    {
        internal static Error UserIdNotFound(Guid userId) =>
            ErrorCodeFactory.Create(
                $"{nameof(ApplicationErrors)}.{nameof(CreateParticipantProfileErrors)}.{nameof(UserIdNotFound)}",
                $"User '{userId}' is not found");
    }
}
