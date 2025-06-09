using GymManagement.Application.Usecases.Sessions.Ports;

namespace GymManagement.Tests.Unit.Abstractions.Providers;

public sealed class TestDateTimeProvider : IDateTimeProvider
{
    private readonly Option<DateTime> _fixedDateTime;

    public DateTime UtcNow => _fixedDateTime.IfNone(DateTime.UtcNow);

    public TestDateTimeProvider(Option<DateTime> fixedDateTime = default)
    {
        _fixedDateTime = fixedDateTime;
    }
}