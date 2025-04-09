using DddGym.Framework.BaseTypes;

using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Domain.AggregateRoots.Subscriptions.Enumerations;
using GymManagement.Domain.AggregateRoots.Subscriptions.Events;
using LanguageExt;
using LanguageExt.Common;
using static GymManagement.Domain.AggregateRoots.Subscriptions.Errors.DomainErrors;

namespace GymManagement.Domain.AggregateRoots.Subscriptions;

// Subscription
//  [ ] Create: 생성자
//  [ ] Create: Factory 패턴 + Error 연동
// _gymIds
//  [x] Add
//  [ ] Can
//  [x] Has
//  [ ] Remove
//  [ ] Get_1
//  [ ] Get_N
public sealed class Subscription : AggregateRoot
{
    private readonly Guid _adminId;
    private readonly List<Guid> _gymIds = [];
    private readonly int _maxGyms;

    // ---------------------

    // 변경: private readonly SubscriptionType _subscriptionType;
    private SubscriptionType SubscriptionType { get; }

    // ---------------------

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        SubscriptionType = subscriptionType;
        _adminId = adminId;

        _maxGyms = GetMaxGyms();
    }

    // TODO: 존재 이유 ???
    private Subscription()
    {
    }

    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public Fin<Unit> AddGym(Gym gym)
    {
        // TODO: IValidator

        // 규칙 생략: Id 중복
        if (_gymIds.Contains(gym.Id))
        {
            return Error.New("Gym already exists in subscription");
        }

        // 규칙
        //  구독은 구독(구독 등급)이 허용된 개수보다 더 많은 헬스장을 가질 수 없다.
        //  A subscription cannot have more gyms than the subscription allows
        if (_gymIds.Count >= _maxGyms)
        {
            return AddGymErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        _domainEvents.Add(new GymAddedEvent(this, gym));

        return Unit.Default;
    }

    // 추가
    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }
}