namespace DddGym.Domain.Sessions;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}