

<br/>

```dockerfile

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

WORKDIR /app
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

ENTRYPOINT ["dotnet", "Crop.Solution.Service.dll"]

LABEL solution=solution
LABEL category=service
```
```yml
x-logging-common: &logging-common
  driver: "json-file"
  options:
    max-size: "10m"
    max-file: "7"

services:
  corp.solution.service:
    env_file: .env
    image: corp.solution.service:${SERVICE_TAG}
    build:
      context: .
      args:
        - SERVICE_USER=${SERVICE_USER}
        - SERVICE_USER_ID=${SERVICE_USER_ID}
      dockerfile: Corp.Solution.Service\Src\Corp.Solution.Service\Dockerfile
    container_name: corp.solution.service
    hostname: corp.solution.service
    networks:
      - net
    logging: *logging-common

networks:
  net:
    name: crop.solution
```


## 완: 프로젝트 구성(레이어 구성)
- [X] AssemblyReference 파일
- [X] 단위 테스트 | xunit.runner.json 파일(모든 테스트 프로젝트)
- [X] 단위 테스트 | Directory.Build.props 테스트 전용 파일
- [X] 단위 테스트 | Abstractions/Constants/Constants.Constants.cs 파일
- [X] 단위 테스트 | ArchitectureTests/ArchitectureBaseTest.cs 파일
- [X] 단위 테스트 | ArchitectureTests/LayerDependencyTests.cs 파일

## 관찰 가능성 옵션/의존성
- [X] 옵션 패턴: IConfigureOptions, IValidateOptions
- [X] 의존성 폴더 구성
  - XxxRegistration
  - RegisterXxx
- [X] 옵션 의존성 주입
  - ConfigureOptions
  - AddSingleton
- [x] appsettings.json
- [x] 단위 테스트: 옵션 네이밍
- [ ] 통합 테스트: 옵션 네이밍

## 관찰 가능성 로그
- [ ] 구조적 로그 Microsoft
- [ ] 구조적 로그 Microsoft, Error
- [ ] 구조적 로그 Microsoft, Exception
- [ ] 로그 시스템 Aspire
- [ ] 로그 시스템
- [ ] 로그 시스템 OpenSearch
- [ ] 구조적 로그 Serilog
- [ ] 구조적 로그 Serilog, Error
- [ ] 구조적 로그 Serilog, Exception

## 컨테이너
- [ ] 도커 파일
- [ ] 도커 컴포즈
- [ ] 헬스체크
- [ ] 로그?

---

## 의존성 주입
- 옵션 값

## Logs

## Metrics

## Traces

---

## 완: Result
### Framework 프로젝트
- [x] Error
- [x] Result, ValidationResult

### Unit 테스트 프로젝트
- [x] Error
- [x] Result
- [ ] Error -> IError
---

## 완: CQRS
### Framework 프로젝트
- [x] ICommand, IQuery
- [ ] ICachedQuery

### Unit 테스트 프로젝트
- [x] ArchitectureTests/NamingConventionsCQRSTests

---

## Known Pipeline(CQRS)
- 유효성 검사
- 로그
- QueryCachingPipeline
---
- 예외
- 시간
- 트랜잭션

---

## Unknown Pipeline

- 예외
- 로그
- 시간

---

## Usecase Validation

## 에러 코드

## 통합 테스트(컨테이너)
- RabbitMQ

## 애플리케이션 타입
- Domain Event

## 도메인 타입
- Entity
- ValueObject
- Domain Service
- Enum

## CQRS
- Command: EFCore
- Query: Dapper

## Adapter
- 반복 작업
- RabbitMQ
- WebApi

## Cache?

## 컨테이너화
- 환경 설정


## TODO
- [ ] CI/CD
- [ ] Mapping(DTO)
- [ ] Repository 패턴
- [ ] UoW 패턴
- [ ] Retry 패턴
- [ ] Circuit Breaker 패턴
- [ ] Saga 패턴
- [ ] Specification 패턴
- [ ] [Outbox 패턴](https://www.kamilgrzybek.com/blog/posts/the-outbox-pattern)
- [ ] 트랜잭션 스크립트 vs 모델

```
root = true

[*.cs]
# CA1501: DepthOfInheritance(5)
#   상속성을 너무 많이 사용하지 마십시오
#   Avoid excessive inheritance
#   https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1501
dotnet_diagnostic.CA1501.severity = error

# CA1502: CyclomaticComplexity(25)
#   지나치게 복잡하게 만들지 마십시오(순환 복잡성)
#   Avoid excessive complexity
#   https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1502
dotnet_diagnostic.CA1502.severity = error

# CA1505: MaintainabilityIndex(10)
#   유지 관리할 수 없는 코드는 사용하지 마십시오
#   Avoid unmaintainable code
#   https://learn.microsoft.com/ko-kr/dotnet/fundamentals/code-analysis/quality-rules/ca1505
dotnet_diagnostic.CA1505.severity = error

# CA1506: ClassCoupling(95/40)
#   클래스 결합을 지나치게 많이 사용하지 마십시오
#   Avoid excessive class coupling
#   https://learn.microsoft.com/ko-kr/dotnet/fundamentals/code-analysis/quality-rules/ca1506
#dotnet_diagnostic.CA1506.severity = error
```