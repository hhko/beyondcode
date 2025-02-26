namespace DddGym.Domain.AggregateRoots.Sessions;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}