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