# Usecase Exploration

## Usecase

| No | Service            | Use case           | AggregateRoot          | Category            | Name                           |
|----|--------------------|--------------------|-------------------     |---------------------|--------------------------------|
| 1  | GymManagement      | Admins             | Admin <- User          | Events(Integration) | AdminProfileCreatedEvent       |
| 2  | UserManagement     | **Authentication** | **User**               | Commands            | Register                       |
| 3  | UserManagement     | **Authentication** | **User**               | Queries             | Login                          |
| 4  | GymManagement      | Gyms               | Gym                    | Commands            | AddTrainer                     |
| 5  | GymManagement      | Gyms               | Gym                    | Commands            | CreateGym                      |
| 6  | GymManagement      | Gyms               | Gym <- Subscription    | Events              | GymAddedEvent                  |
| 7  | SessionReservation | Gyms               | Gym <- Room            | Events(Integration) | SessionScheduledEvent          |
| 8  | GymManagement      | Gyms               | Gym                    | Queries             | GetGym                         |
| 9  | GymManagement      | Gyms               | Gym                    | Queries             | ListGyms                       |
| 10 | SessionReservation | Gyms               | Gym                    | Queries             | ListSessions                   |
| 11 | SessionReservation | Participants       | Participant            | Commands            | CancelReservation              |
| 12 | SessionReservation | Participants       | Participant <- Session | Events              | ReservationCanceledEvent       |
| 13 | SessionReservation | Participants       | Participant <- Session | Events              | SessionCanceledEvent           |
| 14 | SessionReservation | Participants       | Participant <- Session | Events              | SessionSpotReservedEvent       |
| 15 | SessionReservation | Participants       | Participant <- User    | Events(Integration) | ParticipantProfileCreatedEvent |
| 16 | SessionReservation | Participants       | Participant            | Queries             | ListParticipantSessions        |
| 17 | UserManagement     | **Profiles**       | **User**               | Commands            | CreateAdminProfile             |
| 18 | UserManagement     | **Profiles**       | **User**               | Commands            | CreateParticipantProfile       |
| 19 | UserManagement     | **Profiles**       | **User**               | Commands            | CreateTrainerProfile           |
| 20 | UserManagement     | **Profiles**       | **User**               | Queries             | ListProfiles                   |
| 21 | SessionReservation | **Reservations**   | **Session**            | Commands            | CreateReservation              |
| 22 | GymManagement      | Rooms              | Room                   | Commands            | CreateRoom                     |
| 23 | GymManagement      | Rooms              | Room                   | Commands            | DeleteRoom                     |
| 24 | SessionReservation | Rooms              | Room <- Gym            | Events(Integration) | RoomAddedEvent                 |
| 25 | SessionReservation | Rooms              | Room <- Gym            | Events(Integration) | RoomRemovedEvent               |
| 26 | SessionReservation | Rooms              | Room                   | Queries             | GetRoom                        |
| 27 | SessionReservation | Rooms              | Room                   | Queries             | ListRooms                      |
| 28 | SessionReservation | Sessions           | Session                | Commands            | CreateSession                  |
| 29 | SessionReservation | Sessions           | Session <- Room        | Events              | SessionScheduledEvent          |
| 30 | SessionReservation | Sessions           | Session <- Gym         | Events(Integration) | RoomRemovedEvent               |
| 31 | SessionReservation | Sessions           | Session                | Queries             | GetSession                     |
| 32 | GymManagement      | Subscriptions      | Subscription           | Commands            | CreateSubscription             |
| 33 | GymManagement      | Subscriptions      | Subscription <- Admin  | Events              | SubscriptionSetEvent           |
| 34 | GymManagement      | Subscriptions      | Subscription           | Queries             | ListSubscriptions              |
| 35 | SessionReservation | Trainers           | Trainer <- Session     | Events              | SessionCanceledEvent           |
| 36 | SessionReservation | Trainers           | Trainer <- Room        | Events              | SessionScheduledEvent          |
| 37 | SessionReservation | Trainers           | Trainer <- User        | Events(Integration) | TrainerProfileCreatedEvent     |

- Service
  - GymManagement
  - SessionReservation
  - UserManagement
- Actor
  - User
  - Admin
  - Participants
  - Trainers
- Message
  - Command: 12개
    - Register
    - AddTrainer
    - CreateGym
    - CancelReservation
    - CreateAdminProfile
    - CreateParticipantProfile
    - CreateTrainerProfile
    - CreateReservation
    - CreateRoom
    - DeleteRoom
    - CreateSession
    - CreateSubscription
  - Query: 10개
    - Login
    - GetGym
    - ListGyms
    - ListSessions
    - ListParticipantSessions
    - ListProfiles
    - GetRoom
    - ListRooms
    - GetSession
    - ListSubscriptions
  - Event: 11개
    - AdminProfileCreatedEvent
    - GymAddedEvent
    - **SessionScheduledEvent(3 = 2 IDomainEvent + 1 IIntegrationEvent)**: The data types of `IDomainEvent` and `IIntegrationEvent` are different.
      ```cs
      // IDomainEvent
      public record SessionScheduledEvent(
        Room Room,
        Session Session)        // <-
        : IDomainEvent;

      // IIntegrationEvent
      public record SessionScheduledIntegrationEvent(
        Guid RoomId,
        Guid TrainerId)         // <- Replace with "Session.TrainerId"
        : IIntegrationEvent;
      ```
    - ReservationCanceledEvent
    - **SessionCanceledEvent(2 = 2 IDomainEvent + 0 IIntegrationEvent)**
    - SessionSpotReservedEvent
    - ParticipantProfileCreatedEvent(Integration)
    - RoomAddedEvent(Integration)
    - **RoomRemovedEvent(2 = 0 IDomainEvent + 2 IIntegrationEvent)**
    - SubscriptionSetEvent
    - TrainerProfileCreatedEvent(Integration)

## Event

| Publisher Service   | Use case      | CQRS                            | Aggregate Root                | -Event->                                        | Receiver Service    | Service       |
|---------------------|---------------|---------------------------------|-------------------------------|-------------------------------------------------|---------------------|---------------|
| UserManagement      | Profiles      | CreateAdminProfileCommand       | user.CreateAdminProfile       | -AdminProfileCreatedEvent(Integration)->        | GymManagement       | Admin         |
| UserManagement      | Profiles      | CreateParticipantProfileCommand | user.CreateParticipantProfile | -ParticipantProfileCreatedEvent(Integration)->  | SessionReservation  | Participant   |
| UserManagement      | Profiles      | CreateTrainerProfileCommand     | user.CreateTrainerProfile     | -TrainerProfileCreatedEvent(Integration)->      | SessionReservation  | Trainer       |
| GymManagement       | Subscriptions | CreateSubscriptionCommand       | admin.SetSubscription         | -SubscriptionSetEvent->                         | GymManagement       | Subscription  |
| GymManagement       | Gyms          | CreateGymCommand                | subscription.AddGym           | -GymAddedEvent->                                | GymManagement       | Gym           |
| GymManagement       | Rooms         | CreateRoomCommand               | gym.AddRoom                   | -RoomAddedEvent(Integration)->                  | SessionReservation  | Room          |
| GymManagement       | Rooms         | DeleteRoomCommand               | gym.RemoveRoom                | -RoomRemovedEvent(Integration)->                | SessionReservation  | Room          |
| GymManagement       | Rooms         | DeleteRoomCommand               | gym.RemoveRoom                | -RoomRemovedEvent(Integration)->                | SessionReservation  | Session       |
| SessionReservation  | Sessions      | CreateSessionCommand            | room.ScheduleSession          | -SessionScheduledEvent(Integration)->           | GymManagement       | Gym           |
| SessionReservation  | Sessions      | CreateSessionCommand            | room.ScheduleSession          | -SessionScheduledEvent->                        | SessionReservation  | Session       |
| SessionReservation  | Sessions      | CreateSessionCommand            | room.ScheduleSession          | -SessionScheduledEvent->                        | SessionReservation  | Trainer       |
| SessionReservation  | Participants  | CancelReservationCommand        | session.CancelReservation     | -ReservationCanceledEvent->                     | SessionReservation  | Participant   |
| SessionReservation  | Sessions      | RoomRemovedEvent                | session.Cancel                | -SessionCanceledEvent->                         | SessionReservation  | Participant   |
| SessionReservation  | Sessions      | RoomRemovedEvent                | session.Cancel                | -SessionCanceledEvent->                         | SessionReservation  | Trainer       |
| SessionReservation  | Reservations  | CreateReservationCommand        | session.ReserveSpot           | -SessionSpotReservedEvent->                     | SessionReservation  | Participant   |


- UserManagement
  - Profiles
    - CreateAdminProfileCommand: user.CreateAdminProfile
      - `-AdminProfileCreatedEvent(Integration)->`
        - GymManagement Admin
    - CreateParticipantProfileCommand: user.CreateParticipantProfile
      - `-ParticipantProfileCreatedEvent(Integration)->`
        - SessionReservation Participant
    - CreateTrainerProfileCommand: user.CreateTrainerProfile
      - `-TrainerProfileCreatedEvent(Integration)->`
        - SessionReservation Trainer
- GymManagement
  - Subscriptions
    - CreateSubscriptionCommand: admin.SetSubscription
      - `-SubscriptionSetEvent->`
        - GymManagement Subscription
  - Gyms
    - CreateGymCommand: subscription.AddGym
      - `-GymAddedEvent->`
        - GymManagement Gym
  - Rooms
    - CreateRoomCommand: gym.AddRoom
      - `-RoomAddedEvent(Integration)->`
        - SessionReservation Room
    - DeleteRoomCommand: gym.RemoveRoom
      - `-RoomRemovedEvent(Integration)->`
        - SessionReservation Room
        - SessionReservation Session
- SessionReservation
  - Sessions
    - CreateSessionCommand: room.ScheduleSession
      - `-SessionScheduledEvent(Integration)->`
        - GymManagement Gym
        - SessionReservation Session
        - SessionReservation Trainer
    - RoomRemovedEvent: session.Cancel
      - `-SessionCanceledEvent->`
        - SessionReservation Participant
        - SessionReservation Trainer
  - Participants
    - CancelReservationCommand: session.CancelReservation
      - `-ReservationCanceledEvent->`
        - SessionReservation Participant
  - Reservations
    - CreateReservationCommand: session.ReserveSpot
      - `-SessionSpotReservedEvent->`
        - SessionReservation Participant