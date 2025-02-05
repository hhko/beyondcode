using DddGym.Domain.Gyms;
using DddGym.Domain.Subscriptions.Enumerations;
using ErrorOr;
using static DddGym.Domain.Subscriptions.Errors.DomainErrors;

namespace DddGym.Domain.Subscriptions;

public sealed class Subscription
{
    private readonly Grade _subscriptionType;
    private readonly Guid _adminId;
    private readonly Guid _id;

    private readonly List<Guid> _gymIds = [];
    private readonly int _maxGyms;

    public int Xyz1 { get; set; }

    public int Xyz2 { get; set; }

    public Subscription(
        Grade subscriptionType,
        Guid adminId,
        Guid? id = null)
    {
        _subscriptionType = subscriptionType;
        _adminId = adminId;
        _id = id ?? Guid.NewGuid();                 // TODO: Fast Guid

        _maxGyms = GetMaxGyms();
    }

    public int GetMaxGyms() => _subscriptionType.Name switch
    {
        nameof(Grade.Free) => 1,
        nameof(Grade.Starter) => 1,
        nameof(Grade.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => _subscriptionType.Name switch
    {
        nameof(Grade.Free) => 1,
        nameof(Grade.Starter) => 3,
        nameof(Grade.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => _subscriptionType.Name switch
    {
        nameof(Grade.Free) => 4,
        nameof(Grade.Starter) => int.MaxValue,
        nameof(Grade.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public ErrorOr<Success> AddGym(Gym gym)
    {
        // TODO: IValidator
        if (_gymIds.Contains(gym.Id))
        {
            return Error.Conflict(description: "Gym already exists");
        }

        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionError.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }
}
