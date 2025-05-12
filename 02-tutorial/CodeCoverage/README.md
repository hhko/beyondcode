```shell
# 솔루션 빌드
dotnet restore
dotnet build -c Release

# 솔루션 테스트
#   - coverlet.collector 패키지: --collect "XPlat Code Coverage"
dotnet test `
  --configuration Release `
  --no-build `
  --verbosity q `
  --settings .runsettings
  #--collect "XPlat Code Coverage"

# 솔루션 테스트 결과
# CodeCoverage\Numbers.Tests.Unit\
#       TestResults\
#           8edcee01-7c46-4d50-b97a-5d4ec1eaa887\   <- GUID
#               coverage.cobertura.xml              <- Code Coverage:  coverlet.collector 패키지지
#               coverage.opencover.xml

# 코드 커버리지 도구
dotnet tool install -g dotnet-reportgenerator-globaltool

# 코드 커버리지 생성성
reportgenerator `
  -reports:"./**/TestResults/*/*.cobertura.xml" `
  -targetdir:"./.build/CodeCoverage/" `
  -reporttypes:"Html;TextSummary;MarkdownSummaryGithub;MarkdownAssembliesSummary" `
  -verbosity:Info
  #-assemblyfilters:-*.Tests.* `
  #-filters:"-*.g.cs"

# 코드 커비리지 결과
./.build/CodeCoverage/
```

```shell
dotnet new gitignore
dotnet new nuget.config
```

- [Code Coverage Reports for .NET Projects](https://knowyourtoolset.com/2024/01/coverage-reports/)
- [Coverlet integration with VSTest (a.k.a. Visual Studio Test Platform)](https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/VSTestIntegration.md#advanced-options-supported-via-runsettings)

## TODO
- [x] CLI 코드 커버리지
- [X] .runsettings 코드 커버리지
  - [x] 로그
  - [x] 코드 커버리지 형식
  - [x] 코드 커버리지 제외: 테스트 프로젝트, 파일, 속성
  - [ ] 타임 아웃
  - [ ] TargetPlatform 지정
  - [ ] TestCaseFilter 제외
- [ ] 로컬 빌드 자동화: Build.ps1
- [ ] 원격 빌드 자동화: GitHub Actions
- [ ] Allure Report 통합
---
- [ ] Directory.Packages.props
- [ ] Directory.Build.props
---
- [ ] 코드 커버리지 문서