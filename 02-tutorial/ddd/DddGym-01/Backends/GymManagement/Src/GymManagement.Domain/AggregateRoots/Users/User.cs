using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Users.Events;
using LanguageExt;
using System.Diagnostics.Contracts;
using static GymManagement.Domain.AggregateRoots.Users.Errors.DomainErrors;
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

    // CreateAddminProfile  : 기술적 용어
    // PromoteToAddmin      : 비즈니스적 용어(기존 사용자을 Admin으로 승격)
    public Fin<Guid> PromoteToAddmin()
    {
        // =========================================
        // Monadic LINQ 스타일
        // =========================================

        return from _1 in EnsureAdminNotPromoted(AdminId)
               from newAdminId in Pure(NewAdminId())
               from _2 in ApplyAdminPromotion(newAdminId)
               select newAdminId;

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

    // EnsureAddminNotExist     : 기술적 용어
    // EnsureAdminNotPromoted   : 비즈니스적 용어
    [Pure]
    private Fin<Unit> EnsureAdminNotPromoted(Guid? adminId) =>
        !adminId.HasValue
            ? unit
            : UserErrors.AdminAlreadyPromoted(adminId.Value);

    [Pure]
    private Guid NewAdminId() =>
        Guid.NewGuid();

    private Fin<Guid> ApplyAdminPromotion(Guid newAdminId)
    {
        AdminId = newAdminId;
        _domainEvents.Add(new AdminPromotedEvent(Id, newAdminId));

        return newAdminId;
    }

    // CreateParticipantProfile : 기술적 용어
    // PromoteToParticipant     : 비즈니스적 용어(기존 사용자을 Participant으로 승격)
    public Fin<Guid> PromoteToParticipant()
    {
        // =========================================
        // Monadic LINQ 스타일
        // =========================================

        return from _1 in EnsureParticipantNotPromoted(ParticipantId)
               from newParticipantId in Pure(NewParticipantId())
               from _2 in ApplyParticipantPromotion(newParticipantId)
               select newParticipantId;

        // =========================================
        // Imperative Guard 스타일
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

    // EnsureParticipantNotExist    : 기술적 용어
    // EnsureParticipantNotPromoted : 비즈니스적 용어
    [Pure]
    private Fin<Unit> EnsureParticipantNotPromoted(Guid? participantId) =>
        !participantId.HasValue
            ? unit
            : UserErrors.ParticipantAlreadyPromoted(participantId.Value);

    [Pure]
    private Guid NewParticipantId() =>
        Guid.NewGuid();

    private Fin<Guid> ApplyParticipantPromotion(Guid newParticipantId)
    {
        ParticipantId = newParticipantId;
        _domainEvents.Add(new ParticipantPromotedEvent(Id, newParticipantId));

        return newParticipantId;
    }

    // CreateTrainerProfile : 기술적 용어
    // PromoteToTrainer     : 비즈니스적 용어(기존 사용자을 Trainer로 승격)
    public Fin<Guid> PromoteToTrainer()
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

        return from _1 in EnsureTrainerNotPromoted(TrainerId)
               from newTrainerId in Pure(NewTrainerId())
               from _2 in ApplyTrainerPromotion(newTrainerId)
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

    // EnsureTrainerNotExist    : 기술적 용어
    // EnsureTrainerNotPromoted : 비즈니스적 용어
    [Pure]
    private Fin<Unit> EnsureTrainerNotPromoted(Guid? trainerId) =>
      !trainerId.HasValue
          ? unit
          : UserErrors.TrainerAlreadyPromoted(trainerId.Value);

    [Pure]
    private Guid NewTrainerId() =>
        Guid.NewGuid();

    // Map은 순수한 값 변환용 함수 (T -> R)에 쓰입니다.
    // 그러나 SetTrainerProfile은 내부 상태를 변경하는 부수 효과 함수 (T -> Fin<R>)이기 때문에 Bind가 더 적절합니다.
    //  - Map: 순수 함수
    //  - Bind: 부수 효과 함수
    //
    // X: .Map(id => SetTrainerProfile(id)     : private Guid SetTrainerProfile(Guid newTrainerId)
    // O: .Bind(id => SetTrainerProfile(id)    : private Fin<Guid> SetTrainerProfile(Guid newTrainerId)
    private Fin<Guid> ApplyTrainerPromotion(Guid newTrainerId)
    {
        // TrainerId 설정과 이벤트 발생은 불가분의 도메인 행동이다
        //
        // "프로필을 생성한다"는 하나의 도메인 행동이자,
        // 그 결과로 TrainerId가 할당되고 이벤트가 생성되는 것은 불가분 관계입니다.

        TrainerId = newTrainerId;
        _domainEvents.Add(new TrainerPromotedEvent(Id, newTrainerId));

        return newTrainerId;
    }
}