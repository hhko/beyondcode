using GymDdd.Framework.BaseTypes;
using static GymManagement.Domain.AggregateRoots.Users.Errors.DomainErrors;
using static GymManagement.Domain.AggregateRoots.Users.Events.DomainEvents;

namespace GymManagement.Domain.AggregateRoots.Users;

//[GenerateEntityId]
public sealed class User : AggregateRoot
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }

    public Option<Guid> AdminId { get; private set; }
    public Option<Guid> ParticipantId { get; private set; }
    public Option<Guid> TrainerId { get; private set; }

    private User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Option<Guid> adminId,
        Option<Guid> participantId,
        Option<Guid> trainerId,
        Option<Guid> id) : base(id.IfNone(Guid.NewGuid()))
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;

        AdminId = adminId;
        ParticipantId = participantId;
        TrainerId = trainerId;
    }

    private User()
    {
    }

    public static Fin<User> Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Option<Guid> adminId = default,
        Option<Guid> participantId = default,
        Option<Guid> trainerId = default,
        Option<Guid> id = default)
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
    //public async Task<Fin<Unit>> IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    //{
    //    //FinT<IO, bool> usecase = passwordHasher.IsCorrectPassword(password, _passwordHash);
    //    var usecase = from _ in passwordHasher.IsCorrectPassword(password, _passwordHash)
    //                  select _;

    //    return await usecase
    //        .Run()
    //        .RunAsync();
    //}

    //public FinT<IO, Unit> IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    //{
    //    return passwordHasher.IsCorrectPassword(password, _passwordHash);
    //}

    public Fin<Guid> CreateAdminProfile()
    {
        return from _1 in EnsureAdminNotCreated(AdminId)
               let newAdminId = NewAdminId()
               from _2 in ApplyAdminProfileCreation(newAdminId)
               from _3 in RaiseAdminProfileCreatedEvent(newAdminId)
               select newAdminId;

        Fin<Unit> EnsureAdminNotCreated(Option<Guid> adminId) =>
            adminId.IsSome
                ? UserErrors.AdminAlreadyCreated(Id, (Guid)adminId)
                : unit;

        Guid NewAdminId() =>
            Guid.NewGuid();

        Fin<Unit> ApplyAdminProfileCreation(Guid newAdminId)
        {
            AdminId = newAdminId;
            return unit;
        }

        Fin<Unit> RaiseAdminProfileCreatedEvent(Guid newAdminId)
        {
            _domainEvents.Add(new UserEvents.AdminProfileCreatedEvent(Id, newAdminId));
            return unit;
        }
    }

    public Fin<Guid> CreateParticipantProfile()
    {
        return from _1 in EnsureParticipantNotCreated(ParticipantId)
               let newParticipantId = NewParticipantId()
               from _2 in ApplyParticipantProfileCreation(newParticipantId)
               from _3 in RaiseParticipantProfileCreatedEvent(newParticipantId)
               select newParticipantId;

        Fin<Unit> EnsureParticipantNotCreated(Option<Guid> participantId) =>
            participantId.IsSome
                ? UserErrors.ParticipantAlreadyCreated(Id, (Guid)participantId)
                : unit;

        Guid NewParticipantId() =>
            Guid.NewGuid();

        Fin<Unit> ApplyParticipantProfileCreation(Guid newParticipantId)
        {
            ParticipantId = newParticipantId;
            return unit;
        }

        Fin<Unit> RaiseParticipantProfileCreatedEvent(Guid newParticipantId)
        {
            _domainEvents.Add(new UserEvents.ParticipantProfileCreatedEvent(Id, newParticipantId));
            return unit;
        }
    }

    public Fin<Guid> CreateTrainerProfile()
    {
        return from _1 in EnsureTrainerNotCreated(TrainerId)
               let newTrainerId = NewTrainerId()
               from _2 in ApplyTrainerProfileCreation(newTrainerId)
               from _3 in RasieTrainerProfileCreatedEvent(newTrainerId)
               select newTrainerId;

        Fin<Unit> EnsureTrainerNotCreated(Option<Guid> trainerId) =>
            trainerId.IsSome
                ? UserErrors.TrainerAlreadyCreated(Id, (Guid)trainerId)
                : unit;

        Guid NewTrainerId() =>
            Guid.NewGuid();

        Fin<Unit> ApplyTrainerProfileCreation(Guid newTrainerId)
        {
            TrainerId = newTrainerId;
            return unit;
        }

        Fin<Unit> RasieTrainerProfileCreatedEvent(Guid newTrainerId)
        {
            _domainEvents.Add(new UserEvents.TrainerProfileCreatedEvent(Id, newTrainerId));
            return unit;
        }
    }
}

// =========================================
// Monad 스타일
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

//-----------------------------------------------

// =========================================
// Case 2: Monad 스타일
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