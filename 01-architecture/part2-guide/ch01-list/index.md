---
outline: deep
---

# Internal 아키텍처 가이드

## 패키지지
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
- [ ] 테스트 | xUnit
- [ ] 테스트 | NSubstitute
- [ ] 테스트 | Shoudly
- [ ] 테스트 | NBomber(k6)
- [ ] 테스트 | Reqnroll
- [ ] 테스트 | ReportGenerator
- [ ] 테스트 | BenchmarkDotNet
- [ ] 테스트 | coverlet
- [ ] 테스트 | Testcontainers

## 설정
- [ ] 솔루션 설정 | .gitignore
- [ ] 솔루션 설정 | .gitattributes
- [ ] 솔루션 설정 | .dockerignore
- [ ] 솔루션 설정 | 전역 버전(도커 이미지 버전)
- [ ] 솔루션 설정 | global.json
- [ ] 솔루션 설정 | nuget.config
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
---
- [ ] 배포 | 배포 스크립트

## 로그
- [ ] 에러 코드 | DomainErrors.{AggregateRoot}Errors.{이유}
- [ ] 에러 코드 | ApplicationErrors.{CommandName}Errors.{이유}
- [ ] 에러 코드 | ApplicationErrors.{QueryName}Errors.{이유}
- [ ] 에러 코드 | ApplicationErrors.{EventName}Errors.{이유}
- [ ] 에러 코드 | AdapterErrors.{범주}Errors.{이유}

## 레이어
- [ ] 공통 | AssemblyReference.cs
- [ ] 공통 | 클래스 접근 제어자자
---
- [ ] 어댑터 레이어 | IOption
- [ ] 어댑터 레이어 | IOption Validation
- [ ] 어댑터 레이어 | WebApi
- [ ] 어댑터 레이어 | Scheduler
- [ ] 어댑터 레이어 | RabbitMQ
- [ ] 어댑터 레이어 | IAdapter 소스 생성기
- [ ] 어댑터 레이어 | IAdapter Pipeline Exception
- [ ] 어댑터 레이어 | IAdapter Pipeline OpenTelemetry Logs
- [ ] 어댑터 레이어 | IAdapter Pipeline OpenTelemetry Traces
- [ ] 어댑터 레이어 | IAdapter Pipeline OpenTelemetry Metrics
- [ ] 어댑터 레이어 | Repository 패턴 Command(EFCore)
- [ ] 어댑터 레이어 | Repository 패턴 Query(Dapper)
---
- [ ] 애플리케이션 레이어 | ICommand/ICommandUsecase
- [ ] 애플리케이션 레이어 | IQuery/IQueryUsecase
- [ ] 애플리케이션 레이어 | IResponse
- [ ] 애플리케이션 레이어 | IDomainEvent/IDomainEventUsecase
- [ ] 애플리케이션 레이어 | Integration DomainEvent
- [ ] 애플리케이션 레이어 | 1.  Usecase Input Pipeline Exception
- [ ] 애플리케이션 레이어 | 2.  Usecase Input Pipeline Validation
- [ ] 애플리케이션 레이어 | 3.1 Usecase Input Pipeline OpenTelemetry Logs
- [ ] 애플리케이션 레이어 | 3.2 Usecase Input Pipeline OpenTelemetry Traces
- [ ] 애플리케이션 레이어 | 3.3 Usecase Input Pipeline OpenTelemetry Metrics
- [ ] 애플리케이션 레이어 | 4.  Usecase Input Pipeline Transaction
- [ ] 애플리케이션 레이어 | 5.  Usecase Input Pipeline Cache
- [ ] 애플리케이션 레이어 | 컴파일 타임 logging 소스 생성기
- [ ] 애플리케이션 레이어 | Result 타입
- [ ] 애플리케이션 레이어 | Error 타입
- [ ] 애플리케이션 레이어 | Validation
---
- [ ] 도메인 레이어 | Enumerations
- [ ] 도메인 레이어 | Entity
- [ ] 도메인 레이어 | Value Object
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
---
- [ ] 통합 테스트 | WebApi
- [ ] 통합 테스트 | RabbitMQ
- [ ] 통합 테스트 | Quartz
- [ ] 통합 테스트 | Database
- [ ] 통합 테스트 | FTP
- [ ] 통합 테스트 | File
---
- [ ] 성능 테스트 | WebApi
- [ ] 성능 테스트 | ...