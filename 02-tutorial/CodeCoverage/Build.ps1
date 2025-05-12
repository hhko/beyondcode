# 솔루션 빌드
# dotnet restore
# dotnet build -c Release

# 솔루션 테스트
#   - coverlet.collector 패키지: --collect "XPlat Code Coverage"
dotnet test `
  --configuration Release `
  --no-build `
  --verbosity q `
  --settings .runsettings
  #--collect "XPlat Code Coverage"

# 코드 커버리지 생성성
reportgenerator `
  -reports:"./**/TestResults/*/*.cobertura.xml" `
  -targetdir:"./.build/CodeCoverage/" `
  -reporttypes:"Html;TextSummary;MarkdownSummaryGithub;MarkdownAssembliesSummary" `
  -verbosity:Info
#   -assemblyfilters:-*.Tests.* `
#   -filters:"-*.g.cs"