---
outline: deep
---

# 솔루션 구성

## 리포지토리 전략
- 모놀리스(Monolith)
- 멀티 레포(Multi Repo)
- 모노레포(Monorepo)

## 프로젝트 구조
```shell
{T2}.sln
  │ # Asset 범주: Frameworks + Libraries + Domains
  ├─ Assets
  │   ├─ Frameworks
  │   │   ├─ Src
  │   │   │   ├─ {T1}.{T2}.Framework
  │   │   │   └─ {T1}.{T2}.Framework.Contracts
  │   │   └─ Tests
  │   │       └─ {T1}.{T2}.Framework.Tests.Unit
  │   ├─ Libraries
  │   │   ├─ ...
  │   │   └─ {T1}.{T2}.[Tech]                                    // 예. RabbitMQ, ...
  │   └─ Domains
  │       ├─ Src
  │       │   └─ {T1}.{T2}.Domain
  │       └─ Tests
  │           └─ {T1}.{T2}.Domain.Tests.Unit                     // 공유 도메인
  │
  │ # Backend 범주
  ├─ Backend
  │   ├─ {T3}
  │   │   ├─ Src
  │   │   │   ├─ {T1}.{T2}.{T3}                                  // Host
  │   │   │   ├─ {T1}.{T2}.{T3}.Adapters.Infrastructure          // Adapter 레이어: Infrastructure
  │   │   │   ├─ {T1}.{T2}.{T3}.Adapters.Persistence             // Adapter 레이어: Persistence
  │   │   │   ├─ {T1}.{T2}.{T3}.Application                      // Application 레이어
  │   │   │   └─ {T1}.{T2}.{T3}.Domain                           // Domain 레이어
  │   │   └─ Tests
  │   │       ├─ {T1}.{T2}.{T3}.Tests.Integration                // Integration 테스트
  │   │       ├─ {T1}.{T2}.{T3}.Tests.Performance                // Performance 테스트
  │   │       └─ {T1}.{T2}.{T3}.Tests.Unit                       // Unit 테스트
  │   ├─ {T3}
  │   │   ├─ Src
  │   │   └─ Tests
  │   └─ Tests
  │       └─ {T1}.{T2}.Tests.E2E                                 // E2E(End to End) 테스트
  │
  │ # Frontend 범주
  └─ Frontend
      └─ {T3}
          ├─ Src
          │   ├─ {T1}.{T2}.{T3}                                  // Host
          │   ├─ {T1}.{T2}.{T3}.Adapters.Infrastructure          // Adapter 레이어: Infrastructure
          │   ├─ {T1}.{T2}.{T3}.Adapters.Persistence             // Adapter 레이어: Persistence
          │   ├─ {T1}.{T2}.{T3}.Adapters.Presentation            // Adapter 레이어: Presentation
          │   ├─ {T1}.{T2}.{T3}.Application                      // Application 레이어
          │   └─ {T1}.{T2}.{T3}.Domain                           // Domain 레이어
          └─ Tests
              ├─ {T1}.{T2}.{T3}.Tests.Integration                // Integration 테스트
              ├─ {T1}.{T2}.{T3}.Tests.Performance                // Performance 테스트
              └─ {T1}.{T2}.{T3}.Tests.Unit                       // Unit 테스트
```

### 프로젝트 구조 형식

| Level  | Src             | Tests            |
|------- |-------------    |--------------    |
| `{T1}` | Corporation     | Corporation      |
| `{T2}` | Solution        | Solution          |
| `T3`   | Service 또는 UI  | Service 또는 UI  |
| `T4`   | **Layers**      | **Tests**        |
| `T5`   | **Sub-Layers**  | **Test Pyramid** |

- Src
  - `T4` Domain: 비즈니스 단위(Biz. Unit)
  - `T4` Application: 비즈니스 흐름(Biz. Flow)
  - `T4` Adapters: 기술 관심사
    - `T5` Infrastructure
    - `T5` Persistence
    - `T5` Presentation
- Tests
  - `T4` Tests
    - `T5` Unit
    - `T5` Integration
    - `T5` Performance
    - `T5` E2E(End to End)

### 프로젝트 의존성 다이어그램
- TODO
  - 다이어그램
  - TOP -> DOWN 그림
  - 레이어 정의
  - 레이어 예제