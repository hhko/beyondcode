# HKLM:\SYSTEM\CurrentControlSet\Control\FileSystem
# 1
#
# $regPath = 'HKLM:\SYSTEM\CurrentControlSet\Control\FileSystem'
# $regName = 'LongPathsEnabled'
# Set-ItemProperty -Path $regPath -Name $regName -Value 1

$regPath = 'HKLM:\SYSTEM\CurrentControlSet\Control\FileSystem'
$regName = 'LongPathsEnabled'

Write-Host "`n=== LongPathsEnabled 설정 확인 및 변경 ==="

# 현재 값 읽기
$currentValue = Get-ItemProperty -Path $regPath -Name $regName -ErrorAction SilentlyContinue | Select-Object -ExpandProperty $regName

if ($null -eq $currentValue) {
    Write-Host "현재 레지스트리 키가 존재하지 않음. 기본값은 '0'으로 간주됩니다." -ForegroundColor Yellow
    $currentValue = 0
} else {
    Write-Host "변경 전 값: $currentValue"
}

# 필요한 경우 변경
if ($currentValue -ne 1) {
    Write-Host "LongPathsEnabled 값을 1로 설정 중..." -ForegroundColor Cyan
    Set-ItemProperty -Path $regPath -Name $regName -Value 1

    # 변경 후 값 확인
    $newValue = Get-ItemProperty -Path $regPath -Name $regName | Select-Object -ExpandProperty $regName
    Write-Host "변경 후 값: $newValue" -ForegroundColor Green
} else {
    Write-Host "이미 LongPathsEnabled 값이 1로 설정되어 있습니다." -ForegroundColor Green
}

Write-Host "`n※ 변경 사항을 적용하려면 재부팅이 필요할 수 있습니다." -ForegroundColor Yellow
