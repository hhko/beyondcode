---
outline: deep
---

# Domain 레이어

## Domain 레이어 패키지
- Ardalis.SmartEnum
- MediatR.Contracts
- ErrorOr
- Throw

## Domain 레이어 솔루션 구성
```shell
└─ {Corporation}.{Solution}.{Service}.Domain
   ├─ Abstractions                          // 도메인 레이어 부수 목표
   │  ├─ Enumerations                       // - 공용 열거형
   │  ├─ Entities                           // - 공용 엔티티
   │  ├─ Errors                             // - 공용 에러
   │  ├─ Events                             // - 공용 이벤트
   │  ├─ Utilities                          // - 공용 확장 메서드
   │  └─ ValueObjects                       // - 공용 값 객체
   │
   ├─ AggregateRoots                        // 도메인 레이어 주요 목표: Aggregate Root
   │  ├── {AggregateRoot}s
   │  │  ├─ Enumerations                    // - 열거형
   │  │  ├─ Errors                          // - 에러
   │  │  ├─ Events                          // - 이벤트
   │  │  ├─ ValueObjects                    // - 값 객체
   │  │  │
   │  │  ├─ {Entity}.cs                     // - 엔티티s
   │  │  ├─ {IAggregateRootRepository}.cs   // - Aggregate Root 리포지토리
   │  │  └─ {AggregateRoot}.cs              // - Aggregate Root
   │  │
   │  └─ ...
   │
   └─ AssemblyReference.cs
```

