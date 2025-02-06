namespace DddGym.Domain.Gyms;

public sealed class Gym
{
    //private readonly Guid _subscriptionId;
    //private readonly int _maxRooms;
    //private readonly List<Guid> _roomIds = new();

    public Guid Id { get; }

    public Gym(
        //int maxRooms,
        //Guid subscriptionId,
        Guid? id = null)
    {
        //_maxRooms = maxRooms;
        //_subscriptionId = subscriptionId;
        Id = id ?? Guid.NewGuid();
    }
}
