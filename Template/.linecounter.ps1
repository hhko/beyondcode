param (
    [string]$targetDir = (Split-Path -parent $MyInvocation.MyCommand.Definition)
)

# 초기화: 대상 경로, 확장자 목록, 제외할 폴더 목록 설정
$fileExtensions     = @("*.py", "*.cs", "*.js", "*.hpp", "*.cpp")
$excludeFolderNames = @(".ci", "obj", "bin")

# 전체 라인 수와 파일 개수 변수 초기화
$totalLineCount = 0
$totalFileCount = 0

# 확장자별 라인 수와 파일 개수를 위한 해시 테이블
$extensionStats = @{}

# 폴더에서 제외할 조건 확인 함수
function ShouldExcludeFolder($filePath, $excludeFolderNames) {
    foreach ($excludeFolderName in $excludeFolderNames) {
        if ($filePath -like "*\$excludeFolderName\*") {
            return $true
        }
    }
    return $false
}

# 파일의 라인 수를 계산하는 함수
function Get-LineCount($filePath) {
    try {
        return (Get-Content $filePath -ErrorAction Stop).Count
    } catch {
        Write-Warning "Failed to read file: $filePath"
        return 0  # 읽기 오류 시 0 반환
    }
}

# 각 확장자별로 파일을 처리
foreach ($ext in $fileExtensions) {
    # 확장자별 초기화 (라인 수, 파일 개수)
    $extensionStats[$ext] = @{ "LineCount" = 0; "FileCount" = 0 }

    # 해당 확장자 파일을 검색
    $files = Get-ChildItem -Path $targetDir -Filter $ext -Recurse

    foreach ($file in $files) {
        # 파일 경로에 제외할 폴더가 포함되었는지 확인
        if (ShouldExcludeFolder $file.FullName $excludeFolderNames) {
            continue
        }

        # 파일 라인 수 계산
        $lineCount = Get-LineCount $file.FullName

        if ($lineCount -gt 0) {
            # 전체 및 확장자별 통계 업데이트
            $totalLineCount += $lineCount
            $totalFileCount++
            $extensionStats[$ext]["LineCount"] += $lineCount
            $extensionStats[$ext]["FileCount"]++

            # 파일 이름과 라인 수 출력
            Write-Host "File: $($file.FullName), Line Count: $lineCount"
        }
    }
}

# # 확장자별 결과를 한 번에 출력
# foreach ($ext in $fileExtensions) {
#     $extStats = $extensionStats[$ext]
#     Write-Host "$ext - Total Line Count: $($extStats['LineCount']), Total File Count: $($extStats['FileCount'])"
# }

# # 전체 결과 출력
# Write-Host "Total line count: $totalLineCount"
# Write-Host "Total file count: $totalFileCount"

# 확장자별 결과를 Markdown 형식으로 출력
Write-Host "### Extension Summary"
Write-Host "| Extension | Total Line Count | Total File Count |"
Write-Host "|-----------|------------------|------------------|"

foreach ($ext in $fileExtensions) {
    $extStats = $extensionStats[$ext]
    Write-Host "| $ext | $($extStats['LineCount']) | $($extStats['FileCount']) |"
}

# 전체 결과 출력
Write-Host "### Overall Summary"
Write-Host "| Total Line Count | Total File Count |"
Write-Host "|------------------|------------------|"
Write-Host "| $totalLineCount | $totalFileCount |"