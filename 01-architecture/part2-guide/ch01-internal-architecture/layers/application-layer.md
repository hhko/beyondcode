---
outline: deep
---

# Application 레이어

## Application 레이어 패키지
- MediatR
- FluentValidation
- ErrorOr
- OpenTelemetry

## Application 레이어 솔루션 구성

```shell
└─ {Corporation}.{Solution}.{Service}.Application
   ├─ Abstractions                                          // 부수 목표
   │  ├─ Registrations                                      // - 의존성 등록
   │  ├─ Pipelines                                          // - MediatR 파이프라인
   │  └─ ...
   │
   ├─ Usecases                                              // 주 목표
   │  ├── {Usecase}                                         // - 유스케이스
   │  │   ├─ Commands                                       // - Command 유스케이스
   │  │   │  └─ {CommandName}                               //   - Command 이름
   │  │   │      ├─ {CommandName}Command.cs                 //     - Command Input: DTO
   │  │   │      ├─ {CommandName}CommandTelemetry.cs        //     - Command Telemetry: 메시지 로그, 추적, 지표
   │  │   │      ├─ {CommandName}CommandUsecase.cs          //     - Command Usecase: 메시지 처리
   │  │   │      ├─ {CommandName}CommandValidator.cs        //     - Command Validator: 메시지 유효성 검사
   │  │   │      └─ {CommandName}Response.cs                //     - Command Output: DTO
   │  │   │
   │  │   ├─ Errors                                         // - Error
   │  │   │  ├─ ApplicationErrors.{CommandName}Errors.cs    //   - CommandName 에러
   │  │   │  ├─ ApplicationErrors.{QueryName}Errors.cs      //   - QueryName 에러
   │  │   │  ├─ ApplicationErrors.{EventName}Errors.cs      //   - EventName 에러
   │  │   │  └─ ...
   │  │   │
   │  │   ├─ Events                                         // - Event 유스케이스
   │  │   │
   │  │   └─ Queries                                        // - Query 유스케이스
   │  │      └─ {QueryName}                                 //   - Query 이름
   │  │          ├─ {QueryName}Query.cs                     //     - Query Input: DTO
   │  │          ├─ {QueryName}QueryTelemetry.cs            //     - Query Telemetry: 메시지 로그, 추적, 지표
   │  │          ├─ {QueryName}QueryUsecase.cs              //     - Query Usecase: 메시지 처리
   │  │          ├─ {QueryName}QueryValidator.cs            //     - Query Validator: 메시지 유효성 검사
   │  │          └─ {QueryName}Response.cs                  //     - Query Output: DTO
   │  └─ ...
   │
   └─ AssemblyReference.cs
```

## CQRS 구성
- MediatR의 IRequest 인터페이스를 CQRS 원칙에 맞춰 보다 명확하게 표현하기 위해, 입력 메시지를 ICommand와 IQuery로 구분합니다.
- 출력 타입을 `IResponse`으로 표준화 시킵니다.

### ICommand 인터페이스
```cs
public interface ICommand : IRequest<IErrorOr>;

public interface ICommand<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse;

public interface ICommandUsecase<in TCommand> : IRequestHandler<TCommand, IErrorOr>
    where TCommand : ICommand;

public interface ICommandUsecase<in TCommand, TResponse> : IRequestHandler<TCommand, IErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResponse;
```

### IQuery 인터페이스
```cs
public interface IQuery<out TResponse> : IRequest<IErrorOr<TResponse>>
    where TResponse : IResponse;

public interface IQueryUsecase<in TQuery, TResponse> : IRequestHandler<TQuery, IErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IResponse;
```

### 출력 인터페이스
```cs
public interface IResponse;
```

## CQRS 구현
### Verb
> QueryName: Verb + Name + `Query`

```shell
- Create       // 1개
  - Add        // 매열 멤버 변수
  - Delete
  - Get?
  - List?
  - Update?
- Get          // 1개
- List         // N개
- Delete?
- Update?
```

### 클래스 접근 제어자
```
{QueryName}Query                <- public sealed record
{QueryName}QueryTelemetry       <- internal sealed class
{QueryName}QueryUsecase         <- internal sealed class
{QueryName}QueryValidator       <- internal sealed class
{QueryName}Response             <- public sealed record
```

### Output DTO
- Response 생략할 때
  - 사전 정의된 Result 타입: 예. Success
  - 성공: Result을 통해 1:1 값 전달할 때
  - 성공: primitive 값 전달할 때(1:1 값 전달할 때)
  - 실패: Result의 Error N개을 전달할 때

```cs
// 사전 정의된 Result 타입: 예. Success
public async Task<IErrorOr>
{
   ...

   return Result.Success.ToErrorOr();     // return Result.Sucess;
}

// 성공: Result을 통해 1:1 값 전달할 때
public async Task<IErrorOr>
{
   ErrorOr<Guid> createAdminProfileResult = ...

   return createAdminProfileResult;       // x           <-- 확인 필요
}

// 성공: primitive 값 전달할 때
public async Task<IErrorOr>
{
   return 1.ToErrorOr();                  // return 1;
}

// 실패: 에러 N개 처리???
return reserveSpotResult.Errors
    .ToErrorOr();
```

### Command 메시지
```cs
// {CommandName}Command: 입력 메시지
// {CommandName}Response: 출력 메시지

// 입력 메시지: 출력 메시지가 있을 때
public sealed record {CommandName}Command(      // 입력 메시지
    {입력 타입},                                 // - 입력 파라미터
    ...,
    ...) : ICommand<{CommandName}Response>;     // 출력 메시지

// 입력 메시지: 출력 메시지가 없을 때
public sealed record {CommandName}Command(      // 입력 메시지
    {입력 타입},                                 // - 입력 파라미터
    ...,
    ...) : ICommand;

// 출력력 메시지
public sealed record {CommandName}Response(     // 출력 메시지
    {출력 타입},                                 // - 출력 파라미터
    ...,
    ...) : IResponse;
```

### Query 메시지
```cs
// {QueryName}Query: 입력 메시지
// {QueryName}Response: 출력 메시지

// 입력 메시지: 출력 메시지가 있을 때
public sealed record {QueryName}Command(      // 입력 메시지
    {입력 타입},                               // - 입력 파라미터
    ...,
    ...) : IQuery<{QueryName}Response>;       // 출력 메시지

// 입력 메시지: 출력 메시지가 없을 때
public sealed record {QueryName}Command(      // 입력 메시지
    {입력 타입},                               // - 입력 파라미터
    ...,
    ...) : IQuery;

// 출력력 메시지
public sealed record {QueryName}Response(     // 출력 메시지
    {출력 타입},                               // - 출력 파라미터
    ...,
    ...) : IResponse;
```

### Event 구현
```cs
// 개선 전
Participant? participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId);
if (participant is null)
{
    throw new DomainEventException(ReservationCanceledEventErrors.ParticipantNotFound);
}

// 개선 후
Participant participant = await _participantsRepository.GetByIdAsync(domainEvent.Reservation.ParticipantId)
    ?? throw new DomainEventException(ReservationCanceledEventErrors.ParticipantNotFound);

```

### Mapping
```cs
public static class {AggregateRoot}Mapping
{
    public static {Verb}{AggregateRoot}Response ToResponse(
        this {출력 타입})
    {
        return new {Verb}{AggregateRoot}Response(
         ...
        );
    }
}
```

## Errors

### 에러 코드
> - `ApplicationErrors.{CommandName}.{Reason}`
> - `ApplicationErrors.{QueryName}.{Reason}`

### 에러 구현 템플릿
```cs
public static partial class ApplicationErrors
{
    public static class {CommandName}Errors
    {
        public static readonly Error {Reason} = Error.Validation(
            code: $"{nameof(ApplicationErrors)}.{nameof({CommandName})}.{nameof({Reason})}",
            description: " ... ");
    }
}
```

- 파일 이름
  - ApplicationErrors.{CommandName}Errors.cs
- 클래스
  - `public static partial class ApplicationErrors`
  - `public static class {CommandName}Errors`
- 템플릿 변수
  - `{CommandName}`
  - `{Reason}`

### 에러 구성 예
```shell
Authentication
└─ Errors
   └─ ApplicationErrors.LoginQueryErrors.cs   # QueryName: LoginQuery
```
