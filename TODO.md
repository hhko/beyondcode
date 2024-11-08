# 패턴
## Internal 전술 설계 패턴
- [ ] 서비스
  - [ ] 파일 시스템(반복)
  - [ ] WebApi 서비스
  - [ ] RabbitMQ
- [x] 의존성(Strategy 패턴)
- [x] .josn 옵션 패턴
- [ ] Result/Error
- [x] Mediator 패턴
  - [ ] [REPR Pattern(Request-Endpoint-Response)](https://code-maze.com/aspnetcore-repr-request-endpoint-response-pattern/)
- [x] Decorator 패턴(Known Input/Output: Message)
  - [ ] Validation 패턴(Application Service)
  - [ ] 로그
  - [ ] 예외
- [ ] Decorator 패턴(Unknown Input/Output: Adapter)
  - [ ] 로그
  - [ ] 예외
- [x] [CQRS 패턴](https://www.kamilgrzybek.com/blog/posts/simple-cqrs-implementation-raw-sql-ddd)
- [ ] Mapping(DTO)
- [ ] Repository 패턴
- [ ] UoW 패턴
- [ ] Retry 패턴
- [ ] Circuit Breaker 패턴
- [ ] Saga 패턴
- [ ] Specification 패턴
- [ ] [Outbox 패턴](https://www.kamilgrzybek.com/blog/posts/the-outbox-pattern)

## External 전술 설계 패턴
- [ ] Pub/Sub 패턴
- [ ] API Gateway/Load Balancer 패턴
- [ ] BFF(Backend For Frontend) 패턴
- [ ] Cache
- [ ] 분산 락
- [ ] CDC

```

- SDK 버전 global.json
- 빌드 설정 중앙화
  - ServerGarbageCollection
- 패키지 버전 중앙화
- 코드 정적 분석
  - 코드 스타일
  - 코드 품질?
  - 코드 정적 분석
    - https://github.com/MarvinDrude/DotNet.GitHubActionMetrics
    - https://github.com/dotnet/samples/tree/main/github-actions/DotNet.GitHubAction
- 도커
- 도커 컴포즈

<br/>

- `TreatWarningsAsErrors`: Treat all warnings as errors.
- `CodeAnalysisTreatWarningsAsErrors`: Treat code quality (CAxxxx) warnings as errors.
- `EnforceCodeStyleInBuild`: Enables code-style analysis ("IDExxxx") rules.
  - code quality analysis: EnableNETAnalyzers
    - .NET code quality analysis is enabled, by default,
  - code style analysis: EnforceCodeStyleInBuild
    - .NET code style analysis is disabled, by default,
- `AnalysisLevel`: Specifies which analyzers to enable. The default value is latest.
- `AnalysisMode`: Configures the predefined code analysis configuration.



<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
<CodeAnalysisTreatWarningsAsErrors>true</CodeAnalysisTreatWarningsAsErrors>
<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
<AnalysisLevel>latest</AnalysisLevel>
<AnalysisMode>All</AnalysisMode>

StyleCop.Analyzer
SonarAlayzer.CSharp

Direcory.Build.Props <-- 분석 패키지, 분석 설정

스레드 분석
clr 메모리 분석


- https://code-maze.com/entity-framework-core-best-practices/


Meziantou.Analyzer
Microsoft.VisualStudio.Threading.Analyzers
Microsoft.CodeAnalysis.BannedApiAnalyzers

- [Understanding the impact of Roslyn Analyzers on build time](https://www.meziantou.net/understanding-the-impact-of-roslyn-analyzers-on-the-build-time.htm)


- 전략 설계
  ![](./.images/problemspace-and-solutionspace.png)  
  ※ 이미지 출처: [Strategic Design Explained](https://miro.medium.com/v2/resize:fit:1400/format:webp/1*vJzxC1yeMtIKxuk-8Fj8YA.png)

<br/>

- 패키지 버전 중앙화
  - https://devblogs.microsoft.com/dotnet/dotnet-upgrade-assistant-cpm-upgrade/

<br/>

- [API 게이트웨이, Domain-Driven Design example](https://github.com/draphyz/DDD)
- [API 게이트웨이 패턴과 클라이언트-마이크로 서비스 간 직접 통신](https://learn.microsoft.com/ko-kr/dotnet/architecture/microservices/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern)
- [Ocelot을 사용하여 API 게이트웨이 구현](https://learn.microsoft.com/ko-kr/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot)
```