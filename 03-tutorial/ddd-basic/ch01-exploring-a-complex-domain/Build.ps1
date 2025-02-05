$curDir         = Split-Path -parent $MyInvocation.MyCommand.Definition
$coverageDir    = $curDir | Join-Path -ChildPath ".build" | Join-Path -ChildPath "Coverage"

#
# Ready
#
Write-Host "0. Ready" -ForegroundColor Yellow
if (Test-Path $coverageDir) {
    Remove-Item $coverageDir -Recurse -Force
}
New-Item $coverageDir -ItemType Directory -Force | Out-Null

#
# Restore
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
# Build
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
# Test
#
Write-Host "3. Test" -ForegroundColor Yellow
dotnet test `
  --configuration Release `
  --no-build `
  --verbosity q `
  --collect "XPlat Code Coverage"
  #--filter UnitTest=Domain         # 특정 테스트만 실행

#
# CodeCoverage: Tests 프로젝트 제외
#
Write-Host "4. CodeCoverage" -ForegroundColor Yellow
reportgenerator `
  -reports:"$curDir/**/TestResults/*/*.cobertura.xml" `
  -targetdir:$coverageDir `
  -reporttypes:"Html;TextSummary;MarkdownSummaryGithub;MarkdownAssembliesSummary" `
  -verbosity:Info `
  -assemblyfilters:-*.Tests.*
