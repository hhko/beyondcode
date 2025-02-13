using DddGym.Domain.Gyms;
using DddGym.Domain.Subscriptions.Enumerations;
using ErrorOr;
using static DddGym.Domain.Subscriptions.Errors.DomainErrors;

namespace DddGym.Domain.Subscriptions;

public sealed class Subscription
{
    private readonly Grade _grade;
    private readonly Guid _adminId;
    private readonly Guid _id;                      // TODO: public Guid Id { get; }

    private readonly List<Guid> _gymIds = [];
    private readonly int _maxGyms;

    public Subscription(
        Grade grade,
        Guid adminId,
        Guid? id = null)
    {
        _grade = grade;
        _adminId = adminId;
        _id = id ?? Guid.NewGuid();                 // TODO: Fast Guid

        _maxGyms = GetMaxGyms();
    }

    public int GetMaxGyms() => _grade.Name switch
    {
        nameof(Grade.Free) => 1,
        nameof(Grade.Starter) => 1,
        nameof(Grade.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxRooms() => _grade.Name switch
    {
        nameof(Grade.Free) => 1,
        nameof(Grade.Starter) => 3,
        nameof(Grade.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public int GetMaxDailySessions() => _grade.Name switch
    {
        nameof(Grade.Free) => 4,
        nameof(Grade.Starter) => int.MaxValue,
        nameof(Grade.Pro) => int.MaxValue,
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
        // 구독은 구독(구독 등급)이 허용된 개수보다 더 많은 헬스장을 가질 수 없다.
        // A subscription cannot have more gyms than the subscription allows
        if (_gymIds.Count >= _maxGyms)
        {
            return AddGymErrors.CannotHaveMoreGymsThanSubscriptionAllows;
        }

        _gymIds.Add(gym.Id);

        return Result.Success;
    }
}