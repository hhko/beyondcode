- [x] 레이어 구성
- [x] 레이어 의존성 주입/관찰 가능성 옵션
- [ ] 관찰 가능성 콘솔 로그
---
- [ ] 컨테이너 구성
- [ ] 컨테이너 HealthCheck
---
- [ ] 관찰 가능성 로그 Aspire
- [ ] 관찰 가능성 로그 Grafana
- [ ] 관찰 가능성 로그 OpenSearch
---
- [ ] Error 타입
- [ ] IResult/IResult<T> 타입
- [ ] ValidationResult/ValidationResult<T> 타입
- [ ] Validation 로직
---
- [ ] CQRS 메시지 Meditor 패턴
- [ ] Command Decorator 패턴
- [ ] Query Decorator 패턴
---
- [ ] IAdapter 인터페이스
- [ ] IAdapter Decorator 패턴
---
- [ ] DTO
- [ ] Repository 패턴
- [ ] Unit of Work 패턴
- [ ] ORM(Command Repository)
- [ ] SQL(Query Repository)
---
- [ ] SSG
- [ ] 빌드 자동화
- [ ] 배포 자동화
---
- [ ] WebApi
- [ ] RabbitMQ
- [ ] 반복
- [ ] FileSystem
- [ ] FTP
---
- [ ] 관찰 가능성 추적
- [ ] 관찰 가능성 지표
---
- [ ] Retry
- [ ] 서킷 브레이커
- [ ] 캐시
---
- [ ] 아키텍처 테스트
- [ ] 단위 테스트
- [ ] 통합 테스트
- [ ] 성능 테스트
- [ ] Fake 데이터
- [ ] Moq
---
- [ ] Entity
- [ ] ValueObject
- [ ] Enum
- [ ] Domain Service
- [ ] Aggregate Root
- [ ] Domain Event
- [ ] Factory 패턴
---
- [ ] Specification 패턴
- [ ] Saga 패턴
- [ ] Outbox 패턴
---
- [ ] Api Gateway?

<br/>

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

## 로그
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