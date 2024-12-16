## 레이어
- 솔루션 구성
  - 제품
    - Adapters.Infrastructure
    - Adpaters.Persistence
    - Adapters.Presnetation
    - Application
    - Domain
  - 어셈블리
    - AssemblyReference 구현
  - Asserts
    - Frameworks
    - Libraries
    - Domains
- 솔루션 빌드
  - Directory.Build.props(전체, 테스트)
    - .NET
    - 버전
    - 메타
  - Directory.Packages.props(구분)
  - global.json
  - nuget.config
  - .editorconfig
    - 네임스페이스: File scope
    - await 없는 async 메서드
  - .gitignore
  - .gitattirute
- 아키텍처 단위 테스트
  - 폴더 구성
    - Abstractions
    - ArchitectureTests
  - 레이어 의존성 테스트
  - 어셈블리 AssemblyReference 테스트

## 배포 구성
- appsettings 배포 N개
- 도커 컴포즈 N개
- 도커 컴포즈 통합 테스트
  - 서비스
  - 인프라

## 호스트

| IHost    | Windows Service | Docker | Integration Test | Performance Test | Pipeline(Exception) |
| ---      | ---             | ---    | ---              | ---              | ---                 |
| Schedule | O               | O      | O                |                  |                     |
| WebApi   |                 |        |                  |                  |                     |
| RabbitMQ |                 |        |                  |                  |                     |
| gRPC     |                 |        |                  |                  |                     |

- IHost 구현
- 레이어 의존성 구성
  ```
  Abstractions/
    Registration/
      {레이어}Registration.cs
  ```
- 레이어 의존성 옵션 등록
  ```
  - appsettings.json
  -> {Featrue}Options
  -> {Feature}OptionsSetup : IConfigureOptions<{Feature}Options>
  -> {Feature}OptionsValidator : IValidateOptions<{Feature}Options>
  ```
  - 옵션 데이터: XyzOptions
  - 옵션 데이터 읽기: IConfigureOptions
  - 옵션 유효성 검사: IValidateOptions
- 로그
- 레이어 의존성 통합 테스트
  - Valid: 1개
    - appsettings.Valid.json
  - Invalid: N개
    - appsettings.Invalid.xxx.json
    - appsettings.Invalid.xxx.json
- 성능 테스트
- 전역 예외 처리

## CI/CD
- 빌드 자동화
  - 코드 커버리지
  - 레이어 다이어그램
  - 코드 품질
  - 단일 파일
- 배포 자동화
  - 윈도우 서비스
  - 도커 컴포즈
  - 설정?

## 관찰 가능성 시스템
| System     | Logs(Windows) | Logs(Linux) | Logs(Container) | Metrics(Windows) | Metrics(Linux) | Metrics(Container) | Traces |
| ---        | ---           | ---         | ---             | ---              | ---            | ---                | ---    |  
| Aspire     |               |             |                 |                  |                |                    |        |  
| Grafana    |               |             |                 |                  |                |                    |        |  
| OpenSearch |               |             |                 |                  |                |                    |        |  

## 관찰 가능성
- 로그
  - 파일
  - 파일 Json
  - gRPC OpenTelemetry
  - 로그 테스트
  - Fake 데이터 테스트  

## 전역 기본 타입
- Error
  - Error 코드
- Result
- ValidationResult

## Application 기본 타입
- AggregateRoot
- Domain Event
- IRespository
- UoW
- ORM/SQL
- DTO
- Domain Model vs. Tr... Script
- IValidator
- CQRS
  - ICommand
  - IQuery
- Pipeline
  - 예외
  - 로그
  - 유효성 검사
  - 캐시?

## Domain 기본 타입
