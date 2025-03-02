# TODO

```shell
# 1. .NET SDK
dotnet new globaljson --sdk-version 9.0.100 --roll-forward latestMinor --force
dotnet --version

# 2. NuGet 저장소
dotnet new nuget.config

# 3. NuGet 패키지 버전
# Directory.Packages.props
dotnet tool install -g upgrade-assistant
upgrade-assistant upgrade

# 4. 프로젝트 빌드 속성
#Directory.Packages.props
```

- [x] 도메인 | 구독, Subscription
- [x] 도메인 | 헬스장, Gym
- [x] 도메인 | 방, Room
- [x] 도메인 | 트레이너, Trainer
- [x] 도메인 | 참가자, Participant
- [x] 도메인 | 세션, Session
---
- [x] 목차 | Ch01. Flat
- [x] 목차 | Ch02. 폴더 구조화
- [x] 목차 | Ch03. 테스트
- [x] 목차 | Ch04. 전술 패턴
- [ ] 목차 | Ch05. 도메인 요구사항 추가 ::::: Ch05. Start
  - [x] Subscription
  - [x] Gym
  - [x] Room
  - [x] Trainer
  - [x] Participant
  - [x] Session
  - [x] Schedule
  - [x] TimeRange
  - [x] Addmin
- [x] 목차 | 도메인 이벤트
- [ ] 목차 | Application 레이어
  - [x] IQuery
  - [x] ToResponse <- IQuery
  - [x] FluentValidator
  - [x] MediatR 의존성 주입
  - [x] FluentValidator 의존성 주입
  - [ ] MediatR Pipeline: Validator
  - [ ] MediatR Pipeline: OpenTelemetry
  - [ ] MediatR Pipeline: 트랜잭션
  - [ ] MediatR Pipeline: ...?
  - [x] 단위 테스트: 의존성 주입
  - [x] 단위 테스트: Mediator 메시지 호출
  - [x] 단위 테스트: 결과
  - [ ] ICommand
  -----------
  - [x] GymManagement | Gym | Query, Get
  - [x] GymManagement | Gym | Query, List
  - [x] 논리 오류: if (!await _subscriptionsRepository.ExistsAsync())
    ```cs
    if (await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
    {
        return Error.NotFound("Subscription not found");
    }
    ```
---
- [x] Soltuion 템플릿
- [x] Query 아이템 템플릿
- [x] Command 아이템 템플릿
- [ ] AggregateRoot 아이템 템플릿
- [ ] Event 아이템 템플릿
- [ ] Integration Event 아이템 템플릿
- [ ] Service 프로젝트 템플릿
---
- [ ] 데이터 관리???
  ```cs
  await _adminsRepository.UpdateAsync(admin);     ??? 트랜잭션으로 개선할 수 없을까?
  ```
- [x] GymManagement | Admins
- [x] GymManagement | Gyms
- [x] GymManagement | Rooms
- [x] GymManagement | Subscriptions
- [x] UserManagement | Profiles
- [ ] UserManagement | Authentication
- [x] SessionReservation | Gyms
- [x] SessionReservation | Participants
- [x] SessionReservation | Reservations
- [ ] SessionReservation | Rooms
- [ ] SessionReservation | Sessions
- [ ] SessionReservation | Trainers

---
- [ ] event로 인해 트랜잭션 분리 문제는?
  예. 예약 취소(삭제)
    -> 참가자의
- [ ] 이벤트는 반환 타입에 IErrorOr을 적용할 수 없다.

---
- [ ] 문서 정리
- [ ] 이해
- [ ] IValidator 개선

---
- .editorconfig
  ```
  csharp_style_namespace_declarations = file_scoped:error   <- vs 새로 생성할 때 기본 값
                                                            <- error? 적용 안됨? EnforceCodeStyleInBuild
  dotnet_diagnostic.IDE0161.severity = error                <- vs 전용 빌드 에러?
  ```
- 도메인 이벤트로 데이터 Db에 저장?
- 도메인 이벤트에 객체 전달? 객체 Id가 아니리?

----
- [ ] 목차 | WebApi
- [ ] 목차 | EFCore(Sqlite)
---
- [x] 솔루션 | nuget.config
- [x] 솔루션 | Directory.Packages.props
- [ ] 솔루션 | Directory.Build.props
- [ ] 솔루션 | .editorconfig
- [ ] 솔루션 | Use primary constructor (IDE0290) 비활성화???
  ```
  csharp_style_prefer_primary_constructors

  [*.{cs,vb}]
  dotnet_diagnostic.IDE0290.severity = none
  ```
- [x] 솔루션 | public class -> public sealed class
- [ ] 솔루션 | ErrorOr -> CSharpFunctionalExtensions
- [ ] Adapter 프로젝트 | Serilog
- [ ] Adapter 프로젝트 | Serilog + ErrorOr 통합(Error 코드, 예외)
- [ ] Adapter 프로젝트 | OpenTelemtry
- [ ] Adapter 프로젝트 | Aspire Dashboard
- [ ] 도메인 프로젝트 | 성능 개선 Guid 패키지
- [x] 도메인 프로젝트 | file-scoped namespace
- [ ] 도메인 프로젝트 | Entity IValidator ???
- [ ] 도메인 프로젝트 | Error.Conflict -> DomainErrors
- [ ] 도메인 프로젝트 | 기본 Error 타입(정의된 Error 타입)
  ```
  Error.Validation();                                 -> DomainErrors.Validation(nameof( ... ));
  Error.Conflict();
  Error.Unexpected();
  Error.NotFound(description: "Booking not found");
  ```
- [x] 도메인 프로젝트 | Error 코드 -> nameof
- [x] 도메인 프로젝트 | **Error 단위는 Entity? 메서드 단위**
  ```
  DomainErrors{.Entity}.메서드Errors
  ```
- [x] 도메인 프로젝트 | = new(); -> = [];
- [ ] 도메인 프로젝트 | new() { time }; -> [ time ];
- [x] 도메인 프로젝트 | SubscriptionType -> Grade
- [x] 도메인 프로젝트 | 폴더 구성
  ```
  Enumerations
  Errors
  Events
  ValueObjects
  ```
- [ ] 도메인 프로젝트 | Throw?? 예외 발생 이유(중복 Id)
- [ ] 도메인 프로젝트 | 기본 생성자 겹침???
  ```cs
  private Schedule()
  {
  }

  public Schedule(    <-- 기본 값으로 "public Schedule()"이 된다
    Dictionary<DateOnly, List<TimeRange>>? calendar = null,
    Guid? id = null)
  ```
- [ ] 도메인 프로젝트 | Id 통일
  ```
  private readonly Guid _id;
    vs.
  public Guid Id { get; }
  ```
- [ ] 도메인 프로젝트 | Trainer _userId????
- [ ] 도메인 프로젝트 | Error에 실패 값 포함
- [ ] 도메인 프로젝트 | Error 클래스 메서드 단위??? vs. 도메인 클래스 단위???
- [ ] 도메인 프로젝트 | Room과 Trainer의 Schedule 인스턴스???
---
- [x] 테스트 프로젝트 | FluentAssertions -> Shouldly
- [x] 테스트 프로젝트 | sut
- [x] 테스트 프로젝트 | 범주화
- [x] 테스트 프로젝트 | public -> internal
- [x] 테스트 프로젝트 | 폴더 구성 Abstractions
- [x] 테스트 프로젝트 | Constants.Subscriptions -> Constants.Subscription
  ```
  Constants.Constants.{클래스}
  Constants.DomainConstants.{클래스}
  ```
- [x] 테스트 프로젝트 | xunit.runner.json
- [x] 테스트 프로젝트 | FluentAssertions -> Shouldly
- [x] 테스트 프로젝트 | Trait 단위 필터
- [x] 테스트 프로젝트 | Last -(성능 개선)-> [Count - 1], SonarQube 추천
- [x] 테스트 프로젝트 | SubscriptionTest 기반으로 GymTest Add 테스트 역시 동적 건수 기반으로 개선
- [x] 테스트 프로젝트 | reportgenerator
- [x] 테스트 프로젝트 | reportgenerator 대상 선정 필터
- [ ] 테스트 프로젝트 | GitHub Actions 로컬
- [ ] 테스트 프로젝트 | GitHub Actions 코드 커버리지
- [x] 테스트 프로젝트 | Room 규칙 테스트
  - 방은 두 개 이상의 겹치는 세션을 가질 수 없다.
  - A room cannot have two or more overlapping sessions
- Application Usecase 성공일 때 값이 N개일 때
  ```cs
  return (user, _jwtTokenGenerator.GenerateToken(user))     // <---???
    .ToResponse()
  ```