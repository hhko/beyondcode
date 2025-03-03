---
outline: deep
---

# Domain 레이어

## Domain 레이어 패키지
- Ardalis.SmartEnum
- MediatR.Contracts
- ErrorOr
- Throw

## Domain 레이어 템플릿
```shell
└─ {Corporation}.{Solution}.{Service}.Domain
   ├─ Abstractions               // 부수 목표
   │  ├─ Enumerations            // - 열거형
   │  ├─ Entities                // - 엔티티 객체
   │  ├─ Errors                  // - 에러
   │  ├─ Events                  // - 이벤트
   │  ├─ Utilities               // - 확장 메서드
   │  └─ ValueObjects            // - 값 객체
   │
   ├─ AggregateRoots             // 주 목표: Aggregate Root
   │  ├── {AggregateRoot}s
   │  │  ├─ Enumerations         // - 열거형
   │  │  ├─ Errors               // - 에러
   │  │  ├─ Events               // - 이벤트
   │  │  ├─ ValueObjects         // - 값 객체
   │  │  │
   │  │  ├─ {Entity}.cs          // - 엔티티
   │  │  ├─ {Interface}.cs
   │  │  └─ {AggregateRoot}.cs   // - Aggregate Root
   │  │
   │  └─ ...
   │
   └─ AssemblyReference.cs
```

## Errors

```shell
Sessions                                        # Aggregate Root
└─ Errors
   ├─ DomainErrors.CancelReservationErrors.cs   # MethodName: CancelReservation
   └─ DomainErrors.ReserveSpotErrors.cs         # MethodName: ReserveSpot
```

- DomainErrors.{MethodName}Errors.cs
  - 코드
    - `Domain.{AggregateRoot}.{Reason}`
  - 클래스
    - `public static partial class DomainErrors`
    - `public static class {MethodName}Errors`
  - 템플릿
    - {MethodName}
    - {AggregateRoot}
    - {Reason}

```cs
public static partial class DomainErrors
{
    public static class {MethodName}Errors
    {
        public static readonly Error {Reason} = Error.Validation(
            code: $"{nameof(Domain)}.{nameof({AggregateRoot})}.{nameof({Reason})}",
            description: " ... ");
    }
}
```

## Domain 레이어 단위 테스트