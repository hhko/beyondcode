[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

> A beautiful journey to writing wise code
> - **`The source code structure`** should be as clear as **`a table of contents in a book`** to help understand the system.
> - **`Test code`** should serve as **`a manual`** for understanding business rules.

<br/>

## Application Architecture

### Internal Architecture(Hexagonal Architecture)
![](./01-architecture/part1-overview/ch04-internal-architecture/.images/Architecture.Internal.png)

### External Architecture
> TODO

<br/>

## Domain-Driven Design Tutorial
> I have restructured it based on **the design principles I defined**, using '[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)' as a foundation.

### Goal
- Understand code structuring for sustainable software development.
- Learn tactical design that express domain knowledge as code.

### Table of Contents
- Part 1. Domain Concern
  - [x] Chapter 01. [Domain Exploration](./03-tutorial/ddd/ch01-domain-exploration/)
  - [x] Chapter 02. [Domain Exploration Structuring](./03-tutorial/ddd/ch02-domain-exploration-structuring/)
  - [ ] Chapter 03. Domain Event Exploration
  - [ ] Chapter 04. Unit Test
- Part 2. Technical Concern
  - [ ] Chapter 05. WebApi
  - [ ] Chapter 06. OpenTelemetry
  - [ ] Chapter 07. PostgreSQL
  - [ ] Chapter 08. Containerization
- Part 3. Microservices Concern
  - [ ] Chapter 09. RabbitMQ
  - [ ] Chapter 10. Scheduler
  - [ ] Chapter 10. Reverse Proxy
  - [ ] Chapter 11. Resilience
- Part 4. Deployment Concern
  - [ ] Chapter 12. Feature Flag Management
  - [ ] Chapter 13. Infrastructure as Code

### Solution Design Principles

1. **Separation**
   - **Concern**: `Business Concern` vs `Technical Concern`
   - **Goal**: `Main Goal` vs `Sub-Goal`(something supplementary to the main goal, 부수 목표: 주가 되는 것에 붙어 따르는 것)
1. **Direction**
   - **Up**: The more important thing from a technical aspect(Sub-Goal)
   - **Down**: The more important thing from a business aspect(Main Goal)

<br/>

> | `Direction` | `Separation` of Concerns    | `Separation` of Goals                     |
> | ---         | ---                         | ---                                       |
> | Up          | Technical Concern(Infinite) | Sub-Goal(Infinite -Abstractions-> Finite) |
> | Down        | Business Concern(Finite)    | Main Goal(Finite)                         |
>
> - To transform the infinite nature of sub-goals into a finite structure, an `Abstractions` top-level folder is introduced, with sub-goals placed in sub-folders beneath it.
> - This ensures a clear separation between sub-goals and the main goal, making all folders, except for the `Abstractions` folder at the top, more intuitively understood as part of the main goal."

```shell
{T}
├─Src
│  ├─{T}                          // Host               > Up: The more important thing from a technical aspect(Sub-Goal)
│  ├─{T}.Adapters.Infrastructure  // Adapter Layer      > │
│  ├─{T}.Adapters.Persistence     // Adapter Layer      > │
│  ├─{T}.Application              // Application Layer  > ↓
│  └─{T}.Domain                   // Domain Layer       > Down: he more important thing from a business aspect(Main Goal)
│     │
│     ├─Abstractions                                    > Up: The more important thing from a technical aspect(Sub-Goal)
│     │                                                 > ↓
│     └─AggregateRoots                                  > Down: he more important thing from a business aspect(Main Goal)
│
└─Tests
   ├─{T}.Tests.Integration        // Integration Test   > Up: The more important thing from a technical aspect(Sub-Goal)
   ├─{T}.Tests.Performance        // Performance Test   > ↓
   └─{T}.Tests.Unit               // Unit Test          > Down: he more important thing from a business aspect(Main Goal)
```

![](./03-tutorial/ddd/.images/SolutionDesignExample.png)

### Use case

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

- 서비스
  - GymManagement
  - SessionReservation
  - UserManagement
- 액터
  - User
  - Admin
  - Participants
  - Trainers
- 메시지
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

### Event

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