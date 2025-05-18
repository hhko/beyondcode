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
