using DddGym.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Trainers.Errors;

public static partial class DomainErrors
{
    public static partial class TrainerErrors
    {
        public static Error SessionNotScheduled() =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(TrainerErrors)}.{nameof(SessionNotScheduled)}",
                $"A trainer cannot schedule a session");
    }
}
