---
outline: deep
---

# 도메인 에러

## 에러 코드
> `DomainErrors.{AggregateRoot}.{Reason}`

## 에러 클래스
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


## 에러 클래스 구성
```shell
Sessions                                        # Aggregate Root
└─ Errors
   ├─ DomainErrors.CancelReservationErrors.cs   # MethodName: CancelReservation
   └─ DomainErrors.ReserveSpotErrors.cs         # MethodName: ReserveSpot
```