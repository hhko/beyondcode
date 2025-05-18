using FunctionalDdd.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Gyms;
using GymManagement.Domain.AggregateRoots.Subscriptions.Enumerations;
using LanguageExt;
using static GymManagement.Domain.AggregateRoots.Subscriptions.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Subscriptions.Events.DomainEvents;
using static LanguageExt.Prelude;

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

    private SubscriptionType SubscriptionType { get; }

    private Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        SubscriptionType = subscriptionType;
        _adminId = adminId;

        _maxGyms = GetMaxGyms();
    }

    private Subscription()
    {
    }

    public static Subscription Create(
        SubscriptionType subscriptionType,
        Guid adminId,
        Option<Guid> id = default)
    {
        return new Subscription(subscriptionType, adminId, id);
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
        return from _1 in EnsureGymNotFound(gym.Id)
               from _2 in EnsureMaxGymsNotExceeded()
               from _3 in ApplyGymAddition(gym)
               select unit;

        Fin<Unit> EnsureGymNotFound(Guid gymId) =>
            _gymIds.Contains(gymId)
                ? SubscriptionErrors.GymAlreadyExist(Id, gymId)
                : unit;

        Fin<Unit> EnsureMaxGymsNotExceeded() =>
            (_gymIds.Count >= _maxGyms)
                ? SubscriptionErrors.MaxGymsExceeded(Id, _maxGyms)
                : unit;

        Fin<Unit> ApplyGymAddition(Gym gym)
        {
            _gymIds.Add(gym.Id);
            _domainEvents.Add(new SubscriptionEvents.GymAddedEvent(this, gym));

            return unit;
        }

        // =========================================
        // Monad 스타일
        // =========================================

        //return EnsureGymNotAdded(gym.Id)
        //    .Bind(_ => EnsureMaxGymsNotExceeded())
        //    .Bind(_ => RegisterGym(gym));

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        // 규칙 생략: Id 중복
        //if (_gymIds.Contains(gym.Id))
        //{
        //    return Error.New("Gym already exists in subscription");
        //}

        // 규칙
        //  구독은 구독(구독 등급)이 허용된 개수보다 더 많은 헬스장을 가질 수 없다.
        //  A subscription cannot have more gyms than the subscription allows
        //if (_gymIds.Count >= _maxGyms)
        //{
        //    return SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows(_maxGyms);
        //}
        //
        //_gymIds.Add(gym.Id);
        //
        //_domainEvents.Add(new GymAddedEvent(this, gym));
        //
        //return Unit.Default;
    }

    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }
}