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

## 에러 처리

### 에러 코드
> `DomainErrors.{AggregateRoot}.{Reason}`

### 에러 구현 템플릿
```cs
public static partial class DomainErrors
{
    public static class {MethodName}Errors
    {
        public static readonly Error {Reason} = Error.Validation(
            // 에러 코드
            code: $"{nameof(DomainErrors)}.{nameof({AggregateRoot})}.{nameof({Reason})}",

            // 에러 원인
            description: " ... ");
    }
}
```

- 파일 이름
  - DomainErrors.{MethodName}Errors.cs
- 클래스
  - `public static partial class DomainErrors`
  - `public static class {MethodName}Errors`
- 템플릿 변수
  - `{MethodName}`
  - `{AggregateRoot}`
  - `{Reason}`


### 에러 구성 예
```shell
Sessions                                        # Aggregate Root
└─ Errors
   ├─ DomainErrors.CancelReservationErrors.cs   # MethodName: CancelReservation
   └─ DomainErrors.ReserveSpotErrors.cs         # MethodName: ReserveSpot
```