## 완: 의존성 구성
### 프로젝트
1. AssemblyReference 파일

### Unit 테스트 프로젝트
1. xunit.runner.json 파일(모든 테스트 프로젝트)
1. Directory.Build.props 테스트 전용 파일
1. Abstractions/Constants/Constants.Constants.cs 파일
1. ArchitectureTests/ArchitectureBaseTest.cs 파일
1. ArchitectureTests/LayerDependencyTests.cs 파일

---

## 의존성 주입
- 옵션 값

## Logs

## Metrics

## Traces

---

## 완: Result
### Framework 프로젝트
1. Error
1. Result, ValidationResult

### Unit 테스트 프로젝트
1. Error
1. Result

---

## 완: CQRS
### Framework 프로젝트
1. ICommand, IQuery

### Unit 테스트 프로젝트
1. ArchitectureTests/NamingConventionsCQRSTests

---

## Known Pipeline(CQRS)
- 예외
- 로그
- 시간
- 유효성 검사
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