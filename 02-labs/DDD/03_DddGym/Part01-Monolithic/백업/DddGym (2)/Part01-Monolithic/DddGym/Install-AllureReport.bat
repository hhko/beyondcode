@echo off
setlocal

:: 설정: Allure 버전과 설치 디렉터리 설정 (필요시 수정)
set "ALLURE_VERSION=allure-2.34.0"
set "INSTALL_DIR=C:\Workspace\Tools"

:: 현재 디렉터리 기준 PowerShell 스크립트 실행
powershell.exe -ExecutionPolicy Bypass -NoProfile -File "%~dp0Install-AllureReport.ps1" -AllureVersion "%ALLURE_VERSION%" -InstallDir "%INSTALL_DIR%"

endlocal
