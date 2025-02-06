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
```

## 패키지
- [SmartEnum](https://github.com/ardalis/SmartEnum)


## .NET SDK 버전
```
x.y.znn
```
- `x`: major
- `y`: minor
- `z`: feature, 0 ~ 9
- `n`: patch, 0 ~ 99


## Enhancement
- [x] nuget.config
- [x] Directory.Packages.props
- [ ] Directory.Build.props
- [ ] .editorconfig
- [ ] 성능 개선 Guid 패키지
- [x] file-scoped namespace
- [ ] Serilog
- [ ] Serilog + ErrorOr 통합(Error 코드, 예외)
- [ ] OpenTelemtry
- [ ] Aspire Dashboard
- [x] public class -> public sealed class
- [ ] Use primary constructor (IDE0290) 비활성화???
  ```
  csharp_style_prefer_primary_constructors

  [*.{cs,vb}]
  dotnet_diagnostic.IDE0290.severity = none
  ```
- [ ] 도메인 프로젝트 Entity IValidator ???
- [ ] 도메인 프로젝트 Error.Conflict -> DomainErrors
- [x] 도메인 프로젝트 Error 코드 -> nameof
- [x] 도메인 프로젝트 **Error 단위는 Entity? 메서드 단위**
  ```
  DomainErrors{.Entity}.메서드Errors
  ```
- [x] 도메인 프로젝트 = new(); -> = [];
- [x] 도메인 프로젝트 SubscriptionType -> Grade
- [x] 도메인 프로젝트 폴더 구성
  ```
  Enumerations
  Errors
  Events
  ValueObjects
  ```
- [ ] Throw
- [ ] 기본 제공 Error 타입?
  ```
  Error.Validation();                                 -> DomainErrors.Validation(nameof( ... ));
  Error.Conflict();
  Error.Unexpected();
  Error.NotFound(description: "Booking not found");
  ```
- [ ] new() { time }; -> [ time ];
---

- [x] 테스트 프로젝트 sut
- [x] 테스트 프로젝트 범주화
- [x] 테스트 프로젝트 public -> internal
- [x] 테스트 프로젝트 폴더 구성 Abstractions
- [x] 테스트 프로젝트 Constants.Subscriptions -> Constants.Subscription
  ```
  Constants.Constants.{클래스}
  Constants.DomainConstants.{클래스}
  ```
- [x] 테스트 프로젝트 xunit.runner.json
- [x] 테스트 프로젝트 FluentAssertions -> Shouldly
- [x] 테스트 프로젝트 Trait 단위 필터
- [x] 테스트 프로젝트 Last -(성능 개선)-> [Count - 1], SonarQube 추천
- [x] 테스트 프로젝트 SubscriptionTest 기반으로 GymTest Add 테스트 역시 동적 건수 기반으로 개선
- [x] reportgenerator
- [x] reportgenerator 대상 선정 필터
- [ ] GitHub Actions 로컬
- [ ] GitHub Actions 코드 커버리지
