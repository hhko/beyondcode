using GymDdd.Framework.BaseTypes;
using LanguageExt.Common;

namespace GymManagement.Domain.AggregateRoots.Gyms.Errors;

public static partial class DomainErrors
{
    public static partial class GymErrors
    {
        public static Error MaxRoomsExceeded(Guid gymId, int numRooms, int maxRooms) =>
            ErrorCodeFactory.Create(
                $"{nameof(DomainErrors)}.{nameof(GymErrors)}.{nameof(MaxRoomsExceeded)}",
                $"A gym '{gymId}' cannot have more rooms '{numRooms}' than the subscription allows '{maxRooms}'");
    }
}
