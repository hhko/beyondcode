# 도메인 탐색

- [Getting Started: Domain-Driven Design](https://dometrain.com/course/getting-started-domain-driven-design-ddd/?ref=dometrain-github&promo=getting-started-domain-driven-design)
  ```
  Exploring a Complex Domain
    The domain we’ll be exploring
    The domain’s Ubiquitous Language
    Ubiquitous Language as a tool for detecting Domain Objects
    The domain’s invariants
    Enforcing invariants
    Creating the domain objects
    Invariants as Unit Tests
    Enforcing Invariants that rely on dependencies
    The Result Pattern
    Section Recap
  ```
- [Section 3. Exploring a Complex Domain 소스](https://github.com/Dometrain/getting-started-domain-driven-design/tree/main/section-03/end)

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
- [x] public class -> public sealed class
- [ ] Constants. ...?
- [ ] IValidator ???
- [ ] Error 코드 -> nameof
- [ ] Serilog
- [ ] Serilog + ErrorOr 통합(Error 코드, 예외)
- [x] = new(); -> = [];
- [ ] Use primary constructor (IDE0290) 비활성화???
  ```
  csharp_style_prefer_primary_constructors

  [*.{cs,vb}]
  dotnet_diagnostic.IDE0290.severity = none
  ```
- [x] SubscriptionType -> Grade
- [x] 도메인 프로젝트 폴더 구성
  ```
  Enumerations
  Errors
  Events
  ValueObjects
  ```
- [x] 테스트 프로젝트 sut
- [x] 테스트 프로젝트 범주화
- [x] 테스트 프로젝트 public -> internal
- [x] 테스트 프로젝트 폴더 구성 Abstractions
- [x] 테스트 프로젝트 Constants.Subscriptions -> Constants.Subscription
- [x] 테스트 프로젝트 xunit.runner.json
- [x] 테스트 프로젝트 FluentAssertions -> Shouldly
- [x] 테스트 프로젝트 Trait 단위 필터
- [ ] 테스트 프로젝트 Last?
- [ ] reportgenerator
- [ ] GitHub Actions 로컬
- [ ] GitHub Actions 코드 커버리지
