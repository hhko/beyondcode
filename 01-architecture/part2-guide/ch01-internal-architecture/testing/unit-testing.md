---
outline: deep
---

# 단위 테스트

## 단위 테스트 패키지
- xUnit
- Shouldly

## 단위 테스트 솔루션 구성
```shell
└─ {Service}
    ├─ Src
    │  ├─ {Solution}.{Service}
    │  ├─ {Solution}.{Service}.Adapters.Infrastructure
    │  ├─ {Solution}.{Service}.Adapters.Persistence
    │  ├─ {Solution}.{Service}.Application
    │  └─ {Solution}.{Service}.Domain
    │
    │  #
    │  # 테스트
    │  #
    └─ Tests
       └─ {Solution}.{Project}.Tests.Unit              # 단위 테스트
           │  # 부수 목표
           ├─ Abstractions
           │  └─ Constants
           │
           │  # 주요 목표: 레이어 단위 테스트
           └─ LayerTests
               ├─ Application                          # Application 레이어
               │  └─ {Usecase}Tests.cs                 # Usecase 테스트
               │
               └─ Domain                               # Domain 레이어
                   ├─ Constants
                   ├─ Factories
                   └─ {AggregateRoot}Tests.cs          # Aggregate Root 테스트
```