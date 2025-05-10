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
