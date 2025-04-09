using GymManagement.Domain.AggregateRoots.Sessions;

namespace GymManagement.Adapters.Presentation.Abstractions;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => throw new NotImplementedException();
}
