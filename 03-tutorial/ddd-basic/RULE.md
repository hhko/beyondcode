- 솔루션 | .gitignore
- 솔루션 | .gitattributes
- 솔루션 | global.json
- 솔루션 | nuget.config
- 솔루션 | Directory.Packages.props
- 솔루션 | Directory.Build.props
- 솔루션 | .editorconfig
- 솔루션 | 솔루션 빌드 로컬: 코드 커버리지
- 솔루션 | 솔루션 빌드 로컬: 정적 분석
- 솔루션 | 솔루션 빌드 로컬: 프로젝트 참조 다이어그램
- 솔루션 | 솔루션 배포

---

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

Verb
- Create       // 1개
  - Add        // 매열 멤버 변수
  - Delete
  - Get?
  - Update?
- Get          // 1개
- List         // N개
- Delete?
- Update?

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