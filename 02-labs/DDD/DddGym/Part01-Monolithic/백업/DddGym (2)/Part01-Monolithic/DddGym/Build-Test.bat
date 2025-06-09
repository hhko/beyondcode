@echo off
setlocal

:: 설정
set "ROOT_PATH=./"
set "RUNSETTINGS_PATH=.\.runsettings-allurereport"

:: 현재 디렉터리 기준 PowerShell 스크립트 실행
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Build-Test.ps1" -RootPath "%ROOT_PATH%" -RunSettingsPath "%RUNSETTINGS_PATH%"

endlocal
