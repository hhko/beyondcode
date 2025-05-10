@echo off
setlocal

REM === 설정 ===
set "SCRIPT=Build-Test.ps1"
set "ROOT_PATH=./"
set "RUNSETTINGS_PATH=.\.runsettings-allurereport"

REM === PowerShell 실행 ===
powershell -NoProfile -ExecutionPolicy Bypass -File "%SCRIPT%" -RootPath "%ROOT_PATH%" -RunSettingsPath "%RUNSETTINGS_PATH%"

endlocal
