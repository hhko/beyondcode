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
| 31 |   |  Sessions           | Session           | Queries           | GetSession                     |
| 32 | O |  Subscriptions      | Subscription      | Commands          | CreateSubscription             |
| 33 | O |  Subscriptions      | Subscription      | Events            | SubscriptionSetEvent           |
| 34 | O |  Subscriptions      | Subscription      | Queries           | ListSubscriptions              |
| 35 |   |  Trainers           | Trainer           | Events            | SessionCancledEvent            |
| 36 |   |  Trainers           | Trainer           | Events            | SessionScheduledEvent          |
| 37 |   |  Trainers           | Trainer           | IntegrationEvents | TrainerCreatedEvent            |