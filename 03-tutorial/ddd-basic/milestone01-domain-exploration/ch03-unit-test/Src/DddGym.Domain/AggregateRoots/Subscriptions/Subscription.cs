using DddGym.Domain.AggregateRoots.Gyms;
using DddGym.Domain.AggregateRoots.Subscriptions.Enumerations;
using ErrorOr;
using static DddGym.Domain.AggregateRoots.Subscriptions.Errors.DomainErrors;

namespace DddGym.Domain.AggregateRoots.Subscriptions;

public sealed class Subscription
{
    private readonly SubscriptionType _subscriptionType;
    private readonly Guid _adminId;
    private readonly Guid _id;                      // TODO: public Guid Id { get; }

    private readonly List<Guid> _gymIds = [];
    private readonly int _maxGyms;

    public Subscription(
        SubscriptionType subscriptionType,
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
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => _subscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public ErrorOr<Success> AddGym(Gym gym)
    {
        // TODO: IValidator

        // 규칙 생략: Id 중복
        if (_gymIds.Contains(gym.Id))
        {
            return Error.Conflict(description: "Gym already exists in subscription");
        }

        // 규칙
        //  구독은 구독(구독 등급)이 허용된 개수보다 더 많은 헬스장을 가질 수 없다.
        //  A subscription cannot have more gyms than the subscription allows
        if (_gymIds.Count >= _maxGyms)
        {
            return AddGymErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }
}