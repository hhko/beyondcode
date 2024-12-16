## 레이어
- [x] 솔루션 구성
  - [x] 제품
    - [x] Adapters.Infrastructure
    - [x] Adpaters.Persistence
    - [x] Adapters.Presnetation
    - [x] Application
    - [x] Domain
  - [x] 어셈블리
    - [x] AssemblyReference 구현
  - [x] Asserts
    - [x] Frameworks
    - [x] Libraries
    - [x] Domains
- 솔루션 빌드
  - [x] Directory.Build.props: 전체, 테스트
    - [x] .NET
    - [x] 버전
    - [x] 메타
    - [ ] 단일 파일
  - [x] Directory.Packages.props
    - [x] 생성 CLI
    - [ ] 구분
  - [x] global.json
  - [x] nuget.config
  - [x] .editorconfig
    - [x] 네임스페이스: File scope
    - [ ] await 없는 async 메서드
  - [x] .gitignore
  - [ ] .gitattirute
- [ ] 아키텍처 단위 테스트
  - [x] 폴더 구성
    - [x] Abstractions
    - [x] ArchitectureTests
  - [x] 레이어 의존성 테스트
  - [x] 어셈블리 AssemblyReference 테스트

## 배포 구성
- appsettings 배포 N개
- 도커 컴포즈 N개
- 컨테이너 Health Check
- 컨테이너 시작 순서
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
  - git 버전 통합
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
- Known Pipeline
  - 예외
  - 로그
  - 유효성 검사
  - 캐시?
- Unknown Pipeline
  - IAdapter

## Domain 기본 타입
- Entity
- Value Object
- Enum
- Domain Service
- Factory

## Adapter 기본 기능
- Retry
- Circuit Breaker