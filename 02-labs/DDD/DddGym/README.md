

## UserManagement 서비스
```
Domain
  Common
    Interfaces
      IPasswordHasher                         // TODO
  UserAggregate
    User.cs
    Events
      AdminProfileCreatedEvent.cs
      ParticipantProfileCreatedEvent.cs
      TrainerProfileCreatedEvent.cs

Application
  Common
    Interfaces
      IUsersRepository.cs
      IJwtTokenGenerator.cs
  Profiles
    CreateAdminProfile
    CreateParticipantProfile
    CreateTrainerProfile
    ListProfiles
  Authentication
    Common                                    // TODO
    Register
    Login
```