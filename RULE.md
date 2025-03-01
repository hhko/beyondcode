# 규칙

## 솔루션 구성 파일
- 형상관리
  - .gitignore
  - .gitattributes
- 설정
  - global.json
  - nuget.config
  - Directory.Packages.props
  - Directory.Build.props
  - .editorconfig
- 빌드 스크립트
  - .gitlab-ci.yml
  - .gitlab-ci-build.ps1
  - .gitlab-ci-deploy.local.ps1
  - .gitlab-ci-deploy.ps1


```


<br/>
<br/>
<br/>

## 코딩 컨벤션
- 타입
  - `var` 키워드를 사용하지 않는다.
  - `Target-typed new` 키워드를 사용한다.
- 클래스 접근 제어
  ```cs
  internal sealed class Foo
  ```


## Usecase
### 그룹
- AggregateRoots
- 의미
  - 예. Users  -> Profiles       // User 행위 주제 단위 CQRS
  - 예. 

### 메시지
```
{Verb}{EntityName}Command
{Verb}{EntityName}Query
{Verb}{EntityName}Event
```
### 메시지 Verb
```
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

### Usecase 반환 타입
- Response 생략할 때
  - Result
  - 그 외
    - Primitive 타입: 예. int, Guid, string, ...
    - Result 정의 타입: 예. Success, ...

public async Task<IErrorOr>
{
   ErrorOr<Guid> createAdminProfileResult = ...

   return createAdminProfileResult;       // x           <-- 확인 필요
}

public async Task<IErrorOr>
{
   return 1.ToErrorOr();                  // return 1;
}

public async Task<IErrorOr>
{
   ...

   return Result.Success.ToErrorOr();     // return Result.Sucess;
}


// 에러 N개 처리???
return reserveSpotResult.Errors
    .ToErrorOr();








```
{Solution}
└─ {Src}
   └─ Services
      └─ {Service}
         ├─ Src
         │  ├─ {Solution}.{Service}
         │  ├─ {Solution}.{Service}.Adapters.Infrastructure
         │  ├─ {Solution}.{Service}.Adapters.Persistence
         │  ├─ {Solution}.{Service}.Application
         │  │  ├─ Abstractions
         │  │  │  ├─ Registrations
         │  │  │  └─ Pipelines
         │  │  ├─ Usecases
         │  │  │  └─ {Usecase}
         │  │  │     ├─ Command
         │  │  │     ├─ Events
         │  │  │     ├─ IntegrationEvents
         │  │  │     └─ Queries
         │  │  │        ├─ {Usecase}Query.cs
         │  │  │        ├─ {Usecase}QueryResponse.cs
         │  │  │        ├─ {Usecase}QueryTelemetry.cs
         │  │  │        ├─ {Usecase}QueryUsecase.cs
         │  │  │        └─ {Usecase}QueryValidator.cs
         │  │  └─ AssemblyReference.cs
         │  └─ {Solution}.{Service}.Domain
         │     ├─ Abstractions
         │     │  ├─ BaseTypes
         │     │  ├─ Enumerations
         │     │  ├─ Errors
         │     │  ├─ Events
         │     │  └─ ValueObjects
         │     ├─ AggregateRoots
         │     │  └─ {AggregateRoot}s
         │     │     ├─ Enumerations
         │     │     ├─ Errors
         │     │     ├─ Events
         │     │     ├─ ValueObjects
         │     │     ├─ {Entity}.cs
         │     │     ├─ {Interface}.cs
         │     │     └─ {AggregateRoot}.cs
         │     └─ AssemblyReference.cs
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

```
Get   : 1개
List  : N개

```
Abstractions
   Repositories
      I{AggregateRoot}Repository.cs

Usecases
   {AggregateRoot}s
      Commands
         {Verb}{AggregateRoot}
            {Verb}{AggregateRoot}Command              <- Input
            {Verb}{AggregateRoot}CommandTelemetry
            {Verb}{AggregateRoot}CommandUsecase       <- Function
            {Verb}{AggregateRoot}CommandValidator
            {Verb}{AggregateRoot}Response             <- Output
      Queries
         {Verb}{AggregateRoot}
            {Verb}{AggregateRoot}Query                <- public sealed record    Input
            {Verb}{AggregateRoot}QueryTelemetry       <- internal sealed class
            {Verb}{AggregateRoot}QueryUsecase         <- internal sealed class   Function
            {Verb}{AggregateRoot}QueryValidator       <- internal sealed class
            {Verb}{AggregateRoot}Response             <- public sealed record    Output

      {AggregateRoot}Mappings.cs



### Command 메시지
public sealed record {Verb}{AggregateRoot}Command(
    {입력 타입}...)
    : ICommand<{Verb}{AggregateRoot}Response>;

public sealed record {Verb}{AggregateRoot}Response(u{출력 타입}, ...)
    : IResponse
{
   // TODO?
}

## Query 메시지
public sealed record {Verb}{AggregateRoot}Query()
    : IQuery<{Verb}{AggregateRoot}Response>;

public sealed record {Verb}{AggregateRoot}Response({출력 타입}, ...)
    : IResponse
{
   // TODO?
}

## Mapping

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


X? GetByIdAsync(Id)
if (GetByIdAsync(id) in not X x)
{
   // X 타입이 NULL이거나
}

왜 Usecase는 
   Aggregate Root 단위인가?




## 결과 값
// 성공
값.
   .ToResponse()
   .ToErrorOr();

// 실패
Error.
   . ...
   .ToErrorOr();

## .editorconfig
https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161