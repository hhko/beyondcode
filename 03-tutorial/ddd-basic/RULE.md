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
