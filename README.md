[![Build VitePress](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-vitepress.yml)
[![Build C# Template](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-template.yml)
[![Build C# Gym](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml/badge.svg)](https://github.com/hhko/beyondcode/actions/workflows/build-gym.yml)

> Make It Work, Make It Right, Make It Fast

A beautiful journey to writing **wise code that works**
- **`The source code structure`** should be as clear as **`a book’s table of contents`**, making it easy to understand the domain and system.
- **`Test code`** should act as **`a manual`** for understanding business rules.

<br/>

## DDD와 Functional Programming

'무엇을 표현할지'(DDD) 와 '어떻게 표현할지'(Functional Programming) 가 만나서, 변경에 강하고, 테스트 가능하고, 명확한 의도를 가진 코드를 만들어줍니다

- DDD란?
  - 복잡한 비즈니스 로직을 도메인 모델 중심으로 풀어나가는 설계 방법입니다.
  - 도메인 전문가의 언어(Ubiquitous Language) 로 시스템을 설계하는 것이 핵심입니다.
- Function Programming란?
  - 함수(수학적인 함수)에 기반한 프로그래밍 방식입니다.
  - 상태 변경 없이, 입력에 따라 일관된 출력을 보장합니다.

 **DDD 개념**                    | **FP 접근 방식**                                 | **공통 목표 / 장점**
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
### Case 1. Imperative Guard 스타일
```cs
public sealed class Trainer : AggregateRoot
{
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
}
```

### Case 2. Monadic 스타일
```cs
public sealed class Trainer : AggregateRoot
{
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
}
```

### Case 3. Monadic LINQ 스타일
```cs
public sealed class Trainer : AggregateRoot
{
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
}
```
