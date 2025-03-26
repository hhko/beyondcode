#
# 도구
#  project-tree-generator: https://woochanleee.github.io/project-tree-generator/
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