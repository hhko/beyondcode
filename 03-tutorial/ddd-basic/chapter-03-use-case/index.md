- use case
- dto
- Validator
- pipeline
  - observability: 로그 시작/종료
  - observability: 로그 예외
  - observability: 로그 실패
  - observability: 지표
  - observability: 추적
  - 유효성 검사
  - 캐시

## 서비스 단위 유스케이스스

| No | Service            | Usecase        | AggregateRoot | Category          | Name                           |
|----|--------------------|----------------|---------------|-------------------|--------------------------------|
| 1  | GymManagement      | Admins         | Admin         | IntegrationEvents | AdminProfileCreatedEvent       |
| 2  | GymManagement      | Gyms           | Gym           | Commands          | AddTrainer                     |
| 3  | GymManagement      | Gyms           | Gym           | Commands          | CreateGym                      |
| 4  | GymManagement      | Gyms           | Gym           | Events            | GymAddedEvent                  |
| 5  | GymManagement      | Gyms           | Gym           | IntegrationEvents | SessionScheduledEvent          |
| 6  | GymManagement      | Gyms           | Gym           | Queries           | GetGym                         |
| 7  | GymManagement      | Gyms           | Gym           | Queries           | ListGyms                       |
| 8  | GymManagement      | Rooms          | Room          | Commands          | CreateRoom                     |
| 9  | GymManagement      | Rooms          | Room          | Commands          | DeleteRoom                     |
| 10 | GymManagement      | Subscriptions  | Subscription  | Commands          | CreateSubscription             |
| 11 | GymManagement      | Subscriptions  | Subscription  | Events            | SubscriptionSetEvent           |
| 12 | GymManagement      | Subscriptions  | Subscription  | Queries           | ListSubscriptions              |
| 13 | SessionReservation | Gyms           | Gym           | Queries           | ListSessions                   |
| 14 | SessionReservation | Participants   | Participant   | Commands          | CancelReservation              |
| 15 | SessionReservation | Participants   | Participant   | Events            | ReservationCanceledEvent       |
| 16 | SessionReservation | Participants   | Participant   | Events            | SessionCanceledEvent           |
| 17 | SessionReservation | Participants   | Participant   | Events            | SessionSpotReservedEvent       |
| 18 | SessionReservation | Participants   | Participant   | IntegrationEvents | ParticipantProfileCreatedEvent |
| 19 | SessionReservation | Participants   | Participant   | Queries           | ListParticipantSessions        |
| 20 | SessionReservation | Reservations   | Session       | Commands          | CreateReservation              |
| 21 | SessionReservation | Rooms          | Room          | IntegrationEvents | RoomAddedEvent                 |
| 22 | SessionReservation | Rooms          | Room          | IntegrationEvents | RoomRemovedEvent               |
| 23 | SessionReservation | Rooms          | Room          | Queries           | GetRoom                        |
| 24 | SessionReservation | Rooms          | Room          | Queries           | ListRooms                      |
| 25 | SessionReservation | Sessions       | Session       | Commands          | CreateSession                  |
| 26 | SessionReservation | Sessions       | Session       | Events            | SessionScheduledEvent          |
| 27 | SessionReservation | Sessions       | Session       | IntegrationEvents | RoomRemovedEvent               |
| 28 | SessionReservation | Sessions       | Session       | Queries           | GetSession                     |
| 29 | SessionReservation | Trainers       | Trainer       | Events            | SessionCancledEvent            |
| 30 | SessionReservation | Trainers       | Trainer       | Events            | SessionScheduledEvent          |
| 31 | SessionReservation | Trainers       | Trainer       | IntegrationEvents | TrainerCreatedEvent            |
| 32 | UserManagement     | Authentication | User          | Commands          | Register                       |
| 33 | UserManagement     | Authentication | User          | Queries           | Login                          |
| 34 | UserManagement     | Profiles       | User          | Commands          | CreateAdminProfile             |
| 35 | UserManagement     | Profiles       | User          | Commands          | CreateParticipantProfile       |
| 36 | UserManagement     | Profiles       | User          | Commands          | CreateTrainerProfile           |
| 37 | UserManagement     | Profiles       | User          | Queries           | ListProfiles                   |

## 유스케이스
| No | Usecase        | AggregateRoot | Category          | Name                           |
|----|----------------|---------------|-------------------|--------------------------------|
| 1  | Admins         | Admin         | IntegrationEvents | AdminProfileCreatedEvent       |
| 2  | Gyms           | Gym           | Commands          | AddTrainer                     |
| 3  | Gyms           | Gym           | Commands          | CreateGym                      |
| 4  | Gyms           | Gym           | Events            | GymAddedEvent                  |
| 5  | Gyms           | Gym           | IntegrationEvents | SessionScheduledEvent          |
| 6  | Gyms           | Gym           | Queries           | GetGym                         |
| 7  | Gyms           | Gym           | Queries           | ListGyms                       |
| 8  | Gyms           | Gym           | Queries           | ListSessions                   |
| 9  | Rooms          | Room          | Commands          | CreateRoom                     |
| 10 | Rooms          | Room          | Commands          | DeleteRoom                     |
| 11 | Rooms          | Room          | IntegrationEvents | RoomAddedEvent                 |
| 12 | Rooms          | Room          | IntegrationEvents | RoomRemovedEvent               |
| 13 | Rooms          | Room          | Queries           | GetRoom                        |
| 14 | Rooms          | Room          | Queries           | ListRooms                      |
| 15 | Subscriptions  | Subscription  | Commands          | CreateSubscription             |
| 16 | Subscriptions  | Subscription  | Events            | SubscriptionSetEvent           |
| 17 | Subscriptions  | Subscription  | Queries           | ListSubscriptions              |
| 18 | Participants   | Participant   | Commands          | CancelReservation              |
| 19 | Participants   | Participant   | Events            | ReservationCanceledEvent       |
| 20 | Participants   | Participant   | Events            | SessionCanceledEvent           |
| 21 | Participants   | Participant   | Events            | SessionSpotReservedEvent       |
| 22 | Participants   | Participant   | IntegrationEvents | ParticipantProfileCreatedEvent |
| 23 | Participants   | Participant   | Queries           | ListParticipantSessions        |
| 24 | Reservations   | Session       | Commands          | CreateReservation              |
| 25 | Sessions       | Session       | Commands          | CreateSession                  |
| 26 | Sessions       | Session       | Events            | SessionScheduledEvent          |
| 27 | Sessions       | Session       | IntegrationEvents | RoomRemovedEvent               |
| 28 | Sessions       | Session       | Queries           | GetSession                     |
| 29 | Trainers       | Trainer       | Events            | SessionCancledEvent            |
| 30 | Trainers       | Trainer       | Events            | SessionScheduledEvent          |
| 31 | Trainers       | Trainer       | IntegrationEvents | TrainerCreatedEvent            |
| 32 | Authentication | User          | Commands          | Register                       |
| 33 | Authentication | User          | Queries           | Login                          |
| 34 | Profiles       | User          | Commands          | CreateAdminProfile             |
| 35 | Profiles       | User          | Commands          | CreateParticipantProfile       |
| 36 | Profiles       | User          | Commands          | CreateTrainerProfile           |
| 37 | Profiles       | User          | Queries           | ListProfiles                   |