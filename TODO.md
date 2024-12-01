- protected internal Result(TValue? value, Error error)
- private protected Result(Error error)
- 제네릭 타입(variance)
  - out TValue: 공변성 (Covariance)
    - 이는 제네릭 타입이 **반환 타입으로만** 사용될 수 있음을 보장합니다.
    - 공변성 덕분에 더 구체적인 타입으로 대체가 가능합니다. 즉, **서브타입   관계를 유지합니다.**
  - in TValue: 반공변성 (Contravariance)
    - 이는 제네릭 타입이 **입력 타입으로만** 사용될 수 있음을 보장합니다.
    - 반공변성 덕분에 더 일반적인 타입으로 대체가 가능합니다. 즉, **슈퍼타입   관계를 유지합니다.**

```
root = true

# All files
[*]
indent_style = space

# Xml files
[*.xml]
indent_size = 2

# C# files
[*.cs]

#### Core EditorConfig Options ####

# Indentation and spacing
indent_size = 4
tab_width = 4

# New line preferences
insert_final_newline = false

[*.{cs,vb}]
dotnet_analyzer_diagnostic.category-Style.severity = none

dotnet_diagnostic.MA0053.severity = warning
```

### Ch 9.5.1 코드 스타일("IDExxxx")
- 코드 스타일 인덱스: [링크](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/#index)

![](./.images/csharp_style_namespace_declarations.png)

```shell
# 코드 스타일: File Scoped 네임스페이스가 아닐 때
#   - IDE0160: Use block-scoped namespace
#   - IDE0161: Use file-scoped namespace
dotnet_diagnostic.IDE0161.severity = warning
csharp_style_namespace_declarations = file_scoped:warning
```

- [x] [네임스페이스 file_scoped](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161)
  ```ini
  dotnet_diagnostic.IDE0161.severity = warning
  csharp_style_namespace_declarations = file_scoped:warning
  ```
- [x] sealed: CA1852,	MA0053
  - CA only applies to internal types in assemblies that do not expose internal types and members and - by default - report types that inherit from [Exception] (https://learn.microsoft.com/en-us/dotnet/api/system.exception?WT.mc_id=DT-MVP-5003978), but cannot be configured to report types that define virtual members
- [x] [Meziantou.Analyzer's rules: .editorconfig - all rules disabled](https://github.com/meziantou/Meziantou.Analyzer/tree/main/docs#editorconfig---all-rules-disabled)
- [ ] 잘못된 네임스페이스
- [ ] [사용하지 않는 using 구문](https://learn.microsoft.com/ko-kr/dotnet/fundamentals/code-analysis/style-rules/ide0005?pivots=lang-csharp-vb)
  ```ini
  dotnet_diagnostic.IDE0005.severity = warning
  ```
- [ ] [primary 생성자](https://learn.microsoft.com/ko-kr/dotnet/fundamentals/code-analysis/style-rules/ide0290)
  ```ini
  dotnet_diagnostic.IDE0290.severity = warning
  csharp_style_prefer_primary_constructors = true:warning
  ```
- [ ] internal sealed class

```xml
<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
```
- `EnforceCodeStyleInBuild`: 명령줄 및 Visual Studio에서 빌드할 때 코드 스타일("IDExxxx") 분석을 사용하도록 설정할 수 있습니다.


### Ch 9.5.2 코드 분석
- TODO `AnalysisLevel`: latest
- TODO `AnalysisMode`: All
- TODO `CodeAnalysisTreatWarningsAsErrors`: true
- TODO 코드 품질
  - StyleCop.Analyzers
  - SonarAnalyzer.CSharp
- TODO 스레드 분석
- todo clr 메모리 분석
- https://swharden.com/blog/2023-03-05-dotnet-code-analysis/
- https://swharden.com/blog/2023-03-07-treemapping/

<br/>

- https://github.com/cybermaxs/awesome-analyzers?tab=readme-ov-file
  - https://www.meziantou.net/the-roslyn-analyzers-i-use.htm
  - https://github.com/dotnet/roslynator
  - Microsoft.CodeAnalysis.NetAnalyzers
  - https://github.com/meziantou/Meziantou.Analyzer/tree/main
  - https://github.com/code-cracker/code-cracker
  - https://github.com/SonarSource/sonar-dotnet

```

csharp_style_namespace_declarations = file_scoped:error
dotnet_diagnostic.IDE0161.severity = error

Filescope
internal sealed class ...

- Scalability
- Load Balancing
- Latency and Throughput
- Caching
- Consistency
- Availability
- Partitioning
- Replication
- Fault Tolerance
- CAP Theorem
- Database Indexing
- Rate Limiting
- Communication
- Reverse proxy
- Content delivery network
- Performance
- Streaming
- Batch Processing

```
- 전역
- 부분 변경
- 부분 제외
- 공통 설정
  - ServerGarbageCollection
- 확인: dotnet msbuild /pp

폴더별 빌드 사용자 지정
https://learn.microsoft.com/ko-kr/visualstudio/msbuild/customize-by-directory?view=vs-2022

Directory.Build.props - Centralize your builds
https://steven-giesel.com/blogPost/f3f46814-06c9-41b7-84fa-09ebb3305ed0


Understand Directory.Build.props: Centralizing .NET Project Configurations
https://blog.ndepend.com/directory-build-props/
```

# 패턴
## Internal 전술 설계 패턴
- [ ] 서비스
  - [ ] 파일 시스템(반복)
  - [ ] WebApi 서비스
  - [ ] RabbitMQ
- [ ] Assembly.cs
- [ ] 레이어 의존성 테스트
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



  - ServerGarbageCollection
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

- [API 게이트웨이, Domain-Driven Design example](https://github.com/draphyz/DDD)
- [API 게이트웨이 패턴과 클라이언트-마이크로 서비스 간 직접 통신](https://learn.microsoft.com/ko-kr/dotnet/architecture/microservices/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern)
- [Ocelot을 사용하여 API 게이트웨이 구현](https://learn.microsoft.com/ko-kr/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot)
```


## 도커 컴포즈 arg 전달
```shell
# .env

SERVICE_USER=hello
SERVICE_USER_ID=1000

# {T1}-{T2}       : 도커 컴포즈
# {T1}.{T2}       : 볼륨, 네트워크
# {T1}.{T2}.{T3}  : 서비스(services), 컨테이너(container_name), 호스트(hostname)
# {T1}/{T2}/{T3}  : 이미지

# docker-compose.yml
x-logging-common: &logging-common
  driver: "json-file"
  options:
    max-size: "10m"
    max-file: "7"

    env_file: .env
    build:
      context: .
      args:
        - SERVICE_USER=${SERVICE_USER}
        - SERVICE_USER_ID=${SERVICE_USER_ID}
      dockerfile: Services/Api/Src/Crop.Hello.Api/Dockerfile
    logging: *logging-common
    

#Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# 필수 패키지 설치
#   - procps        : ps -ef
#   - net-tools     : ifconfig
#   - iputils-ping  : ping
#   - curl          : curl
#   - sudo          : sudo
USER root
RUN apt-get update \
    && apt-get --no-install-recommends --no-install-suggests --yes --quiet install \
            procps \
            net-tools \
            iputils-ping \
            curl \
            sudo \
    && apt-get clean \
    && apt-get --yes --quiet autoremove --purge \
    && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/* \
              /usr/share/doc/* /usr/share/groff/* /usr/share/info/* /usr/share/linda/* \
              /usr/share/lintian/* /usr/share/locale/* /usr/share/man/*

...

FROM base AS final
ARG SERVICE_USER
ARG SERVICE_USER_ID

WORKDIR /app
COPY --from=publish /app/publish .

RUN addgroup --gid $SERVICE_USER_ID $SERVICE_USER \
    && adduser --uid $SERVICE_USER_ID --gid $SERVICE_USER_ID --disabled-password --gecos "" $SERVICE_USER \
    && chown -R $SERVICE_USER:$SERVICE_USER /app
USER $SERVICE_USER

ENTRYPOINT ["dotnet", "Corp.Hello.Api.dll"]

LABEL solution=hello
LABEL category=service
```

```
sudo usermod -aG sudo {계정}
id
groups
```

- DB 서비스
  ```cs
  private IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
  using var scope = _serviceScopeFactory.CreateScope();
  var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
  ```
- 옵션 접근
- 컨테이너 HealthCheck 


```
  C:\Assets\Domains\Tests\Crop.Hello.Domain.Unit\Crop.Hello.Domain.Unit.csproj : warning NU1903: 'System.Net.Http' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-7jgj-8wvc-jh57이(가) 있습니다.
  C:\Assets\Domains\Tests\Crop.Hello.Domain.Unit\Crop.Hello.Domain.Unit.csproj : warning NU1903: 'System.Text.RegularExpressions' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-cmhx-cq75-c4mj이(가) 있습니다.
  C:\Assets\Frameworks\Tests\Crop.Hello.Framework.Tests.Unit\Crop.Hello.Framework.Tests.Unit.csproj : warning NU1903: 'System.Net.Http' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-7jgj-8wvc-jh57이(가) 있습니다.
  C:\Assets\Frameworks\Tests\Crop.Hello.Framework.Tests.Unit\Crop.Hello.Framework.Tests.Unit.csproj : warning NU1903: 'System.Text.RegularExpressions' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-cmhx-cq75-c4mj이(가) 있습니다.
Crop.Hello.Domain.Unit 2 경고와 함께 성공 (0.8초) → Assets\Domains\Tests\Crop.Hello.Domain.Unit\bin\Debug\net8.0\Crop.Hello.Domain.Unit.dll
  C:\Assets\Domains\Tests\Crop.Hello.Domain.Unit\Crop.Hello.Domain.Unit.csproj : warning NU1903: 'System.Net.Http' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-7jgj-8wvc-jh57이(가) 있습니다.
  C:\Assets\Domains\Tests\Crop.Hello.Domain.Unit\Crop.Hello.Domain.Unit.csproj : warning NU1903: 'System.Text.RegularExpressions' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-cmhx-cq75-c4mj이(가) 있습니다.
Crop.Hello.Framework.Tests.Unit 2 경고와 함께 성공 (1.6초) → Assets\Frameworks\Tests\Crop.Hello.Framework.Tests.Unit\bin\Debug\net8.0\Crop.Hello.Framework.Tests.Unit.dll
  C:\Assets\Frameworks\Tests\Crop.Hello.Framework.Tests.Unit\Crop.Hello.Framework.Tests.Unit.csproj : warning NU1903: 'System.Net.Http' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-7jgj-8wvc-jh57이(가) 있습니다.
  C:\Assets\Frameworks\Tests\Crop.Hello.Framework.Tests.Unit\Crop.Hello.Framework.Tests.Unit.csproj : warning NU1903: 'System.Text.RegularExpressions' 4.3.0 패키지에 알려진 높은 심각도 취약성인 https://github.com/advisories/GHSA-cmhx-cq75-c4mj이(가) 있습니다.
```