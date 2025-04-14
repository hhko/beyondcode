[![Build VitePress](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml)
[![Build C# Template](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml)
[![Build C# Gym](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml)

> Make It Work, Make It Right, Make It Fast

A beautiful journey to writing **wise code that works**
- **`The source code structure`** should be as clear as **`a book’s table of contents`**, making it easy to understand the domain and system.
- **`Test code`** should act as **`a manual`** for understanding business rules.

<br/>

## 도메인 주도 설계와 함수형 프로그래밍

도메인 주도 설계의 '무엇을 표현할지'와 함수형 프로그래밍의 '어떻게 표현할지'가 만나서, 변경에 강하고, 테스트 가능하고, 명확한 의도를 가진 코드를 만듭니다.

- DDD란?
  - 복잡한 비즈니스 로직을 도메인 모델 중심으로 풀어나가는 설계 방법입니다.
  - 도메인 전문가의 언어(Ubiquitous Language) 로 시스템을 설계하는 것이 핵심입니다.
- Function Programming란?
  - 함수(수학적인 함수)에 기반한 프로그래밍 방식입니다.
  - 상태 변경 없이, 입력에 따라 일관된 출력을 보장합니다.

 **DDD 개념**                    | **FP 접근 방식**                                 | **공통 목표(장점)**
-------------------------------|---------------------------------------------------|----------------------------------
복잡성 분리                    | 부작용 없는 순수 함수 구성                          | 변경에 강한 모델
명확한 경계 (Bounded Context) | 상태 변화는 명시적 함수 결과로 표현                   | 명확한 책임 분리, 테스트 용이성
유비쿼터스 언어               | 도메인 타입을 코드로 직접 모델링                       | 도메인 언어와 코드가 일치
의미 있는 행동 메서드         | 도메인 행동을 순수 함수로 추상화                       | 의도가 명확한 코드, 재사용 가능성 증가
도메인 규칙 불변성 유지       | 불변 객체 사용 (데이터는 항상 새로운 구조로 복사)        | 상태 일관성 보장, 디버깅 용이
실패를 도메인의 일부로 처리   | `Fin`, `Option`, `Either` 등으로 실패를 값으로 표현    | 명시적인 에러 흐름, 예외 최소화
도메인 이벤트 사용            | 이벤트를 값으로 함께 반환                              | 부작용 분리, 이벤트 소싱에 유리
명확한 상태 전이              | 상태마다 구체적인 타입으로 표현 (`Union Type`)         | 불가능한 상태 방지, 검증된 상태 흐름
일관된 로직 흐름              | `Map`, `Bind`, `LINQ`, `Pipeline` 등으로 체이닝       | 읽기 쉬운 흐름, 변경 시 영향 최소화

<br/>

## Application Architecture

### Architecture Technical Map
![](./.images/ArchitectureTechMap.png)

### Internal Architecture (Hexagonal Architecture)
![hexagonal architecture](./01-architecture/part1-overview/ch03-internal-architecture/.images/Architecture.Internal.Hexagonal.png)

### External Architecture
> TODO

<br/>

## Hands-on Labs
I restructured '[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)' based on the design principles and practices I defined.

### 목표
- Understand code structuring for sustainable software development.
- Learn tactical design that express domain knowledge as code.

### 목차
- Part 1. Domain
  - [ ] Chapter 01. [Domain Glossary](./02-tutorial/ddd/ch01-domain-glossary/index.md)
  - [x] Chapter 02. [Domain Exploration](./02-tutorial/ddd/ch02-domain-exploration/index.md)
  - [ ] Chapter 03. Domain Structuring
  - [ ] Chapter 04. Domain Test
- Part 2. Use Case
  - [ ] Chapter 05. Use Case Exploration
  - [ ] Chapter 06. Use Case Pipeline
  - [ ] Chapter 07. Use Case Test(Cucumber)
- Part 2. Monolithic
  - [ ] Chapter 08. WebApi
  - [ ] Chapter 09. OpenTelemetry
  - [ ] Chapter 10. PostgreSQL
  - [ ] Chapter 11. Cache
  - [ ] Chapter 12. Containerization
- Part 3. Microservices
  - [ ] Chapter 13. Aspire
  - [ ] Chapter 14. RabbitMQ
  - [ ] Chapter 15. Resilience
  - [ ] Chapter 16. Reverse Proxy
  - [ ] Chapter 17. Chaos Engineering
- Part 4. Operations
  - [ ] Chapter 18. OpenFeature(Feature Flag Management)
  - [ ] Chapter 19. OpenSearch(Observability System)
  - [ ] Chapter 20. Ansible(Infrastructure as Code)
  - [ ] Chapter 21. Backstage(Building developer portals)

### 소스 폴더 구성 원칙

- 분리(Separation)
  - **관심사의 분리: 비즈니스 관심사 vs. 기술 관심사**
    - 관심사의 분리는 레이어로 구분됩니다.
      - Adapter 레이어: 기술 관심사
      - Application 레이어: 비즈니스 관심사(도메인 흐름)
      - Domain 레이어: 비즈니스 관심사(도메인 단위)
  - **목표의 분리: 주요 목표 vs. 부수 목표 (주된 목표에 따르는 부수적인 목표)**
    - 목표의 분리는 배치 방향으로 구분됩니다.
      - 위쪽: 기술적인 측면에서 더 중요한 것(부수 목표: Abstractions)을 배치합니다.
      - 아래쪽: 비즈니스 측면에서 더 중요한 것(주요 목표)을 배치합니다.

  | 방향     | 관심사의 분리           | 목표의 분리                                 |
  | ---     | ---                             | ---                               |
  | 위쪽    | 기술 관심사 (무한)       | 부수 목표 (무한 -**_Abstractions_**-> 유한)  |
  | 아래쪽  | 비즈니스 관심사 (유한)    | 주요 목표 (유한)                            |
  - 레이어의 주요 목표를 직관적으로 이해하기 위해, 여러 부수 목표를 Abstractions 폴더 아래에 배치하여 부수 목표를 하나로 묶습니다.
  - 이렇게 하면 부수 목표는 Abstractions 폴더 안에 고정되어 상단에 위치하게 되어,
  - 주요 목표와 쉽게 구분할 수 있어 주요 목표를 더 잘 이해할 수 있습니다.

```shell
{T}
├─Src
│  ├─{T}                          // Host               > 위쪽: 기술 관심사 (부수 목표)
│  ├─{T}.Adapters.Infrastructure  // Adapter Layer      >  │
│  ├─{T}.Adapters.Persistence     // Adapter Layer      >  │
│  ├─{T}.Application              // Application Layer  >  ↓
│  └─{T}.Domain                   // Domain Layer       > 아래쪽: 비즈니스 관심사 (주요 목표)
│     │
│     ├─Abstractions                                    > 위쪽: 기술 관심사 (부수 목표)
│     │                                                 >  ↓
│     └─AggregateRoots                                  > 아래쪽: 비즈니스 관심사 (주요 목표)
│
└─Tests
   ├─{T}.Tests.Integration        // Integration Test   > 위쪽: 기술 관심사 (부수 목표)
   ├─{T}.Tests.Performance        // Performance Test   >  ↓
   └─{T}.Tests.Unit               // Unit Test          > 아래쪽: 비즈니스 관심사 (주요 목표)
```

<br/>

## Functional DDD 리팩토링

### 도메인 의도를 표현하는 메서드 이름 만들기

개선 전(기술적 의도) | 개선 후(도메인 의도) | 설명
--- | --- | ---
CreateTrainerProfile | `PromoteToTrainer` | 기존 사용자를 트레이너로 승격
EnsureTrainerNotExist | `EnsureTrainerNotPromoted` | 사용자가 이미 트레이너로 승격되지 않았는지 확인

### 하나의 논리적 작업 단위로 메서드 만들기
- 도메인 행동과 도메인 이벤트는 하나의 논리적 작업 단위로 만들기
  ```cs
  private Fin<Guid> ApplyTrainerPromotion(Guid newTrainerId)
  {
    TrainerId = newTrainerId;
    _domainEvents.Add(new TrainerPromotedEvent(Id, newTrainerId));

    return newTrainerId;
  }
  ```
  - 예를 들어, *"트레이너 프로필을 생성한다"*는 도메인 행동은 단순히 TrainerId를 할당하는 것에 그치지 않고,
  - 그 결과로 도메인 이벤트(TrainerProfileCreatedEvent)가 함께 발생하는 것은 불가분의 관계입니다.
  - 따라서 이 둘은 하나의 메서드(ApplyTrainerProfile) 내에서 함께 처리하는 것이 자연스럽습니다.
- Map과 Bind의 차이 이해하기
  ```cs
  .Map(_ => NewTrainerId())
  .Bind(newTrainerId => ApplyTrainerPromotion(newTrainerId));
  ```
  - Map은 순수한 값 변환 함수 (T → R)에 사용하는 함수입니다.
  - 예: NewTrainerId()는 실패하지 않는 순수 함수이므로 Map의 입력값으로 적합합니다.
  - 반면 Bind는 부수 효과를 포함한 함수 (T → Fin<R>)에 사용해야 합니다.
  - 예: ApplyTrainerPromotion()은 내부 상태를 변경하고 도메인 이벤트를 추가하는 부수 효과 함수이므로 Bind를 통해 연결하는 것이 적절합니다.
- 순수 함수를 Pure 모나드로 만들기
  ```cs
  // 일반 메서드
  [Pure]
  Guid NewTrainerId()

  Pure<Guid> monad = Pure(NewTrainerId())
  ```
  - 다른 모나드(예. Fin, Option, ...)들과 함수 체이닝하기 위해 순수한 값을 리프팅(pure lifted values)합니다.

```cs
// Case 1: Imperative Guard 스타일
public Fin<Guid> PromoteToTrainer()
{
  if (TrainerId is not null)
      return UserErrors.TrainerAlreadyPromoted(TrainerId.Value);

  Guid newTrainerId = Guid.NewGuid();

  TrainerId = newTrainerId;
  _domainEvents.Add(new TrainerPromotedEvent(Id, newTrainerId));

  return TrainerId.Value;
}

// Case 2. Monadic 스타일
public Fin<Guid> PromoteToTrainer()
{
  return EnsureTrainerNotPromoted(TrainerId)
    .Map(_ => NewTrainerId())
    .Bind(newTrainerId => ApplyTrainerPromotion(newTrainerId));
}

// Case 3. Monadic LINQ 스타일
public Fin<Guid> PromoteToTrainer()
{
  return from _1 in EnsureTrainerNotPromoted(TrainerId)
         from newTrainerId in Pure(NewTrainerId())
         from _2 in ApplyTrainerPromotion(newTrainerId)
         select newTrainerId;
}

[Pure]
private Fin<Unit> EnsureTrainerNotPromoted(Guid? trainerId) =>
  trainerId.HasValue
    ? UserErrors.TrainerAlreadyPromoted(trainerId.Value)
    : unit;

[Pure]
private Guid NewTrainerId() =>
  Guid.NewGuid();

private Fin<Guid> ApplyTrainerPromotion(Guid newTrainerId)
{
  // 하나의 논리적 작업: TrainerId 설정과 이벤트 발생은 불가분의 도메인 행동이다
  //
  // "프로필을 생성한다"는 하나의 도메인 행동이자,
  // 그 결과로 TrainerId가 할당되고 이벤트가 생성되는 것은 불가분 관계입니다.

  TrainerId = newTrainerId;
  _domainEvents.Add(new TrainerPromotedEvent(Id, newTrainerId));

  return newTrainerId;
}
```

### void 메서드 제거하기
- Unit을 반환하는 함수로 개선하기
  ```cs
  // 개선 전
  // void UnregisterSession(Guid sessionId)

  // 개선 후
  Unit UnregisterSession(Guid sessionId)
  ```
  - void를 반환하는 함수는 Unit을 반환하도록 변경하여, 함수 체이닝이 가능하도록 합니다.
- Bind 메서드 이해하기
  ```cs
  Fin<Unit> UnregisterSession(Guid sessionId)
  ```
  - 부수 효과가 있는 함수는 실패 가능성과 무관하더라도,
  - 함수형 체이닝(Bind) 안에서 일관되게 Fin<Unit>을 반환하는 것이 좋습니다.

```cs
// Case 1. Imperative Guard 스타일
public Fin<Unit> UnscheduleSession(Session session)
{
  if (!_sessionIds.Contains(session.Id))
  {
      return TrainerErrors.SessionNotScheduled;
  }

  var unbookTimeSlotResult = _schedule.UnbookTimeSlot(session.Date, session.Time);
  if (unbookTimeSlotResult.IsFail)
  {
      return (Error)unbookTimeSlotResult;
  }

  _sessionIds.Remove(session.Id);

  return unit;
}

// Case 2. Monadic 스타일
public Fin<Unit> UnscheduleSession(Session session)
{
  return EnsureSessionScheduled(session.Id)
      .Bind(_ => _schedule.UnbookTimeSlot(session.Date, session.Time))
      .Map(_ => UnregisterSession(session.Id));
}

// Case 3. Monadic LINQ 스타일
public Fin<Unit> UnscheduleSession(Session session)
{
  return from _1 in EnsureSessionScheduled(session.Id)
         from _2 in _schedule.UnbookTimeSlot(session.Date, session.Time)
         from _3 in UnregisterSession(session.Id)
         select unit;
}

private Fin<Unit> EnsureSessionScheduled(Guid sessionId) =>
    _sessionIds.Contains(sessionId)
        ? unit
        : TrainerErrors.SessionNotScheduled(sessionId);

private Fin<Unit> UnregisterSession(Guid sessionId)
{
    _sessionIds.Remove(sessionId);    // 부수 효과
    return unit;
}
```

### 에러 재포장하기

- AggregateRoot는 Entity에서 발생한 에러를, 상위 도메인 맥락에 맞게 의미 있는 도메인 언어로 포장해주는 것이 더 적절할 수 있습니다.
  ```cs
  .MapFail(error =>
    error.Combine(
      TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions(
  ```

```cs
public Fin<Unit> ScheduleSession(Session session)
{
  // 에러 재포장 전
  //return from _1 in EnsureSessionNotScheduled(session.Id)
  //       from _2 in _schedule.BookTimeSlot(session.Date, session.Time)
  //       from _3 in RegisterSession(session.Id)
  //       select unit;

  // 에러 재포한 후
  return from _1 in EnsureSessionNotScheduled(session.Id)
         from _2 in _schedule.BookTimeSlot(session.Date, session.Time)
          .MapFail(error =>
            error.Combine(
              TrainerErrors.CannotHaveTwoOrMoreOverlappingSessions(
                session.Date,
                session.Time)))
         from _3 in RegisterSession(session.Id)
         select unit;
```
