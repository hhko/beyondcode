- [x] 설정 | .gitignore
- [ ] 설정 | .gitattributes
- [ ] 설정 | .dockerignore
- [x] 설정 | global.json
- [x] 설정 | nuget.config
- [x] 설정 | .editorconfig
- [x] 설정 | Directory.Build.props
- [x] 설정 | Directory.Packages.props
- [ ] 설정 | .runsettings
- [ ] 설정 | local build script: code coverage
- [ ] 설정 | appsettings.json 복수개
- [ ] 설정 | Dockerization
---
- [ ] 빌드 | 코드 커버리지
- [ ] 빌드 | 클래스 다이어그램
- [ ] 빌드 | 프로젝트 의존성 다이어그램
- [ ] 빌드 | 순환 복잡도
- [ ] 빌드 | 코드 유사성
---
- [x] 솔루션 구조화 | Abstractions
- [ ] 솔루션 구조화 | Regstrations
- [ ] 솔루션 구조화 | AggregateRoots    // Domain Layer
- [ ] 솔루션 구조화 | Usecases          // Application Layer
- [ ] 솔루션 구조화 | LayerTests
- [ ] 도메인 레이어 구조화 | ...
- [ ] 애플리케이션 레이어 구조화 | ...
---
- [x] 유스케이스 | AssemblyReference.cs
- [ ] 유스케이스 | Request/Response???
- [ ] 유스케이스 | DTO
- [x] 유스케이스 | ICommand/ICommandUsecase
- [x] 유스케이스 | IQuery/IQueryUsecase
- [x] 유스케이스 | IResponse
- [x] 유스케이스 | IDomainEvent & IDomainEventUsecase
- [ ] 유스케이스 | 통합 DomainEvent
- [ ] 유스케이스 | 1.  Usecase Input Pipeline Validation
- [ ] 유스케이스 | 2.1 Usecase Input Pipeline OpenTelemetry Logs
- [ ] 유스케이스 | 2.2 Usecase Input Pipeline OpenTelemetry Traces
- [ ] 유스케이스 | 2.3 Usecase Input Pipeline OpenTelemetry Metrics
- [ ] 유스케이스 | 3.  Usecase Input Pipeline Transaction
- [ ] 유스케이스 | 4.  Usecase Input Pipeline Cache
- [ ] 유스케이스 | 5.  Usecase Input Pipeline
- [ ] 유스케이스 | Compile-time logging source generation
- [ ] 유스케이스 | Result
- [ ] 유스케이스 | Error
- [ ] 유스케이스 | Validation
---
- [ ] 어댑터 | IAdapter source generator
---
- [ ] 에러코드 | DomainErrors.{AggregateRoot}.{Reason}
  ```cs
  // DomainErrors.{Reason}.cs
  //    DomainErrors
  //        {메서드}Errors
  //            {Reason1}       <- DomainErrors.{AggregateRoot}.{Reason1}
  //            {Reason2}       <- DomainErrors.{AggregateRoot}.{Reason2}
  //            ...
  ```
- [ ] 에러코드 | ApplicationErrors.{CommandName}Errors.cs
  ```cs
  // ApplicationErrors.{Reason}.cs
  //    ...
  111
- [ ] 에러코드 | ApplicationErrors.{QueryName}Errors.cs
- [ ] 에러코드 | ApplicationErrors.{EventName}Errors.cs
---
- [x] 도메인 | Enumerations: Ardalis.SmartEnum
- [ ] 도메인 | Entity
- [ ] 도메인 | ValueObject
- [ ] 도메인 | Domain Service
- [ ] 도메인 | Aggregate Root
---
- [ ] 테스트 | Test Category
- [ ] 테스트 | xunit.runner.json
- [ ] 테스트 | Layer Dependency Test
- [ ] 테스트 | ICommand/ICommandUsecase NamingConventions
- [ ] 테스트 | iQuery/IQueryUsecase NamingConventions
- [ ] 테스트 | IDomainEvent/IDomainEventUsecase NamingConventions


## 지식

- 도메인 vs. 기술 분리
  - 도메인
    - Only CPU and Memory: IO excluded
    - async/await 없음
    - 순수 함수 지향
  - 기술
    - IO included
    - async/await 있음
    - 비순수 함수 지향향
- 도메인 서비스: 엔티티 n개와 관련있을 때
- 애그리게이트: 엔티티 0개와 관련있을 때때
- 엔티티
- 값
- 열거형