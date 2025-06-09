﻿using DddGym.Framework.BaseTypes;
using ErrorOr;
using GymManagement.Domain.AggregateRoots.Users.Events;

namespace GymManagement.Domain.AggregateRoots.Users;

public sealed class User : AggregateRoot
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;

    public string Email { get; } = null!;
    public Guid? AdminId { get; private set; }
    public Guid? ParticipantId { get; private set; }
    public Guid? TrainerId { get; private set; }

    private readonly string _passwordHash = null!;

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid? adminId = null,
        Guid? participantId = null,
        Guid? trainerId = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;

        AdminId = adminId;
        ParticipantId = participantId;
        TrainerId = trainerId;
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public ErrorOr<Guid> CreateAddminProfile()
    {
        if (AdminId is not null)
        {
            return Error.Conflict(description: "User already has an admin profile");
        }

        AdminId = Guid.NewGuid();

        _domainEvents.Add(new AdminProfileCreatedEvent(Id, AdminId.Value));

        return AdminId.Value;
    }

    public ErrorOr<Guid> CreateParticipantProfile()
    {
        if (ParticipantId is not null)
        {
            return Error.Conflict(description: "User already has a participant profile");
        }

        ParticipantId = Guid.NewGuid();

        _domainEvents.Add(new ParticipantProfileCreatedEvent(Id, ParticipantId.Value));

        return ParticipantId.Value;
    }

    public ErrorOr<Guid> CreateTrainerProfile()
    {
        if (TrainerId is not null)
        {
            return Error.Conflict(description: "User already has a trainer profile");
        }

        TrainerId = Guid.NewGuid();

        _domainEvents.Add(new TrainerProfileCreatedEvent(Id, TrainerId.Value));

        return TrainerId.Value;
    }
}