---
outline: deep
---

# Internal 아키텍처 가이드

![hexagonal architecture](./../../part1-overview/ch04-internal-architecture/.images/Architecture.Internal.Hexagonal.png)

## 패키지
- [ ] 프레임워크 | ErrorOr
- [ ] 프레임워크 | Throw
- [ ] 프레임워크 | MediatR
- [ ] 프레임워크 | Ardalis.SmartEnum
- [ ] 프레임워크 | FluetValidaton
- [ ] 프레임워크 | Scrutor
- [ ] 프레임워크 | SonarAnalyzer.CSharp
- [ ] 프레임워크 | Riok.Mapperly
---
- [ ] 인프라스트럭처 | Serilog
- [ ] 인프라스트럭처 | OpenTelemetry
- [ ] 인프라스트럭처 | MassTransit
- [ ] 인프라스트럭처 | Quartz
- [ ] 인프라스트럭처 | FluentFTP
- [ ] 인프라스트럭처 | Polly
- [ ] 인프라스트럭처 | FusionCache
---
- [ ] 영속성 | EFCore
- [ ] 영속성 | Dapper
---
- [ ] 테스트 | xUnit.v3
- [ ] 테스트 | NSubstitute
- [ ] 테스트 | Shoudly
- [ ] 테스트 | NBomber(k6)
- [ ] 테스트 | Reqnroll
- [ ] 테스트 | coverlet
- [ ] 테스트 | ReportGenerator
- [ ] 테스트 | BenchmarkDotNet
- [ ] 테스트 | Testcontainers
- [ ] 테스트 | Verify

## 설정
- [x] 솔루션 설정 | [.gitignore](./settings/solution-gitignore.md)
- [x] 솔루션 설정 | [.gitattributes](./settings/solution-gitattributes.md)
- [ ] 솔루션 설정 | .dockerignore
- [ ] 솔루션 설정 | 전역 버전(도커 이미지 버전)
- [x] 솔루션 설정 | [global.json](./settings/solution-globaljson.md)
- [x] 솔루션 설정 | [nuget.config](./settings/solution-nugetconfig.md)
- [ ] 솔루션 설정 | .editorconfig
- [ ] 솔루션 설정 | Directory.Build.props
- [ ] 솔루션 설정 | Directory.Packages.props
---
- [ ] 프로젝트 설정 | InternalsVisibleTo
---
- [ ] WebApi 프로젝트 설정 | Microsoft.NET.Sdk.Web
---
- [ ] 테스트 프로젝트 설정 | appsettings.json 그룹화
- [ ] 테스트 프로젝트 설정 | Dockerization
- [ ] 테스트 프로젝트 설정 | xunit.runner.json
- [ ] 테스트 프로젝트 설정 | .runsettings
- [ ] 테스트 프로젝트 설정 | 테스트 Category

## 구성
- [ ] 폴더 구성 | 폴더 구성 원칙
- [ ] 폴더 구성 | 솔루션 폴더 구성
- [ ] 폴더 구성 | 프로젝트 폴더 구성
- [ ] 폴더 구성 | 도메인 레이어 폴더 구성
- [ ] 폴더 구성 | 애플리케이션 레이어 폴더 구성
- [ ] 폴더 구성 | 어덥터 레이어 폴더 구성

## CI/CD
- [ ] 빌드 | 빌드 스크립트
- [ ] 빌드 | 코드 커버리지
- [ ] 빌드 | 클래스 다이어그램
- [ ] 빌드 | 프로젝트 의존성 다이어그램
- [ ] 빌드 | ERD 다이어그램
- [ ] 빌드 | 테스트 목록 문서서
- [ ] 빌드 | Cucumber 문서
- [ ] 빌드 | 순환 복잡도
- [ ] 빌드 | 코드 유사성
- [ ] 빌드 | 단일 파일
---
- [ ] 배포 | 배포 스크립트
- [ ] 배포 | 설정 파일
- [ ] 배포 | 컨테이너 이미지
- [ ] 배포 | 패키지

## 로그
- [ ] 에러 코드 | DomainErrors.{AggregateRoot}Errors.{이유}
- [ ] 에러 코드 | ApplicationErrors.{CommandName}Errors.{이유}
- [ ] 에러 코드 | ApplicationErrors.{QueryName}Errors.{이유}
- [ ] 에러 코드 | ApplicationErrors.{EventName}Errors.{이유}
- [ ] 에러 코드 | AdapterErrors.{범주}Errors.{이유}

## 레이어
- [x] 공통 | [AssemblyReference.cs](./layers/common-assemblyreference.md)
- [x] 공통 | [접근 제어자](./layers//common-access-modifiers.md)
- [x] 공통 | [Pure Function](./layers/common-pure-function.md)
---
- [ ] 호스트 | Dump 파이프라인
- [ ] 호스트 | 로그(예외 처리)
- [ ] 호스트 | 윈도우 서비스
- [ ] 호스트 | 컨테이너화
- [ ] 호스트 | 컨테이너 유틸리티(Dump, ...)
---
- [ ] 어댑터 레이어 | IOption
- [ ] 어댑터 레이어 | IOption Validation
- [ ] 어댑터 레이어 | IAdapter 소스 생성기
- [ ] 어댑터 레이어 | IAdapter 파이프라인 Exception
- [ ] 어댑터 레이어 | IAdapter 파이프라인 OpenTelemetry Logs
- [ ] 어댑터 레이어 | IAdapter 파이프라인 OpenTelemetry Traces
- [ ] 어댑터 레이어 | IAdapter 파이프라인 OpenTelemetry Metrics
- [ ] 어댑터 레이어 | Repository 패턴 Command(ORM: EFCore)
- [ ] 어댑터 레이어 | Repository 패턴 Query(SQL: Dapper)
- [ ] 어댑터 레이어 | Unit of Work 패턴
---
- [ ] 어댑터 레이어 | External, WebApi
- [ ] 어댑터 레이어 | External, Quartz
- [ ] 어댑터 레이어 | External, RabbitMQ
- [ ] 어댑터 레이어 | External, gRPC
- [ ] 어댑터 레이어 | External, FTP
- [ ] 어댑터 레이어 | External, File System
- [ ] 어댑터 레이어 | External, Time/TimeZone
- [ ] 어댑터 레이어 | External, PostgreSQL
---
- [ ] 어댑터 레이어 | Resilience, Retry: 일시적 오류 발생 시 지정된 횟수만큼 재시도	API 호출 실패 시 3회 재시도
- [ ] 어댑터 레이어 | Resilience, Wait and Retry:	일정 시간 간격 또는 지수 백오프를 적용하여 재시도	네트워크 장애 시 점진적 재시도
- [ ] 어댑터 레이어 | Resilience, Circuit Breaker: 일정 횟수 이상 실패하면 일정 시간 동안 요청 차단	API 장애 감지 후 일정 시간 차단
- [ ] 어댑터 레이어 | Resilience, Advanced Circuit Breaker: 일정 기간 동안 특정 비율 이상의 실패 발생 시 차단	트래픽 대비 오류 비율이 높을 때 차단
- [ ] 어댑터 레이어 | Resilience, Fallback: 장애 발생 시 기본 응답 또는 대체 로직 실행	API 응답이 없을 때 기본값 반환
- [ ] 어댑터 레이어 | Resilience, Timeout: 지정된 시간 내에 응답이 없으면 요청 취소	데이터베이스 쿼리 5초 제한
- [ ] 어댑터 레이어 | Resilience, Bulkhead: 요청을 격리하여 특정 리소스 이상을 점유하지 않도록 제한	최대 동시 요청 수 제한
- [ ] 어댑터 레이어 | Resilience, Cache: 동일한 요청에 대해 이전 성공 결과를 재사용	API 응답을 일정 시간 동안 캐싱
- [ ] 어댑터 레이어 | Resilience, Rate Limit: 일정 시간 동안 요청 횟수를 제한	초당 10개 이상의 요청 차단
- [ ] 어댑터 레이어 | Resilience, 비동기 메시징: MassTransit + RabbitMQ로 장애 발생 시 메시지 큐 활용
- [ ] 어댑터 레이어 | Resilience, Saga 패턴: 데이터 일관성 유지, Saga 패턴을 활용하여 장애 발생 시 트랜잭션 보장
- [ ] 어댑터 레이어 | Resilience, 컨테이너 Health Check
---
- [ ] 애플리케이션 레이어 | CQRS
- [ ] 애플리케이션 레이어 | ICommand/ICommandUsecase
- [ ] 애플리케이션 레이어 | IQuery/IQueryUsecase
- [ ] 애플리케이션 레이어 | IResponse
- [ ] 애플리케이션 레이어 | IDomainEvent/IDomainEventUsecase
- [ ] 애플리케이션 레이어 | Integration DomainEvent
- [ ] 애플리케이션 레이어 | DTO
- [ ] 애플리케이션 레이어 | 1.  Usecase Input 파이프라인 Exception
- [ ] 애플리케이션 레이어 | 2.  Usecase Input 파이프라인 Validation
- [ ] 애플리케이션 레이어 | 3.1 Usecase Input 파이프라인 OpenTelemetry Logs
- [ ] 애플리케이션 레이어 | 3.2 Usecase Input 파이프라인 OpenTelemetry Traces
- [ ] 애플리케이션 레이어 | 3.3 Usecase Input 파이프라인 OpenTelemetry Metrics
- [ ] 애플리케이션 레이어 | 4.  Usecase Input 파이프라인 Transaction <- 메시지 전송과 시점 충돌?
- [ ] 애플리케이션 레이어 | 5.  Usecase Input 파이프라인 Cache
- [ ] 애플리케이션 레이어 | 컴파일 타임 logging 소스 생성기
- [ ] 애플리케이션 레이어 | Result 타입
- [ ] 애플리케이션 레이어 | Error 타입
- [ ] 애플리케이션 레이어 | Validation
---
- [ ] 도메인 레이어 | Enumerations
- [ ] 도메인 레이어 | Entity
- [ ] 도메인 레이어 | Value Object
- [ ] 도메인 레이어 | Factory
- [ ] 도메인 레이어 | Domain Service
- [ ] 도메인 레이어 | Aggregate Root

## 테스트
- [ ] 단위 테스트(아키텍처 테스트) | Layer Dependency Test
- [ ] 단위 테스트(아키텍처 테스트) | ICommand/ICommandUsecase NamingConventions
- [ ] 단위 테스트(아키텍처 테스트) | iQuery/IQueryUsecase NamingConventions
- [ ] 단위 테스트(아키텍처 테스트) | IDomainEvent/IDomainEventUsecase NamingConventions
---
- [ ] 단위 테스트(레이어 테스트) | 도메인 레이어
- [ ] 단위 테스트(레이어 테스트) | 애플리케이션 레이어
- [ ] 단위 테스트(레이어 테스트) | 애플리케이션 레이어(시나리오, Cucumber)
---
- [ ] 통합 테스트 | WebApi
- [ ] 통합 테스트 | Quartz
- [ ] 통합 테스트 | RabbitMQ
- [ ] 통합 테스트 | gRPC
- [ ] 통합 테스트 | FTP
- [ ] 통합 테스트 | File System
- [ ] 통합 테스트 | Time/TimeZone
- [ ] 통합 테스트 | PostgreSQL
---
- [ ] 성능 테스트 | WebApi
- [ ] 성능 테스트 | ...