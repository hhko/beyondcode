# 도메인 주도 설계 기본

> '[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)'를 기반으로 재구성되었습니다.

## 목표
- 지속 가능한 소프트웨어 개발을 위한 코드 구성 방법을 이해한다.
- 도메인 지식을 코드로 표현하는 설계 패턴을 학습니다.

## 목차
- Part 1. 비즈니스 관심사
  - [ ] Chapter 01. Domain Exploration
  - [ ] Chapter 02. Deeper Domain Exploration
  - [ ] Chapter 03. Use Case
  - [ ] Chapter 04. Domain
- Part 2. Host 기술 관심사
  - [ ] Chapter 05. Host(Option)
  - [ ] Chapter 06. Container(Service Discovery)
  - [ ] Chapter 07. OpenTelemetry
  - [ ] Chapter 08. Resilience
- Part 3. Input/Output 기술 관심사
  - [ ] Chapter 09. WebApi
  - [ ] Chapter 10. PosgreSQL
  - [ ] Chapter 11. RabbitMQ
  - [ ] Chapter 12. Reverse Proxy

## 솔루션 구성 원칙

1. **분리(Separation)**
   - **괸심사(Concerns)**: `비즈니스 관심사` vs `기술 관심사`
   - **목표(Goals)**: `주 목표` vs `부수 목표`(주가 되는 것에 붙어 따르는 것)
1. **방향(Direction)**
   - **위(Up)**: 기술적으로 더 중요한 요소(부수 목표)
   - **아래(Down)**: 비즈니스적으로 더 중요한 요소(주 목표)

| 방향  | 관심사의 분리 | 목표의 분리                         |
| --- | --- | --- |
| 위(Up)      | 기술 관심사(무한)   | 부수 목표(무한 -Abstractions-> 유한)   |
| 아래(Down)  | 비즈니스 관심사(유한)    | 주 목표(유한)     |

- 부수 목표의 무한성을 유한으로 전환하기 위해 `Abstractions` 상위 폴더를 도입하고, 그 아래 하위 폴더에 무한한 부수 목표를 배치합니다.
- 이를 통해 부수 목표가 주 목표와 명확히 분리되며, `Abstractions` 폴더를 상단에 배치함으로써 나머지 모든 폴더가 주 목표를 명확히 드러내게 됩니다.

```
{T}
├─Src
│  ├─{T}                          // Host               > 위(Up): 기술적으로 더 중요한 요소(부수 목표)
│  ├─{T}.Adapters.Infrastructure  // Adapter Layer      > │
│  ├─{T}.Adapters.Persistence     // Adapter Layer      > │
│  ├─{T}.Application              // Application Layer  > ↓
│  └─{T}.Domain                   // Domain Layer       > 아래(Down): 비즈니스적으로 더 중요한 요소(주 목표)
│     ├─Abstractions                                    > 위(Up): 기술적으로 더 중요한 요소(부수 목표)
│     │                                                 > ↓
│     └─AggregateRoots                                  > 아래(Down): 비즈니스적으로 더 중요한 요소(주 목표)
│
└─Tests
   ├─{T}..Tests.Integration       // Integration Test   > 위(Up): 기술적으로 더 중요한 요소(부수 목표)
   ├─{T}..Tests.Performance       // Performance Test   > ↓
   └─{T}..Tests.Unit              // Unit Test          > 아래(Down): 비즈니스적으로 더 중요한 요소(주 목표)
```

![](./.images/SolutionDesignExample.png)

## 유스케이스

| No | O |  Usecase            | AggregateRoot     | Category          | Name                           |
|----|---| --------------------|-------------------|-------------------|--------------------------------|
| 1  | O |  Admins             | Admin             | IntegrationEvents | AdminProfileCreatedEvent       |
| 2  | O |  **Authentication** | **User**          | Commands          | Register                       |
| 3  | O |  **Authentication** | **User**          | Queries           | Login                          |
| 4  | O |  Gyms               | Gym               | Commands          | AddTrainer                     |
| 5  | O |  Gyms               | Gym               | Commands          | CreateGym                      |
| 6  | O |  Gyms               | Gym               | Events            | GymAddedEvent                  |
| 7  | O |  Gyms               | Gym               | IntegrationEvents | SessionScheduledEvent          |
| 8  | O |  Gyms               | Gym               | Queries           | GetGym                         |
| 9  | O |  Gyms               | Gym               | Queries           | ListGyms                       |
| 10 | O |  Gyms               | Gym               | Queries           | ListSessions                   |
| 11 | O |  Participants       | Participant       | Commands          | CancelReservation              |
| 12 | O |  Participants       | Participant       | Events            | ReservationCanceledEvent       |
| 13 | O |  Participants       | Participant       | Events            | SessionCanceledEvent           |
| 14 | O |  Participants       | Participant       | Events            | SessionSpotReservedEvent       |
| 15 |   |  Participants       | Participant       | IntegrationEvents | ParticipantProfileCreatedEvent |
| 16 | O |  Participants       | Participant       | Queries           | ListParticipantSessions        |
| 17 | O |  **Profiles**       | **User**          | Commands          | CreateAdminProfile             |
| 18 | O |  **Profiles**       | **User**          | Commands          | CreateParticipantProfile       |
| 19 | O |  **Profiles**       | **User**          | Commands          | CreateTrainerProfile           |
| 20 | O |  **Profiles**       | **User**          | Queries           | ListProfiles                   |
| 21 | O |  **Reservations**   | **Session**       | Commands          | CreateReservation              |
| 22 | O |  Rooms              | Room              | Commands          | CreateRoom                     |
| 23 | O |  Rooms              | Room              | Commands          | DeleteRoom                     |
| 24 |   |  Rooms              | Room              | IntegrationEvents | RoomAddedEvent                 |
| 25 |   |  Rooms              | Room              | IntegrationEvents | RoomRemovedEvent               |
| 26 | O |  Rooms              | Room              | Queries           | GetRoom                        |
| 27 | O |  Rooms              | Room              | Queries           | ListRooms                      |
| 28 |   |  Sessions           | Session           | Commands          | CreateSession                  |
| 29 |   |  Sessions           | Session           | Events            | SessionScheduledEvent          |
| 30 |   |  Sessions           | Session           | IntegrationEvents | RoomRemovedEvent               |
| 31 | O |  Sessions           | Session           | Queries           | GetSession                     |
| 32 | O |  Subscriptions      | Subscription      | Commands          | CreateSubscription             |
| 33 | O |  Subscriptions      | Subscription      | Events            | SubscriptionSetEvent           |
| 34 | O |  Subscriptions      | Subscription      | Queries           | ListSubscriptions              |
| 35 | O |  Trainers           | Trainer           | Events            | SessionCancledEvent            |
| 36 | O |  Trainers           | Trainer           | Events            | SessionScheduledEvent          |
| 37 |   |  Trainers           | Trainer           | IntegrationEvents | TrainerCreatedEvent            |