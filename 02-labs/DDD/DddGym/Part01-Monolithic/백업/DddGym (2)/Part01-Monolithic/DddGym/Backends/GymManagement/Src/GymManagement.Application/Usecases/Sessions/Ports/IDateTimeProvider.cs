namespace GymManagement.Application.Usecases.Sessions.Ports;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}