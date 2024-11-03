# _better_ **CODE** _with domain-driven design_

## 기술 맵
![](./.images/TechMap.png)

<br/>

## 목차
- Part 1. 아키텍처
  - [x] [Ch 01. 아키텍처 개요](#ch-1-아키텍처-개요)
  - [x] [Ch 02. 아키텍처 원칙](#ch-2-아키텍처-원칙)
  - [x] [Ch 03. 레이어 격리](#ch-3-레이어-격리)
  - [x] [Ch 04. 레이어 테스트](#ch-4-레이어-테스트)
  - [x] [Ch 05. 레이어 고도화](#ch-5-레이어-고도화)
  - [ ] Ch 06. 레이어 통합
- Part 3. Internal 솔루션
  - [x] [Ch 07. 솔루션 구조](#ch-8-솔루션-구조)
  - [ ] [Ch 08. 솔루션 설정](#ch-9-솔루션-설정)
  - [ ] Ch 09. 테스트
  - [ ] Ch 10. 빌드
  - [ ] Ch 11. 배포
- Part 4. Internal 전술 설계
  - [x] [Ch 12. 전술 설계 패턴](#ch-12-전술-설계-패턴)
  - [ ] TODO
- Part 5. External 솔루션
- Part 6. External 전술 설계
- Part 7. 전략 설계

<br/>

---

<br/>

# Part 1. 아키텍처

# Ch 1. 아키텍처 개요

## 아키텍처 정의
![](./.images/Architecture.png)

※ 출처: [Making Architecture Matter, 소프트웨어 아키텍처의 중요성](https://www.youtube.com/watch?v=4E1BHTvhB7Y)

## 아키텍처 범주
![](./.images/Architecture.Category.png)

※ 출처: [Making old applications new again](https://sellingsimplifiedinsights.com/asset/app-development/ASSET_co-modernization-whitepaper-inc0460201-122016kata-v1-en_1511772094768.pdf)

```
Application Architecture
  ├─ Monolithic Architecture
  ├─ Modular Monolithic Architecture
  ├─ N-tier Architecture
  └─ Microservices Architecture
      ├─ Internal Architecture
      │    └─ Layered Architecture
      │         ├─ Hexagonal Architecture
      │         ├─ Onion Architecture
      │         ├─ Clean Architecture
      │         ├─ Vertical Slice Architecture
      │         └─ ...
      │
      └─ External Architecture
           └─ 외부 시스템 구성 아키텍처: 예. CNCF Landscape
```
- Microservices Architecture = Internal Architecture + External Architecture
  ![](./.images/Architecture.Microservices.png)

  ※ 출처: [DDD 및 CQRS 패턴을 사용하여 마이크로 서비스에서 비즈니스 복잡성 처리](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/)

## 아키텍처 역사
![](./.images/Architecture.History.png)

<br/>

# Ch 2. 아키텍처 원칙
> - [아키텍처 원칙](https://learn.microsoft.com/ko-kr/dotnet/architecture/modern-web-apps-azure/architectural-principles)
>   - **Separation of concerns**

## 관심사의 분리
- **개발할 때**: 요구사항을 비즈니스와 기술 관심사로 분해합니다.
- **운영할 때**: 로그를 비즈니스와 기술 관심사로 식별합니다.

## 레이어
![](./.images/Layer.SoC.png)

- 개발 시 요구사항과 운영 시 로그는 서로 다른 시점이지만, **코드에 대한 관점은 관심사(레이어) 기준으로 일관되어야 합니다.**
  - **기술 관심사**
    - Adapter
      - Known 입력
      - Known 출력
      - Unknown 출력: 부수 효과(Side Effects)
  - **비즈니스 관심사**
    - Application: 비즈니스 흐름(Biz. Flow)
    - Domain: 비즈니스 단위(Biz. Unit)

## 레이어 배치
![](./.images/Layer.Alignment.png)

<br/>

# Ch 3. 레이어 격리

## 격리 전
![](./.images/Layer.Isolation.Before.png)
![](2024-10-30-00-14-30.png)

## 격리 후
![](./.images/Layer.Isolation.After.png)
- Strategy 패턴

<br/>

# Ch 4. 레이어 테스트

![](./.images/Layer.Isolation.Test.png)
- 단위 테스트: Biz. 관심사를 테스트합니다.
- 통합 테스트: Tech. 관심사까지 포함하여 Biz. 관심사를 테스트합니다.

<br/>

# Ch 5. 레이어 고도화

## 격리 고도화
![](./.images/Layer.Mediator.png)

- Mediator 패턴은 메시지를 Mediator 객체를 통해 간접적으로 전달하여 런타임 때도 호출자의 정보가 숨겨집니다.
  | 구분                       | Mediator  패턴                       | Strategy  패턴          |
  | ---                        | ---                                  | ---                     |
  | 호출자 정보 **컴파일 타임**  | Unknown                              | Unknown                 |
  | 호출자 정보 **런타임**      | Unknown                              | Known                   |
  | **통신**                   | 컴파일 타임과 런타임 모두 **간접**     | 컴파일 타임에만 **간접** |
- Mediator 패턴은 메시지로 의사소통 방식을 추상화합니다.

## 메시지 고도화
![](./.images/Layer.Decorator.png)
- Mediator 패턴은 Decorator 패턴과 조합하여 동적으로 메시지에 새 기능을 추가할 수 있습니다.
  - 예. 메시지 처리 시간 로그
  - 예. 입력 메시지 유효성 검사
  - 예. Command 메시지일 때 트랜잭션 처리(CQRS 패턴)

## 메시지 범주화(CQRS)
![](./.images/Layer.CQRS.png)

- Mediator 패턴은 CQRS(Command and Query Responsibility Segregation) 패턴과 조합하여 메시지를 Command 메시지와 Query 메시지로 분류할 수 있습니다.
  - Command 메시지: 데이터 `CUD`(`Create, Update, Delete`: **데이터 가변**)
  - Query 메시지: 데이터 `R`(`Read`: **데이터 불변**)

## 메시지 범주화(CQRS) 흐름
![](./.images/Layer.CQRS.Flow.png)
※ 출처: [Module Requests Processing via CQRS](https://github.com/kgrzybek/modular-monolith-with-ddd?tab=readme-ov-file#34-module-requests-processing-via-cqrs)  

| 구분       | Command  | Query      |
| ---        | ---      | ---        |
| 트랜잭션    | O(필요)  | X(불 필요)  |
| 구현       | ORM      | SQL        |
| DTO 변환   | O(필요)  | X(불 필요)  |
| SQL 복잡도 | ↓(낮다)  | ↑(높다)     |

- `트랜잭션`: Command은 데이터를 변경하기 때문에 트랜잭셩이 필요합니다.
- `구현`:  Query는 성능 향상을 위해 도메인 타임 변환 없이 SQL을 사용하여 DTO 데이터으로 바로 반환합니다.
- `SQL 복잡도`: Query는 상대적으로 Command에 비해 더 많은 Table 접근을 요구합니다.

<br/>

# Ch 6. 레이어 통합
- TODO

<br/>

---

<br/>

# Part 3. Internal 솔루션

# Ch 7. 솔루션 구조
- 예제 코드: [링크](./Ch07.SolutionStructure/)

## 솔루션 구조 템플릿
```shell
{Product}.sln
  │ # 범주 Abstraction: Backend와 Frontend을 구성하기 위해 필요한 부수적인 코드
  ├─Abstraction
  │   ├─Frameworks
  │   │   ├─{Corporation}.{Product}.Framework
  │   │   └─{Corporation}.{Product}.Framework.Contracts
  │   ├─Libraries
  │   │   └─{Corporation}.{Product}.{Tech}                                    // 예. RabbitMQ, ...
  │   └─Domains
  │       └─{Corporation}.{Product}.Domain                                    // 공유 도메인, ...
  │
  │ # 범주 Backend
  ├─Backend
  │   ├─{Service}
  │   │   ├─Src
  │   │   │   ├─{Corporation}.{Product}.{Service}                             // 호스트 프로젝트
  │   │   │   ├─{Corporation}.{Product}.{Service}.Adapters.Infrastructure     // Adapter 레이어
  │   │   │   ├─{Corporation}.{Product}.{Service}.Adapters.Persistence        // Adapter 레이어
  │   │   │   ├─{Corporation}.{Product}.{Service}.Application                 // Application 레이어
  │   │   │   └─{Corporation}.{Product}.{Service}.Domain                      // Domain 레이어
  │   │   └─Tests
  │   │       ├─{Corporation}.{Product}.{Service}.Tests.Integration           // Integration 테스트
  │   │       ├─{Corporation}.{Product}.{Service}.Tests.Performance           // Performance 테스트
  │   │       └─{Corporation}.{Product}.{Service}.Tests.Unit                  // Unit Test
  │   ├─{Service}
  │   │   ├─Src
  │   │   └─Tests
  │   └─Tests
  │       └─{Corporation}.{Product}.Tests.E2E                                 // End to End 테스트
  │
  │ # 범주 Frontend
  └─Frontend
      └─{UI}
          ├─Src
          │   ├─{Corporation}.{Product}.{UI}                                  // 호스트 프로젝트
          │   ├─{Corporation}.{Product}.{UI}.Adapters.Infrastructure          // Adapter 레이어
          │   ├─{Corporation}.{Product}.{UI}.Adapters.Persistence             // Adapter 레이어
          │   ├─{Corporation}.{Product}.{UI}.Application                      // Application 레이어
          │   └─{Corporation}.{Product}.{UI}.Domain                           // Domain 레이어
          └─Tests
              ├─{Corporation}.{Product}.{UI}.Tests.Integration                // Integration 테스트
              ├─{Corporation}.{Product}.{UI}.Tests.Performance                // Performance 테스트
              └─{Corporation}.{Product}.{UI}.Tests.Unit                       // Unit Test
```
- **Format**
  - Src: `T1.T2.T3.T4.{T5}`
    - `T1`: Corporation
    - `T2`: Product
    - `T3`: Category
    - `T4`: Layer
    - `T5`: Sub-Layer
    - 예. `{Corporation}.{Product}.{Service}.Domain`
    - 예. `{Corporation}.{Product}.{Service}.Adapters.Infrastructure`
  - Test: `T1.T2.T3.T4.T5`
    - `T1`: Corporation
    - `T2`: Product
    - `T3`: Category
    - `T4`: Tests
    - `T5`: Test Pyramid
    - 예. `{Corporation}.{Product}.{Service}.Tests.Unit`
    - 예. `{Corporation}.{Product}.{Service}.Tests.Integration`
- **Category**
  - Abstraction: Backend와 Frontend을 구성하기 위해 필요한 부수적인 코드
  - Backend
  - Frontend
- **Layer**
  - 기술 관심사
    - Adapters.Infrastructure
    - Adapters.Persistence
    - Adapters.Presentation
  - 비즈니스 관심사
    - Application: 비즈니스 흐름(Biz. Flow)
    - Domain: 비즈니스 단위(Biz. Unit)
- **Test Pyramid**
  - Unit
  - Integration
  - Performance
  - E2E(End to End)

## 솔루션 구조 예
![](./.images/SolutionExplorer.png)

- **이름**
  - {Corporation}: Corp
  - {Product}: Hello
  - {Service}:
    - Api
    - Master
  - {UI}: 생략

<br/>

# Ch 8. 솔루션 설정

## SDK 버전
- TODO global.json

## 빌드 설정 중앙화
- TODO Directory.Build.prop
- TODO ServerGarbageCollection

## 패키지 버전 중앙화
- TODO Directory.Package.prop

## 코드 분석
- 코드 스타일
- 코드 품질

## 컨테이너
- TODO 이름 규칙
- todo

<br/>

---

<br/>

# Part 4. Internal 전술 설계

# Ch 12. 전술 설계 패턴
![](./.images/TacticalDesign.Pattern.png)

<br/>

---

<br/>

# 참고 자료
- [ ] [SharedKernelSample](https://github.com/NimblePros/SharedKernelSample)
  - Domain과 Application 레이어 구현을 위한 기본 타입 기본 구현과 테스트 참고
- [ ] [C#10 `record struct` Deep Dive & Performance Implications](https://nietras.com/2021/06/14/csharp-10-record-struct/)

## 클린 아키텍처 템플릿
- [ ] [ardalis | CleanArchitecture](https://github.com/ardalis/CleanArchitecture)
- [ ] [amantinband | clean-architecture](https://github.com/amantinband/clean-architecture)

## DDD
- [ ] [Moving IO to the edges of your app](https://www.youtube.com/watch?v=P1vES9AgfC4)
  [![](https://img.youtube.com/vi/P1vES9AgfC4/0.jpg)](https://www.youtube.com/watch?v=P1vES9AgfC4)  
  - 아키텍처 관점에서 Pure Function과 Impure Function 배치의 중요성을 이해할 수 있습니다.
- [ ] [modular-monolith-with-ddd](https://github.com/kgrzybek/modular-monolith-with-ddd)

## 테스트
### 아키텍처 테스트
- [ ] [Enforcing Software Architecture With Architecture Tests](https://www.milanjovanovic.tech/blog/enforcing-software-architecture-with-architecture-tests)
- [ ] [Shift Left With Architecture Testing in .NET](https://www.milanjovanovic.tech/blog/shift-left-with-architecture-testing-in-dotnet)

## 설정
### 코드 분석
- [ ] [Editorconfig In Visual Studio In 10 Minutes or Less](https://www.youtube.com/watch?v=CQW5b58mPdg&t)
  - editorconfig 탭 간격, 마지막 라인, 네임스페이 기본 값(컴파일러 수준)
- [ ] [How To Write Clean Code With The Help Of Static Code Analysis](https://www.youtube.com/watch?v=0nVT1gM4vPg)
  - Directory.Build.props 파일을 이용한 코드 분석 패키지 전역화, 코드 분석을 위한 빌드 설정