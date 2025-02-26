# 도메인 주도 설계 기본

> "[Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)"을 기초하여 재구성하였습니다.

## 목표
- 지속 가능한 소프트웨어 개발을 위한 구현 순서를 이해합니다.
  > 비즈니스 관심사 -> 기술 관심사
- 도메인 지식을 코드로 표현하는 설계 패턴과 구조를 익힙니다.
  > Aggregate Root  
  > Use case
  > ...

## 목차
> 목차는 프로젝트 수행 과정에 따라 마일스톤 단위로 구성되어 있습니다.

- Part 1. 비즈니스 관심사
  - [ ] 마일스톤 1. 도메인 탐색
  - [ ] 마일스톤 2. 깊은 도메인 탐색
  - [ ] 마일스톤 3. 유스케이스
- Part 2. 기술 관심사
  - [ ] 마일스톤 4. 인프라스트럭처(WebApi)
  - [ ] 마일스톤 5. 영속성(Db)
  - [ ] 마일스톤 6. 서비스

## 솔루션 구성 원칙

| `방향`  | 관심사 `분리` | 목표 `분리` |
| --- | --- | --- |
| 위    | 기술 관심사(무한)      | 부수 목표(무한 -Abstractions-> 유한)  |
| 아래  | 비즈니스 관심사(유한)   | 주 목표(유한)   |

- **분리**
  - **관심사**: 비즈니스 관심사, 기술 관심사
  - **목표**: 주 목표, 부수 목표(붙을 부附, 따를 수隨: 주가 되는 것에 붙어 따르는 것)
- **방향**
  - **위**: 기술적으로(부수 목표) 더 중요한 것
  - **아래**: 비즈니스적으로(주 목표) 더 중요한 것

```
└─{T3}
    ├─Src
    │   ├─{T1}.{T2}.{T3}                            // Host                     // 위: 기술적으로(부수 목표) 더 중요한 것
    │   ├─{T1}.{T2}.{T3}.Adapters.Infrastructure    // Adapter 레이어              │
    │   ├─{T1}.{T2}.{T3}.Adapters.Persistence       // Adapter 레이어              │
    │   ├─{T1}.{T2}.{T3}.Application                // Application 레이어          ↓
    │   └─{T1}.{T2}.{T3}.Domain                     // Domain 레이어            // 아래: 비즈니스적으로(주 목표) 더 중요한 것
    │       ├─Abstractions                          // Domain 레이어 부수 목표   // 위: 기술적으로(부수 목표) 더 중요한 것
    │       └─AggregateRoots                        // Domain 레이어 주 목표     // 아래: 비즈니스적으로(주 목표) 더 중요한 것
    └─Tests
        ├─{T1}.{T2}.{T3}.Tests.Integration          // Integration 테스트       // 위: 기술적으로(부수 목표) 더 중요한 것
        ├─{T1}.{T2}.{T3}.Tests.Performance          // Performance 테스트          ↓
        └─{T1}.{T2}.{T3}.Tests.Unit                 // Unit 테스트              // 아래: 비즈니스적으로(주 목표) 더 중요한 것
```
