# 솔루션 폴더 구성 원칙

- **관심사의 분리: 비즈니스 관심사 vs. 기술 관심사**
  - 관심사의 분리는 레이어로 구분됩니다.
    - Adapter 레이어: 기술 관심사
    - Application 레이어: 비즈니스 관심사 (도메인 흐름)
    - Domain 레이어: 비즈니스 관심사 (도메인 단위)
- **목표의 분리: 주요 목표 vs. 부수 목표 (주된 목표에 따르는 부수적인 목표)**
  - 목표의 분리는 배치 방향으로 구분됩니다.
    - 위쪽: 기술적인 측면에서 더 중요한 것(부수 목표: Abstractions)을 배치합니다.
    - 아래쪽: 비즈니스 측면에서 더 중요한 것(주요 목표)을 배치합니다.

| 방향    | 관심사의 분리           | 목표의 분리                                 |
| ---    | ---                             | ---                               |
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