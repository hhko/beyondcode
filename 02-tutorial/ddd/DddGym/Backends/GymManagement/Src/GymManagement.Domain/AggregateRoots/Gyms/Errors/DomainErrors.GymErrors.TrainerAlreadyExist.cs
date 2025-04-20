using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static partial class GymErrors
    {
        public static Error TrainerAlreadyExist(Guid gymId, Guid trainerId) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(TrainerAlreadyExist)}",
                $"Gym '{gymId}' already has a trainer '{trainerId}'");
    }
}
