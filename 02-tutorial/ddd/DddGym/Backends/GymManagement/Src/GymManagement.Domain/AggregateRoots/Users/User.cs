using DddGym.Framework.BaseTypes;
using LanguageExt;
using System.Diagnostics.Contracts;
using static GymManagement.Domain.AggregateRoots.Users.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;
using static LanguageExt.Prelude;

namespace GymManagement.Domain.AggregateRoots.Users;

public sealed class User : AggregateRoot
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }

    public Guid? AdminId { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public Guid? TrainerId { get; private set; }

    private readonly string _passwordHash;

    private User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid? adminId,
        Guid? participantId,
        Guid? trainerId,
        Guid? id) : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;

        AdminId = adminId;
        ParticipantId = participantId;
        TrainerId = trainerId;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid? adminId = null,
        Guid? participantId = null,
        Guid? trainerId = null,
        Guid? id = null)
    {
        return new User(
            firstName,
            lastName,
            email,
            passwordHash,
            adminId,
            participantId,
            trainerId,
            id);
    }


    // TODO?: IPasswordHasher 인터페이스를 도메인 레이어에서 정의해야 하나?
    // TODO?: 이 함수의 구현 위치가 도메인 레이어???
    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public Fin<Guid> CreateAdminProfile()
    {
        // =========================================
        // Monadic LINQ 스타일
        // =========================================

        return from _1 in EnsureAdminNotCreated(AdminId)
               from newAdminId in Pure(NewAdminId())            // Map
               from _2 in ApplyAdminProfile(newAdminId)         // Bind
               select newAdminId;

        // =========================================
        // Monadic 스타일
        // =========================================

        //return EnsureAdminNotCreated(AdminId)
        //    .Map(_ => NewAdminId())
        //    .Bind(newAdminId => ApplyAdminProfile(newAdminId));

        // =========================================
        // Imperative Guard 스타일
        // =========================================

        //if (AdminId is not null)
        //{
        //    return UserErrors.AlreadyExistAdminProfile(AdminId.Value);
        //}
        //
        //AdminId = Guid.NewGuid();
        //
        //_domainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));
        //
        //return AdminId.Value;
    }

    [Pure]
    private Fin<Unit> EnsureAdminNotCreated(Guid? adminId) =>
        !adminId.HasValue
            ? unit
            : UserErrors.AdminAlreadyCreated(Id, adminId.Value);

    [Pure]
    private Guid NewAdminId() =>
        Guid.NewGuid();

    private Fin<Guid> ApplyAdminProfile(Guid newAdminId)
    {
        AdminId = newAdminId;
        _domainEvents.Add(new UserEvents.AdminProfileCreatedEvent(Id, newAdminId));

        return newAdminId;
    }

    public Fin<Guid> CreateParticipantProfile()
    {
        // =========================================
        // Case 3. Monadic LINQ 스타일
        // =========================================

        return from _1 in EnsureParticipantNotCreated(ParticipantId)
               from newParticipantId in Pure(NewParticipantId())
               from _2 in ApplyParticipantProfile(newParticipantId)
               select newParticipantId;

        // =========================================
        // Case 1. Imperative Guard 스타일
        // =========================================

        //if (ParticipantId is not null)
        //{
        //    return UserErrors.AlreadyExistParticipantProfile(ParticipantId.Value);
        //}
        //
        //ParticipantId = Guid.NewGuid();
        //
        //_domainEvents.Add(new ParticipantProfileCreatedEvent(Id, ParticipantId.Value));
        //
        //return ParticipantId.Value;
    }

    [Pure]
    private Fin<Unit> EnsureParticipantNotCreated(Guid? participantId) =>
        !participantId.HasValue
            ? unit
            : UserErrors.ParticipantAlreadyCreated(Id, participantId.Value);

    [Pure]
    private Guid NewParticipantId() =>
        Guid.NewGuid();

    private Fin<Guid> ApplyParticipantProfile(Guid newParticipantId)
    {
        ParticipantId = newParticipantId;
        _domainEvents.Add(new UserEvents.ParticipantProfileCreatedEvent(Id, newParticipantId));

        return newParticipantId;
    }

    public Fin<Guid> CreateTrainerProfile()
    {
        // =========================================
        // Case 3. Monadic LINQ 스타일
        // =========================================

        //return from _ in TrainerId is null 
        //            ? Fin<Unit>.Succ(unit)
        //            : UserErrors.AlreadyExistTrainerProfile(TrainerId.Value)
        //       from newTrainerId in Pure(NewTrainerId())
        //       from _2 in SetTrainerProfile(newTrainerId)
        //       select newTrainerId;

        return from _1 in EnsureTrainerNotCreated(TrainerId)
               from newTrainerId in Pure(NewTrainerId())
               from _2 in ApplyTrainerProfile(newTrainerId)
               select newTrainerId;

        // =========================================
        // Case 2: Monadic 스타일
        // =========================================

        //return EnsureTrainerNotExist(TrainerId)
        //    .Map(_ => NewTrainerId())
        //    .Bind(newTrainerId => SetTrainerProfile(newTrainerId));

        // =========================================
        // Case 1: Imperative Guard 스타일
        // =========================================

        //if (TrainerId is not null)
        //    return UserErrors.AlreadyExistTrainerProfile(TrainerId.Value);
        //
        //Guid newTrainerId = Guid.NewGuid();
        //
        //TrainerId = newTrainerId;
        //_domainEvents.Add(new TrainerProfileCreatedEvent(Id, newTrainerId));
        //
        //return TrainerId.Value;
    }

    [Pure]
    private Fin<Unit> EnsureTrainerNotCreated(Guid? trainerId) =>
      !trainerId.HasValue
          ? unit
          : UserErrors.TrainerAlreadyCreated(Id, trainerId.Value);

    [Pure]
    private Guid NewTrainerId() =>
        Guid.NewGuid();

    // Map은 순수한 값 변환용 함수 (T -> R)에 쓰입니다.
    // 그러나 ApplyTrainerProfile은 내부 상태를 변경하는 부수 효과 함수 (T -> Fin<R>)이기 때문에 Bind가 더 적절합니다.
    //  - Map: 순수 함수
    //  - Bind: 부수 효과 함수
    //
    // X: .Map(id => ApplyTrainerProfile(id)     : private Guid ApplyTrainerProfile(Guid newTrainerId)
    // O: .Bind(id => ApplyTrainerProfile(id)    : private Fin<Guid> ApplyTrainerProfile(Guid newTrainerId)
    private Fin<Guid> ApplyTrainerProfile(Guid newTrainerId)
    {
        // TrainerId 설정과 이벤트 발생은 불가분의 도메인 행동이다
        //
        // "프로필을 생성한다"는 하나의 도메인 행동이자,
        // 그 결과로 TrainerId가 할당되고 이벤트가 생성되는 것은 불가분 관계입니다.

        TrainerId = newTrainerId;
        _domainEvents.Add(new UserEvents.TrainerProfileCreatedEvent(Id, newTrainerId));

        return newTrainerId;
    }
}