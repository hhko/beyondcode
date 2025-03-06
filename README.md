[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

> A beautiful journey to writing wise code
> - **`The structure`** of the source code should be as clear as **`a table of contents in a book`** to help understand the system.
> - **`Test code`** should serve as **`a manual`** for understanding business rules.

<br/>

## Application Architecture

### Internal Architecture
![](./01-architecture/part1-overview/ch04-internal-architecture/.images/Architecture.Internal.png)

### External Architecture
> TODO

<br/>

## Domain-Driven Design Tutorial

> This has been restructured based on "[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)".

### Goal
- Understand code structuring for sustainable software development.
- Learn tactical design that express domain knowledge as code.

### Table of Contents
- Part 1. Business Concern
  - [ ] Chapter 01. Domain Exploration
  - [ ] Chapter 02. Deeper Domain Exploration
  - [ ] Chapter 03. Use Case
    - [ ] CQRS
    - [ ] Event
    - [ ] Validator
    - [ ] DTO
    - [ ] Factory
    - [ ] Pipeline
- Part 2. Host Technical Concern
  - [ ] Chapter 01. Host
    - [ ] Option
    - [ ] Job
    - [ ] Integration Test
  - [ ] Chapter 02. Container
    - [ ] Dockerfile
    - [ ] docker-compose.yml
    - [ ] Service Discovery
  - [ ] Chapter 03. OpenTelemetry
  - [ ] Chapter 04. Resilience
- Part 3. Input/Output Technical Concern
  - [ ] Chapter 01. WebApi
  - [ ] Chapter 02. PostgreSQL
  - [ ] Chapter 03. RabbitMQ
  - [ ] Chapter 04. Reverse Proxy

### Solution Design Principles

1. **Separation**
   - **Concern**: `Business Concern` vs `Technical Concern`
   - **Goal**: `Main Goal` vs `Sub-Goal`(something supplementary to the main goal, 부수 목표: 주가 되는 것에 붙어 따르는 것)
1. **Direction**
   - **Up**: The more important thing from a technical aspect(Sub-Goal)
   - **Down**: The more important thing from a business aspect(Main Goal)

<br/>

| `Direction` | `Separation` of Concerns    | `Separation` of Goals                     |
| ---         | ---                         | ---                                       |
| Up          | Technical Concern(Infinite) | Sub-Goal(Infinite -Abstractions-> Finite) |
| Down        | Business Concern(Finite)    | Main Goal(Finite)                         |

- To transform the infinite nature of sub-goals into a finite structure, an `Abstractions` top-level folder is introduced, with sub-goals placed in sub-folders beneath it.
- This ensures a clear separation between sub-goals and the main goal, making all folders, except for the `Abstractions` folder at the top, more intuitively understood as part of the main goal."

```shell
# {T}: Service

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

![](./03-tutorial/ddd-basic/.images/SolutionDesignExample.png)

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
      // DomainEvent
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
| SessionReservation  | Sessions      | CreateSessionCommand            | room.ScheduleSession          | -SessionScheduledEvent(Integration)->           | GymManagement       | Gym           |
| SessionReservation  | Sessions      | CreateSessionCommand            | room.ScheduleSession          | -SessionScheduledEvent->                        | SessionReservation  | Session       |
| SessionReservation  | Sessions      | CreateSessionCommand            | room.ScheduleSession          | -SessionScheduledEvent->                        | SessionReservation  | Trainer       |
| SessionReservation  | Participants  | CancelReservationCommand        | session.CancelReservation     | -ReservationCanceledEvent->                     | SessionReservation  | Participant   |
| SessionReservation  | Sessions      | RoomRemovedEvent                | session.Cancel                | -SessionCanceledEvent->                         | SessionReservation  | Participant   |
| SessionReservation  | Sessions      | RoomRemovedEvent                | session.Cancel                | -SessionCanceledEvent->                         | SessionReservation  | Trainer       |
| SessionReservation  | Reservations  | CreateReservationCommand        | session.ReserveSpot           | -SessionSpotReservedEvent->                     | SessionReservation  | Participant   |
| GymManagement       | Rooms         | CreateRoomCommand               | gym.AddRoom                   | -RoomAddedEvent(Integration)->                  | SessionReservation  | Room          |
| GymManagement       | Rooms         | CreateRoomCommand               | gym.AddRoom                   | -RoomRemovedEvent(Integration)->                | SessionReservation  | Room          |
| GymManagement       | Rooms         | CreateRoomCommand               | gym.AddRoom                   | -RoomRemovedEvent(Integration)->                | SessionReservation  | Session       |

x 문서 정리
x 컴파일러 정리
  ppt 업데이트
  validator 구현 개선
  테스트 프로젝트
    errors?
    ...
    코드 리뷰뷰


