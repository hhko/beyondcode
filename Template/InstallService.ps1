<#
.SYNOPSIS
Windows 서비스 설치 및 복구 동작 설정 스크립트.

.DESCRIPTION
이 스크립트는 Windows 서비스를 설치하고, 복구 동작(3번 실패 시 1분 후 재시작)을 설정합니다.

.PARAMETER ServiceName
설치할 서비스의 이름.

.PARAMETER ExecutablePath
서비스 실행 파일의 전체 경로.

.PARAMETER StartType
서비스의 시작 유형 (기본값: Auto).

.EXAMPLE
.\InstallService.ps1 -ServiceName "MyService" -ExecutablePath "C:\Path\To\MyService.exe"
#>

param (
    [Parameter(Mandatory = $true)]
    [string]$ServiceName,

    [Parameter(Mandatory = $true)]
    [string]$ExecutablePath,

    [Parameter(Mandatory = $false)]
    [ValidateSet("auto", "manual", "disabled")]
    [string]$StartType = "auto"
)

function Install-Service {
    param (
        [string]$ServiceName,
        [string]$ExecutablePath,
        [string]$StartType
    )

    Write-Host "서비스를 설치 중입니다: $ServiceName" -ForegroundColor Green

    # 서비스 설치 명령
    $installCommand = "sc create `"$ServiceName`" binPath= `"$ExecutablePath`" start= $StartType"
    Invoke-Expression $installCommand

    if ($LASTEXITCODE -ne 0) {
        Write-Error "서비스 설치에 실패했습니다."
        exit 1
    }

    Write-Host "서비스 설치 완료." -ForegroundColor Green
}

function Configure-ServiceRecovery {
    param (
        [string]$ServiceName
    )

    Write-Host "서비스 복구 설정 중입니다: $ServiceName" -ForegroundColor Green

    # 서비스 복구 설정 명령
    $recoveryCommand = "sc failure `"$ServiceName`" reset= 3600 actions= restart/60000/restart/60000/restart/60000"
    Invoke-Expression $recoveryCommand

    if ($LASTEXITCODE -ne 0) {
        Write-Error "서비스 복구 설정에 실패했습니다."
        exit 1
    }

    Write-Host "서비스 복구 설정 완료." -ForegroundColor Green
}

try {
    Install-Service -ServiceName $ServiceName -ExecutablePath $ExecutablePath -StartType $StartType
    Configure-ServiceRecovery -ServiceName $ServiceName
    Write-Host "`n서비스 설치 및 복구 설정이 완료되었습니다." -ForegroundColor Cyan
} catch {
    Write-Error "오류가 발생했습니다: $_"
    exit 1
}