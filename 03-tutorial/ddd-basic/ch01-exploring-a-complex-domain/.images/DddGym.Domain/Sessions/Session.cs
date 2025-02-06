using DddGym.Domain.Rooms.ValueObjects;

namespace DddGym.Domain.Sessions;

public sealed class Session
{
    public Guid Id { get; }

    public DateOnly Date { get; }

    public TimeRange Time { get; }

    public Session(
        DateOnly date,
        TimeRange time,
        //int maxParticipants,
        //Guid trainerId,
        Guid? id = null)
    {
        Date = date;
        Time = time;
        //_maxParticipants = maxParticipants;
        //_trainerId = trainerId;
        Id = id ?? Guid.NewGuid();
    }
}
