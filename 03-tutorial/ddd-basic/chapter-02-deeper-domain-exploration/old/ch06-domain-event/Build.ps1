#
# 폴더 구성
#   ├─ .build                     // 빌드 결과
#   │   └─ CodeCoverage           // 코드 커버리지
#   │       ├─ ...
#   │       └─ Summary.txt        // 코드 커버리지 요약
#   │
#   └─ Build.ps1                  // 로컬 빌드 스크립트
#
# 도구
#  project-tree-generator: https://woochanleee.github.io/project-tree-generator/
#  dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.4.4
#

$curDir         = Split-Path -parent $MyInvocation.MyCommand.Definition
$coverageDir    = $curDir | Join-Path -ChildPath ".build" | Join-Path -ChildPath "CodeCoverage"

#
# Ready
#
Write-Host "0. Ready" -ForegroundColor Yellow
if (Test-Path $coverageDir) {
    Remove-Item $coverageDir -Recurse -Force
}
New-Item $coverageDir -ItemType Directory -Force | Out-Null

#
# 1. Restore
#
Write-Host "1. Restore" -ForegroundColor Yellow
dotnet restore `
  --verbosity q `
  --property WarningLevel=0

if ($LASTEXITCODE -ne 0) {
    Write-Host "Restore failed" -ForegroundColor Red
    exit 1
}

#
# 2. Build
#
Write-Host "2. Build" -ForegroundColor Yellow
dotnet build `
  --no-restore `
  --configuration Release `
  --verbosity q `
  --property WarningLevel=0

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed" -ForegroundColor Red
    exit 1
}

#
# 3. Test
#
Write-Host "3. Test" -ForegroundColor Yellow
dotnet test `
  --configuration Release `
  --no-build `
  --verbosity q `
  --collect "XPlat Code Coverage"
  #--filter UnitTest=Domain         # 특정 테스트만 실행

#
# 4. Code Coverage: Tests 프로젝트 제외
#
Write-Host "4. Code Coverage" -ForegroundColor Yellow
reportgenerator `
  -reports:"$curDir/**/TestResults/*/*.cobertura.xml" `
  -targetdir:$coverageDir `
  -reporttypes:"Html;TextSummary;MarkdownSummaryGithub;MarkdownAssembliesSummary" `
  -verbosity:Info `
  -assemblyfilters:-*.Tests.*

#
# 5. Code Coverage Report
#
Write-Host "5. Code Coverage Report" -ForegroundColor Yellow
Get-Content (Join-Path $coverageDir "Summary.txt") | Select-Object -First 19
