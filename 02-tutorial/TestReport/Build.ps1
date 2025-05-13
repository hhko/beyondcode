# -OpenAllure:$false
# -OpenCoverage:$false

param (
  [bool]$OpenCoverage = $true,
  [bool]$OpenAllure = $true
)

# ---------------------------------------------------
# 폴더 구성
# ---------------------------------------------------
# ./.coverage-results     : 코드 커버리지 결과
# ./.coverage-report      : 코드 커버리지 웹사이트 보고서
#
# ./.allure-results   : allure 테스트 결과
# ./.allure-report    : allure 테스트 웹사이트 보고서

# 오류 발생 시 즉시 중단
# https://learn.microsoft.com/ko-kr/powershell/module/microsoft.powershell.core/about/about_preference_variables?view=powershell-7.5#erroractionpreference
$ErrorActionPreference = "Stop"

function Run-Step {
  param (
    [string]$StepName,
    [ScriptBlock]$Action
  )

  Write-Host "==== $StepName ====" -ForegroundColor Cyan
  & $Action
  Write-Host ""
}

# 솔루션 복원 및 빌드
Run-Step "🔧 Restore and Build Solution" {
  dotnet restore
  dotnet build -c Release
}

# Allure history 복사
Run-Step "📁 Copy Allure History (if exists)" {
  $previousHistory = ".\.allure-report\history"
  $newResultDir = ".\.allure-results\"

  if (Test-Path $previousHistory) {
    Write-Host "➡ 복사 중: $previousHistory → $newResultDir"
    Copy-Item -Path $previousHistory -Destination $newResultDir -Recurse -Force
  }
  else {
    Write-Host "⚠ 이전 history 디렉토리가 존재하지 않습니다. 생략합니다."
  }
}

# 테스트 실행
Run-Step "🧪 Run Tests with Code Coverage" {
  dotnet test `
    --configuration Release `
    --no-build `
    --verbosity q `
    --settings .runsettings
}

# 코드 커버리지 리포트 생성 (reportgenerator 사용)
Run-Step "📊 Generate Code Coverage Report" {
  reportgenerator `
    -reports:"./**/TestResults/*/*.cobertura.xml" `
    -targetdir:"./.coverage-report/" `
    -reporttypes:"Html;TextSummary;MarkdownSummaryGithub;MarkdownAssembliesSummary" `
    -verbosity:Info
}

# Allure 리포트 생성
Run-Step "📈 Generate Allure Report" {
  allure generate .\.allure-results\ --clean -o .\.allure-report\
}

# 옵션: 코드 커버리지 리포트 열기
if ($OpenCoverage) {
  Run-Step "🌐 Open Code Coverage Report in Browser" {
    $coveragePath = Resolve-Path ".\.coverage-report\index.html"

    # 커버리지 리포트 경로 출력
    Write-Host "커버리지 리포트 경로: $coveragePath" -ForegroundColor Green

    Start-Process $coveragePath
  }
}

# 옵션: Allure 리포트 열기
if ($OpenAllure) {
  Run-Step "🌐 Open Allure Report in Browser" {
    $reportPath = Resolve-Path ".\.allure-report\"

    # 경로 출력
    Write-Host "📂 Allure Report Path: $reportPath" -ForegroundColor Green

    # Allure 리포트를 브라우저에서 열기
    allure open $reportPath
  }
}
