---
outline: deep
---

# 아키텍처 테스트

## 단위 테스트 템플릿
```shell
└─ {Service}
    ├─ Src
    │  ├─ {Solution}.{Service}
    │  ├─ {Solution}.{Service}.Adapters.Infrastructure
    │  ├─ {Solution}.{Service}.Adapters.Persistence
    │  ├─ {Solution}.{Service}.Application
    │  └─ {Solution}.{Service}.Domain
    └─ Tests
    └─ {Solution}.{Project}.Tests.Unit
        ├─ Abstractions
        │  └─ Constants
        └─ LayerTests
            ├─ Application
            │  └─ {UseCase}Tests.cs
            └─ Domain
                ├─ Constants
                ├─ Factories
                └─ {AggregateRoot}Tests.cs
```