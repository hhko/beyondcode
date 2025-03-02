---
outline: deep
---

# 솔루션 프로젝트

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

```shell
{T}
├─ Src
│  ├─ {T}                          // Host               > 위(Up): 기술적으로 더 중요한 요소(부수 목표)
│  ├─ {T}.Adapters.Infrastructure  // Adapter Layer      > │
│  ├─ {T}.Adapters.Persistence     // Adapter Layer      > │
│  ├─ {T}.Application              // Application Layer  > ↓
│  └─ {T}.Domain                   // Domain Layer       > 아래(Down): 비즈니스적으로 더 중요한 요소(주 목표)
│     │
│     ├─ Abstractions                                    > 위(Up): 기술적으로 더 중요한 요소(부수 목표)
│     │                                                  > ↓
│     ├─ AggregateRoots                                  > 아래(Down): 비즈니스적으로 더 중요한 요소(주 목표)
│     └─ AssemblyReference.cs
│
└─ Tests
   ├─ {T}.Tests.Integration       // Integration Test    > 위(Up): 기술적으로 더 중요한 요소(부수 목표)
   ├─ {T}.Tests.Performance       // Performance Test    > ↓
   └─ {T}.Tests.Unit              // Unit Test           > 아래(Down): 비즈니스적으로 더 중요한 요소(주 목표)
```

- 레이어 주 목표
  | 레이어                | 주 목표                     |
  | ---                  | -------------               |
  | Adapter 레이어        | 무한(Infinite)              |
  | Application 레이어    | 유한(Finite): Use case       |
  | Domain 레이어         | 유한(Finite): Aggregate Root |
- Adapter 레이어
  - Infrastructure
  - Persistence
  - Presentation
- AssemblyReference.cs: 어셈블리 식별 역할
  ```cs
  using System.Reflection;

  // T1: {Corporation}
  // T2: {Solution}
  // T3: {Service}
  namespace Corporation.Solution.Service.Domain;

  public static class AssemblyReference
  {
      public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
  }
  ```

## 솔루션 구성 템플릿
```shell
📦 {Solution}
│  # 부수 목표
├─ Abstractions
│  ├─ Frameworks
│  │  ├─ Src
│  │  │  ├─ {Corporation}.{Solution}.Framework
│  │  │  └─ {Corporation}.{Solution}.Framework.Contracts
│  │  └─ Tests
│  │     └─ {Corporation}.{Solution}.Framework.Tests.Unit
│  ├─ Libraries
│  │  ├─ Src
│  │  │  ├─ {Corporation}.{Solution}.{Library-1}
│  │  │  ├─ {Corporation}.{Solution}.{Library-2}
│  │  │  └─ {Corporation}.{Solution}.{Library-N}
│  │  └─ Tests
│  │     ├─ {Corporation}.{Solution}.{Library-1}.Tests.Unit
│  │     ├─ {Corporation}.{Solution}.{Library-2}.Tests.Unit
│  │     └─ {Corporation}.{Solution}.{Library-3}.Tests.Unit
│  └─ Domains
│     ├─ Src
│     │  └─ {Corporation}.{Solution}.Domain
│     └─ Tests
│        └─ {Corporation}.{Solution}.Domain.Tests.Unit
│
│  # 주 목표
├─ Backends
│  ├─ {Service-1}
│  │  ├─ Src
│  │  │  ├─ {Corporation}.{Solution}.{Service-1}
│  │  │  ├─ {Corporation}.{Solution}.{Service-1}.Adapters.Infrastructure
│  │  │  ├─ {Corporation}.{Solution}.{Service-1}.Adapters.Persistence
│  │  │  ├─ {Corporation}.{Solution}.{Service-1}.Application
│  │  │  └─ {Corporation}.{Solution}.{Service-1}.Domain
│  │  └─ Tests
│  │     ├─ {Corporation}.{Solution}.{Service-1}.Tests.Integration
│  │     ├─ {Corporation}.{Solution}.{Service-1}.Tests.Performance
│  │     └─ {Corporation}.{Solution}.{Service-1}.Tests.Unit
│  ├─ {Service-2}
│  ├─ {Service-N}
│  └─ Tests
│     └─ .{Corporation}.{Solution}.Tests.E2E
│
│  # 주 목표
├─ Frontends
└─ Wiki
```

### 리포지토리 전략
- 모놀리스(Monolith)
- 멀티 레포(Multi Repo)
- 모노레포(Monorepo)