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
- [ ] 호스트 | IOption
- [ ] 호스트 | IOption Validation
- [ ] 호스트 | Scheduler
- [ ] 호스트 | Message
---
- [ ] Presentation | Host에서 WebApi 분리
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
- [ ] 테스트 | BDD
- [ ] 테스트 | 성능
- [ ] 테스트 | 컨테이너


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
- var vs. 타입
  - "타입" 코드 지저분??? 개선 방법?


### Presentation | Host에서 WebApi 분리
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
```
```xml
<ItemGroup>
  <FrameworkReference Include="Microsoft.AspNetCore.App" />
</ItemGroup>
```

```cs
services
  .AddControllers()
  .AddApplicationPart(AssemblyReference.Assembly);
```

### 레포지토리 인터페이스 정의
1. 입력 -DTO-> 도메인 모델 -DTO-> 출력(영속성 모델)
1. 입력 -DTO-> 도메인 모델 -----> 출력(도메인 모델)
1. 입력 -DTO-> 도메인 모델 -DTO-> 출력(영속성 모델: Command)
1. 입력 -DTO-> 도메인 모델 -----> 출력(영속성 모델: Query)

> - 도메인 격리: DTO 사용
> - ?
>
> - 애플리케이션 서비스 레이어는 도메인 모델과 외부 시스템 간의 상호 작용을 조율하는 역할
> - 도메인 레이어는 순수한 도메인 로직만 포함
>
> - DTO은 기술적인 요구사항에 따라 변경될 수 있습니다.
> - 애그리거트 루트

- 레포지토리는 애그리거트의 영속성을 관리하며, 도메인 모델의 일부로 간주할 때(일반적으로는 도메인 타입을 직접 사용하는 것이 권장)
  - 도메인 타입 사용 시:
    - 레포지토리 인터페이스는 도메인 레이어에 정의합니다.
    - 이는 레포지토리가 도메인 모델의 일부로 간주되고, 도메인 로직과 밀접하게 관련되기 때문입니다.
    - 도메인 레이어는 순수한 도메인 로직만 포함해야 하므로, DTO와 같은 기술적인 세부 사항은 포함하지 않습니다.
- 레포지토리는 영속성 역시 기술로 간주할 때
  - DTO 타입 사용 시:
    - 레포지토리 인터페이스는 애플리케이션 서비스 레이어에 정의합니다.
    - DTO는 데이터 전송을 위한 객체이며, 기술적인 세부 사항을 포함합니다.
    - 따라서 DTO는 도메인 레이어가 아닌 애플리케이션 서비스 레이어에 정의하는 것이 적절합니다.
    - 애플리케이션 서비스 레이어는 도메인 모델과 외부 시스템 간의 상호 작용을 조율하는 역할을 수행하므로, DTO를 사용하여 데이터를 주고받는 것이 자연스럽습니다.



```
System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[GymManagement.Application.Usecases.Participants.Commands.CancelReservation.CancelReservationCommand,ErrorOr.IErrorOr]
 Lifetime: Transient ImplementationType: GymManagement.Application.Usecases.Participants.Commands.CancelReservation.CancelReservationCommandUsecase':
  Unable to resolve service for type 'GymManagement.Domain.AggregateRoots.Sessions.IDateTimeProvider' while attempting to activate 'GymManagement.Application.Usecases.Participants.Commands.CancelReservation.CancelReservationCommandUsecase'.) (Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[GymManagement.Application.Usecases.Authentication.Queries.Login.LoginQuery,ErrorOr.IErrorOr`1[GymManagement.Application.Usecases.Authentication.Queries.Login.LoginResponse]] Lifetime: Transient ImplementationType: GymManagement.Application.Usecases.Authentication.Queries.Login.LoginQueryUsecase': Unable to resolve service for type 'GymManagement.Application.Abstractions.Tokens.IJwtTokenGenerator' while attempting to activate 'GymManagement.Application.Usecases.Authentication.Queries.Login.LoginQueryUsecase'.) (Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[GymManagement.Application.Usecases.Authentication.Commands.Register.RegisterCommand,ErrorOr.IErrorOr`1[GymManagement.Application.Usecases.Authentication.Commands.Register.RegisterResponse]] Lifetime: Transient ImplementationType: GymManagement.Application.Usecases.Authentication.Commands.Register.RegisterCommandUsecase': Unable to resolve service for type 'GymManagement.Application.Abstractions.Tokens.IJwtTokenGenerator' while attempting to activate 'GymManagement.Application.Usecases.Authentication.Commands.Register.RegisterCommandUsecase'.)'
```