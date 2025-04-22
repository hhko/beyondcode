using GymManagement.Application.Usecases.Sessions.Ports;

namespace GymManagement.Adapters.Presentation.Abstractions;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => throw new NotImplementedException();
}
