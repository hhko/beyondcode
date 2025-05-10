# 테스트 보고서(Allure Report)

## 개요
- Allure Report는 테스트 실행 결과를 시각적으로 보기 쉽게 정리해주는 리포트 도구입니다.
- .NET 프로젝트에서 xUnit, Reqnroll과 함께 사용하면 테스트 결과를 웹 기반 리포트로 확인할 수 있어 매우 유용합니다.

![](./test-allurereport.png)

<br/>

## 지침
- 테스트 상황을 웹 기반 리포트로 공유합니다.
- 테스트 프로젝트에서는 Allure 관련 NuGet 패키지를 추가하고, `allureConfig.json` 파일을 설정하여 Allure 결과 파일(.allure-results)을 생성합니다.

<br/>

## Allure Report v2 설치
- https://github.com/allure-framework/allure2/releases
  - `allure-*.zip` 최신 버전 다운로드

```bat
@echo off
setlocal

:: Allure 버전과 설치 디렉터리 설정 (필요시 수정)
set "ALLURE_VERSION=allure-2.34.0"
set "INSTALL_DIR=C:\Workspace\Tools"

:: 현재 디렉터리 기준 PowerShell 스크립트 실행
powershell.exe -ExecutionPolicy Bypass -NoProfile -File "%~dp0install-AllureReport.ps1" -AllureVersion "%ALLURE_VERSION%" -InstallDir "%INSTALL_DIR%"

endlocal
```
- `ALLURE_VERSION`에 설치할 allure report 버전을 명시합니다.
- `INSTALL_DIR`은 allure report을 설치할 경로입니다.

```powershell
param (
    [Parameter(Mandatory = $true)]
    [string]$AllureVersion,  # 예: allure-2.34.0

    [Parameter(Mandatory = $true)]
    [string]$InstallDir      # 예: C:\Workspace\Tools
)

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

# 경로 및 URL 설정
$VersionNumber = $AllureVersion.TrimStart("allure-")
$ZipUrl = "https://github.com/allure-framework/allure2/releases/download/$VersionNumber/$AllureVersion.zip"
$ZipPath = "$env:TEMP\$AllureVersion.zip"
$ExtractPath = Join-Path -Path $InstallDir -ChildPath $AllureVersion
$AllureBinPath = Join-Path -Path $ExtractPath -ChildPath "bin"

try {
    Run-Step "Downloading Allure CLI $AllureVersion" {
        Write-Host "Downloading from $ZipUrl..."
        Invoke-WebRequest -Uri $ZipUrl -OutFile $ZipPath -UseBasicParsing
    }

    Run-Step "Extracting archive to $ExtractPath" {
        Expand-Archive -Path $ZipPath -DestinationPath $InstallDir -Force
    }

    Run-Step "Updating PATH (User scope)" {
        $ExistingPath = [Environment]::GetEnvironmentVariable("PATH", "User")
        $FilteredPath = ($ExistingPath -split ";") | Where-Object { $_ -and $_ -notlike "*\allure-*\bin" }
        $NewPath = ($FilteredPath + $AllureBinPath) -join ";"
        [Environment]::SetEnvironmentVariable("PATH", $NewPath, "User")
    }

    Run-Step "Installation complete" {
        Write-Host "Allure CLI $AllureVersion installed successfully at $ExtractPath"
    }
}
catch {
    Write-Error "An error occurred during installation: $_"
    exit 1
}
```

```shell
# 버전 확인
allure --version
  2.34.0
```

<br/>

## 솔루션

### .runsettings-allurereport
```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <RunConfiguration>
    <ReporterSwitch>allure</ReporterSwitch>
  </RunConfiguration>
</RunSettings>
```

- `.runsettings` 파일을 이용하여 reporter을 `xunit.runner.visualstudio`에서 `allure`으로 변경합니다.
  - .runsettings-allurereport 대신 `dotnet test -- RunConfiguration.ReporterSwitch=allure` 명령으로 대체할 수 있습니다.
  - https://xunit.net/docs/runsettings#ReporterSwitch

### .gitignore
```.gitignore
.allure-results/
.allure-report/
```

- Allure에서 생성하는 결과 파일은 형상관리 대상에서 제외 시킵니다.

<br/>

## 테스트 프로젝트
### Allure.Xunt 패키지
  - Allure.Xunit: `2.12.1`
  - **System.Text.Json: `9.0.4`** (Allure.Xunit 2.12.1 버전에서 System.Text.Json 8.0.1 버전을 사용하고 있기 때문에 업그레이드합니다)
  - xunit: `2.9.3
  - **xunit.runner.visualstudio: `2.8.2`**
    - **.NET 9.0과 Allure.Xunit 2.12.1 버전에서는 xunit.runner.visualstudio 3.x.x 버전일 때는 정상동작하지 않습니다.**

### Allure Results 생성
```json
{
  "$schema": "https://raw.githubusercontent.com/allure-framework/allure-csharp/2.10.0/Allure.XUnit/Schemas/allureConfig.hema.json",
  "allure": {
    "directory": "../../../../../../../.allure-results"
  }
}
 ```
```xml
<ItemGroup>
  <Content Include="allureConfig.json" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```
- `allureConfig.json`을 이용하여 Allure Report 생성을 위한 데이터 파일을 생성 경로 `.allure-results` 폴더의 상대 경로를 지정합니다.

### Allure Report 생성

```shell
# Allure Report 생성
allure generate .\.allure-results\ --clean -o .\.allure-report

# Allure Report 웹사이트 열기
allure open .\.allure-report
```

<br/>

## 테스트 스크립트
```bat
@echo off
setlocal

REM === 설정 ===
set "SCRIPT=Build-Test.ps1"
set "ROOT_PATH=./"
set "RUNSETTINGS_PATH=.\.runsettings-allurereport"

REM === PowerShell 실행 ===
powershell -NoProfile -ExecutionPolicy Bypass -File "%SCRIPT%" -RootPath "%ROOT_PATH%" -RunSettingsPath "%RUNSETTINGS_PATH%"

endlocal
```

```poershell
param (
    [Parameter(Mandatory = $true)]
    [string]$RootPath,  # 예: "./"

    [Parameter(Mandatory = $true)]
    [string]$RunSettingsPath  # 예: "./.runsettings-allurereport"
)

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

# Allure 관련 패키지 이름 키워드
$allurePackageKeywords = @(
    "Allure.Commons",
    "Allure.Xunit",
    "Allure.Reqnroll"
)

# 테스트 프로젝트만 필터링 (<IsTestProject>true</IsTestProject> 포함된 .csproj만)
$testProjects = Get-ChildItem -Path $RootPath -Recurse -Filter *.csproj | Where-Object {
    $content = Get-Content $_.FullName -Raw
    $content -match '<IsTestProject>\s*true\s*</IsTestProject>'
}

Run-Step "Deleting existing .allure-results folder (if exists)" {
    $resultsPath = ".\.allure-results"
    if (Test-Path $resultsPath) {
        Remove-Item $resultsPath -Recurse -Force
        Write-Host "Deleted $resultsPath"
    }
    else {
        Write-Host "$resultsPath does not exist. Skipping delete."
    }
}

$allureUsed = $false  # 전체 실행에서 Allure 적용 여부 추적

Run-Step "Cleaning the solution" { dotnet clean }

# WarningLevel
# https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings#warninglevel
#
# 예.
#   <WarningLevel>3</WarningLevel>
#   --property WarningLevel=3
# 0: Turns off emission of all warning messages

Run-Step "Restoring the solution" {
    dotnet restore --verbosity q --property WarningLevel=0

    if ($LASTEXITCODE -ne 0) {
        exit 1
    }
}

Run-Step "Building the solution" {
    dotnet build --no-restore --verbosity q --property WarningLevel=0

    if ($LASTEXITCODE -ne 0) {
        exit 1
    }
}

foreach ($proj in $testProjects) {
    $projPath = $proj.FullName
    $projName = $proj.Name

    # Allure 패키지 포함 여부 확인
    $csprojContent = Get-Content $projPath -Raw
    $hasAllure = $false

    foreach ($keyword in $allurePackageKeywords) {
        if ($csprojContent -match "<PackageReference[^>]*$keyword") {
            $hasAllure = $true
            $allureUsed = $true
            break
        }
    }

    # dotnet test 실행
    # dotnet test는 `--property WarningLevel=3`을 제공하지 않습니다
    if ($hasAllure) {
        Run-Step "Testing with Allure Report: $projName" {
            dotnet test "$projPath" --no-restore --no-build --logger "console;verbosity=normal" `
                                    --settings "$RunSettingsPath"
        }
    } else {
        Run-Step "Testing: $projName" {
            dotnet test "$projPath" --no-restore --no-build --logger "console;verbosity=normal"
        }
    }
}

if ($allureUsed) {
    Run-Step "Generating Allure Report" {
        allure generate .\.allure-results\ --clean -o .\.allure-report
    }

    Run-Step "Opening Allure Report" {
        allure open .\.allure-report
    }
} else {
    Write-Host "No test project used Allure packages. Skipping report generation."
}
```
