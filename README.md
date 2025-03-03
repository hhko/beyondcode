[![build](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml/badge.svg)](https://github.com/hhko/better-code-with-ddd/actions/workflows/build.yml)

> A Beautiful Journey to Writing Wise Code
> - **`The structure`** of the source code should be as clear as **`a table of contents in a book`** to help understand the system.
> - **`Test code`** should serve as **`a manual`** for understanding business rules.

<br/>

## Application Architecture

### Internal Architecture
![](./01-architecture/part1-overview/ch04-internal-architecture/.images/Architecture.Internal.png)

### External Architecture
> TODO

<br/>

## Domain-Driven Design Basic Tutorial

> This has been restructured based on "[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)".

### Goal
- Understand code structuring for sustainable software development.
- Learn tactical Design that express domain knowledge as code.

### Table of Contents
- Part 1. Business Concern
  - [ ] Chapter 01. Domain Exploration
  - [ ] Chapter 02. Deeper Domain Exploration
  - [ ] Chapter 03. Use Case(DTO)
  - [ ] Chapter 04. Factory(Error)
- Part 2. Host Technical Concern
  - [ ] Chapter 05. Host(Option)
  - [ ] Chapter 06. Container(Service Discovery)
  - [ ] Chapter 07. OpenTelemetry
  - [ ] Chapter 08. Resilience
- Part 3. Input/Output Technical Concern
  - [ ] Chapter 09. WebApi
  - [ ] Chapter 10. PostgreSQL
  - [ ] Chapter 11. RabbitMQ
  - [ ] Chapter 12. Reverse Proxy

### Solution Design Principles

1. **Separation**
   - **Concern**: `Business Concern` vs `Technical Concern`
   - **Goal**: `Main Goal` vs `Sub-Goal`(something supplementary to the main goal, 부수 목표: 주가 되는 것에 붙어 따르는 것)
1. **Direction**
   - **Up**: The more important thing from a technical aspect(Sub-Goal)
   - **Down**: The more important thing from a business aspect(Main Goal)

<br/>

| `Direction`  | `Separation` of Concerns | `Separation` of Goals                         |
| --- | --- | --- |
| Up    | Technical Concern(Infinite)   | Sub-Goal(Infinite -Abstractions-> Finite)   |
| Down  | Business Concern(Finite)      | Main Goal(Finite)                           |

- To transform the infinite nature of sub-goals into a finite structure, an `Abstractions` top-level folder is introduced, with sub-goals placed in sub-folders beneath it.
- This ensures a clear separation between sub-goals and the main goal, making all folders, except for the `Abstractions` folder at the top, more intuitively understood as part of the main goal."

```
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
   ├─{T}..Tests.Integration       // Integration Test   > Up: The more important thing from a technical aspect(Sub-Goal)
   ├─{T}..Tests.Performance       // Performance Test   > ↓
   └─{T}..Tests.Unit              // Unit Test          > Down: he more important thing from a business aspect(Main Goal)
```
- {T}: Service

![](./03-tutorial/ddd-basic/.images/SolutionDesignExample.png)

### Use case

| No |  Use case           | AggregateRoot     | Category          | Name                           |
|----| --------------------|-------------------|-------------------|--------------------------------|
| 1  |  Admins             | Admin             | IntegrationEvents | AdminProfileCreatedEvent       |
| 2  |  **Authentication** | **User**          | Commands          | Register                       |
| 3  |  **Authentication** | **User**          | Queries           | Login                          |
| 4  |  Gyms               | Gym               | Commands          | AddTrainer                     |
| 5  |  Gyms               | Gym               | Commands          | CreateGym                      |
| 6  |  Gyms               | Gym               | Events            | GymAddedEvent                  |
| 7  |  Gyms               | Gym               | IntegrationEvents | SessionScheduledEvent          |
| 8  |  Gyms               | Gym               | Queries           | GetGym                         |
| 9  |  Gyms               | Gym               | Queries           | ListGyms                       |
| 10 |  Gyms               | Gym               | Queries           | ListSessions                   |
| 11 |  Participants       | Participant       | Commands          | CancelReservation              |
| 12 |  Participants       | Participant       | Events            | ReservationCanceledEvent       |
| 13 |  Participants       | Participant       | Events            | SessionCanceledEvent           |
| 14 |  Participants       | Participant       | Events            | SessionSpotReservedEvent       |
| 15 |  Participants       | Participant       | IntegrationEvents | ParticipantProfileCreatedEvent |
| 16 |  Participants       | Participant       | Queries           | ListParticipantSessions        |
| 17 |  **Profiles**       | **User**          | Commands          | CreateAdminProfile             |
| 18 |  **Profiles**       | **User**          | Commands          | CreateParticipantProfile       |
| 19 |  **Profiles**       | **User**          | Commands          | CreateTrainerProfile           |
| 20 |  **Profiles**       | **User**          | Queries           | ListProfiles                   |
| 21 |  **Reservations**   | **Session**       | Commands          | CreateReservation              |
| 22 |  Rooms              | Room              | Commands          | CreateRoom                     |
| 23 |  Rooms              | Room              | Commands          | DeleteRoom                     |
| 24 |  Rooms              | Room              | IntegrationEvents | RoomAddedEvent                 |
| 25 |  Rooms              | Room              | IntegrationEvents | RoomRemovedEvent               |
| 26 |  Rooms              | Room              | Queries           | GetRoom                        |
| 27 |  Rooms              | Room              | Queries           | ListRooms                      |
| 28 |  Sessions           | Session           | Commands          | CreateSession                  |
| 29 |  Sessions           | Session           | Events            | SessionScheduledEvent          |
| 30 |  Sessions           | Session           | IntegrationEvents | RoomRemovedEvent               |
| 31 |  Sessions           | Session           | Queries           | GetSession                     |
| 32 |  Subscriptions      | Subscription      | Commands          | CreateSubscription             |
| 33 |  Subscriptions      | Subscription      | Events            | SubscriptionSetEvent           |
| 34 |  Subscriptions      | Subscription      | Queries           | ListSubscriptions              |
| 35 |  Trainers           | Trainer           | Events            | SessionCanceledEvent           |
| 36 |  Trainers           | Trainer           | Events            | SessionScheduledEvent          |
| 37 |  Trainers           | Trainer           | IntegrationEvents | TrainerCreatedEvent            |

- Category
  - Commands
  - Queries
  - Events
  - IntegrationEvents