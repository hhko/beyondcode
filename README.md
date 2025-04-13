[![Build VitePress](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml)
[![Build C# Template](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml)
[![Build C# Gym](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml)

> Make It Work, Make It Right, Make It Fast

A beautiful journey to writing **wise code that works**
- **`The source code structure`** should be as clear as **`a book’s table of contents`**, making it easy to understand the domain and system.
- **`Test code`** should act as **`a manual`** for understanding business rules.

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

<br/>

| 방향 | 관심사의 분리        | 목표의 분리                                           |
| ---       | ---                             | ---                                                             |
| 위쪽     | 기술 관심사 (_Infinite_) | 부수 목표 (_Infinite_ -**_Abstractions_**-> _Finite_)  |
| 아래쪽    | 비즈니스 관심사 (_Finite_)    | 주요 목표 (_Finite_)                                           |

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
### Case 1. Imperative Guard 스타일
```cs
public Fin<Unit> RemoveFromSchedule(Session session)
{
  if (!_sessionIds.Contains(session.Id))
  {
      return TrainerErrors.SessionNotFound;
  }

  var removeBookingResult = _schedule.RemoveBooking(session.Date, session.Time);
  if (removeBookingResult.IsFail)
  {
      return (Error)removeBookingResult;
  }

  _sessionIds.Remove(session.Id);

  return Unit.Default;
}
```

### Case 2. Monadic 스타일
```cs
private Fin<Unit> ValidateSessionExists(Guid sessionId) =>
    _sessionIds.Contains(sessionId)
        ? Unit.Default
        : TrainerErrors.SessionNotFound;

private Unit RemoveSessionId(Guid sessionId)
{
    _sessionIds.Remove(sessionId);
    return Unit.Default;
}

public Fin<Unit> RemoveFromSchedule(Session session)
{
  return ValidateSessionExists(session.Id)
      .Bind(_ => _schedule.RemoveBooking(session.Date, session.Time))
      .Map(_ => RemoveSessionId(session.Id));
}
```

### Case 3. Monadic LINQ 스타일
```cs
private Fin<Unit> ValidateSessionExists(Guid sessionId) =>
    _sessionIds.Contains(sessionId)
        ? Unit.Default
        : TrainerErrors.SessionNotFound;

private Unit RemoveSessionId(Guid sessionId)
{
    _sessionIds.Remove(sessionId);
    return Unit.Default;
}

public Fin<Unit> RemoveFromSchedule(Session session)
{
  return from _1 in ValidateSessionExists(session.Id)
         from _2 in _schedule.RemoveBooking(session.Date, session.Time)
         select RemoveSessionId(session.Id);
}
```