using DddGym.Framework.BaseTypes;
using GymManagement.Domain.AggregateRoots.Users.Events;
using LanguageExt;
using static GymManagement.Domain.AggregateRoots.Users.Errors.DomainErrors;

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

    public Fin<Guid> CreateAddminProfile()
    {
        if (AdminId is not null)
        {
            return UserErrors.AlreadyExistAdminProfile;
        }

        AdminId = Guid.NewGuid();

        _domainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));

        return AdminId.Value;
    }

    public Fin<Guid> CreateParticipantProfile()
    {
        if (ParticipantId is not null)
        {
            return UserErrors.AlreadyExistParticipantProfile;
        }

        ParticipantId = Guid.NewGuid();

        _domainEvents.Add(new ParticipantProfileCreatedEvent(Id, ParticipantId.Value));

        return ParticipantId.Value;
    }

    public Fin<Guid> CreateTrainerProfile()
    {
        if (TrainerId is not null)
        {
            return UserErrors.AlreadyExistTrainerProfile;
        }

        TrainerId = Guid.NewGuid();

        _domainEvents.Add(new TrainerProfileCreatedEvent(Id, TrainerId.Value));

        return TrainerId.Value;
    }
}