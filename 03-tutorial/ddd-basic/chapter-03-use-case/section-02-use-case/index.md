| No | O |  Usecase            | AggregateRoot     | Category          | Name                           |
|----|---| --------------------|-------------------|-------------------|--------------------------------|
| 1  | O |  Admins             | Admin             | IntegrationEvents | AdminProfileCreatedEvent       |
| 2  | O |  **Authentication** | **User**          | Commands          | Register                       |
| 3  | O |  **Authentication** | **User**          | Queries           | Login                          |
| 4  |   |  Gyms               | Gym               | Commands          | AddTrainer                     |
| 5  |   |  Gyms               | Gym               | Commands          | CreateGym                      |
| 6  |   |  Gyms               | Gym               | Events            | GymAddedEvent                  |
| 7  |   |  Gyms               | Gym               | IntegrationEvents | SessionScheduledEvent          |
| 8  |   |  Gyms               | Gym               | Queries           | GetGym                         |
| 9  |   |  Gyms               | Gym               | Queries           | ListGyms                       |
| 10 |   |  Gyms               | Gym               | Queries           | ListSessions                   |
| 11 |   |  Participants       | Participant       | Commands          | CancelReservation              |
| 12 |   |  Participants       | Participant       | Events            | ReservationCanceledEvent       |
| 13 |   |  Participants       | Participant       | Events            | SessionCanceledEvent           |
| 14 |   |  Participants       | Participant       | Events            | SessionSpotReservedEvent       |
| 15 |   |  Participants       | Participant       | IntegrationEvents | ParticipantProfileCreatedEvent |
| 16 |   |  Participants       | Participant       | Queries           | ListParticipantSessions        |
| 17 |   |  **Profiles**       | **User**          | Commands          | CreateAdminProfile             |
| 18 |   |  **Profiles**       | **User**          | Commands          | CreateParticipantProfile       |
| 19 |   |  **Profiles**       | **User**          | Commands          | CreateTrainerProfile           |
| 20 |   |  **Profiles**       | **User**          | Queries           | ListProfiles                   |
| 21 |   |  **Reservations**   | **Session**       | Commands          | CreateReservation              |
| 22 |   |  Rooms              | Room              | Commands          | CreateRoom                     |
| 23 |   |  Rooms              | Room              | Commands          | DeleteRoom                     |
| 24 |   |  Rooms              | Room              | IntegrationEvents | RoomAddedEvent                 |
| 25 |   |  Rooms              | Room              | IntegrationEvents | RoomRemovedEvent               |
| 26 |   |  Rooms              | Room              | Queries           | GetRoom                        |
| 27 |   |  Rooms              | Room              | Queries           | ListRooms                      |
| 28 |   |  Sessions           | Session           | Commands          | CreateSession                  |
| 29 |   |  Sessions           | Session           | Events            | SessionScheduledEvent          |
| 30 |   |  Sessions           | Session           | IntegrationEvents | RoomRemovedEvent               |
| 31 |   |  Sessions           | Session           | Queries           | GetSession                     |
| 32 |   |  Subscriptions      | Subscription      | Commands          | CreateSubscription             |
| 33 |   |  Subscriptions      | Subscription      | Events            | SubscriptionSetEvent           |
| 34 |   |  Subscriptions      | Subscription      | Queries           | ListSubscriptions              |
| 35 |   |  Trainers           | Trainer           | Events            | SessionCancledEvent            |
| 36 |   |  Trainers           | Trainer           | Events            | SessionScheduledEvent          |
| 37 |   |  Trainers           | Trainer           | IntegrationEvents | TrainerCreatedEvent            |