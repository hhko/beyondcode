# .\new-buildprops.ps1
# .\new-buildprops.ps1 -t .\Backend\Api\Tests\ -i
# .\new-buildprops.ps1 -t .\Backend\Master\Tests\ -i

# Step 1: 모든 .csproj 파일을 스캔하여 각 PropertyGroup 항목의 속성과 값을 저장하고, 항목이 나타나는 횟수를 기록합니다.
# Step 2: 모든 프로젝트에서 공통으로 존재하는 속성만 필터링합니다. 속성이 모든 프로젝트에 중복으로 나타날 때만 commonProperties에 추가됩니다.
# Step 3: commonProperties를 이용해 Directory.Build.props 파일을 생성합니다. 만약 공통 속성이 없으면 파일을 생성하지 않습니다.
# Step 4: 각 .csproj 파일에서 Directory.Build.props로 옮긴 공통 속성을 제거하고 저장합니다.

param (
    [Alias("t", "targetdir")]
    [string]$TARGET_DIR = $(Split-Path -Parent $MyInvocation.MyCommand.Path),

    [Alias("i", "import ")]
    [switch]$Import
)

# Define the directory containing the .csproj files and the output Directory.Build.props path
$slnDir = $TARGET_DIR
Write-Host $slnDir
$directoryBuildPropsPath = "$slnDir\Directory.Build.props"

# Collect all PropertyGroups from .csproj files
$propertyCounts = @{}
$projectFiles = Get-ChildItem -Path $slnDir -Filter *.csproj -Recurse
$totalProjectCount = $projectFiles.Count

# Step 1: Scan each .csproj file for PropertyGroup items
foreach ($file in $projectFiles) {
    $csprojFile = $file.FullName
    $xml = [xml](Get-Content -Path $csprojFile)

    # Find all PropertyGroup items
    foreach ($propertyGroup in $xml.Project.PropertyGroup) {
        foreach ($property in $propertyGroup.ChildNodes) {
            $propertyName = $property.Name
            $propertyValue = $property.InnerText

            # Track the occurrence of each property-value pair
            $key = "$propertyName=$propertyValue"
            if ($propertyCounts.ContainsKey($key)) {
                $propertyCounts[$key]++
            } else {
                $propertyCounts[$key] = 1
            }
        }
    }
}

# Step 2: Filter only properties that are common to all projects
$commonProperties = @{}
foreach ($entry in $propertyCounts.GetEnumerator()) {
    if ($entry.Value -eq $totalProjectCount) {
        $parts = $entry.Key -split "="
        $propertyName = $parts[0]
        $propertyValue = $parts[1]
        $commonProperties[$propertyName] = $propertyValue
    }
}

# Step 3: Create Directory.Build.props with common PropertyGroup elements
if ($commonProperties.Count -gt 0) {
    $directoryBuildPropsContent = "<Project>"

    if ($Import) {
        $directoryBuildPropsContent += "`r`n`r`n  <Import Project=""`$([MSBuild]::GetPathOfFileAbove('Directory.Build.props', '`$(MSBuildThisFileDirectory)../'))"" />"
    }

    $directoryBuildPropsContent += "`r`n`r`n  <PropertyGroup>`r`n"

    foreach ($property in $commonProperties.Keys) {
        $directoryBuildPropsContent += "    <$property>$($commonProperties[$property])</$property>`r`n"
    }

    $directoryBuildPropsContent += "  </PropertyGroup>`r`n`r`n</Project>"

    # Write the Directory.Build.props file
    Set-Content -Path $directoryBuildPropsPath -Value $directoryBuildPropsContent
    Write-Host "Directory.Build.props file created with common properties."
} else {
    Write-Host "No common properties found across all projects."
}

# Step 4: Remove common properties from individual .csproj files
# foreach ($file in $projectFiles) {
#     $xml = [xml](Get-Content -Path $file.FullName)
#
#     foreach ($propertyGroup in $xml.Project.PropertyGroup) {
#         foreach ($property in $commonProperties.Keys) {
#             $node = $propertyGroup.SelectSingleNode($property)
#             if ($node -ne $null -and $node.InnerText -eq $commonProperties[$property]) {
#                 $propertyGroup.RemoveChild($node) | Out-Null
#             }
#         }
#     }
#
#     # Save the modified .csproj file
#     $xml.Save($file.FullName)
#     Write-Host "Updated and removed common properties from $($file.FullName)"
# }

# Step 4: Remove common properties from individual .csproj files without affecting blank lines
foreach ($file in $projectFiles) {
    $csprojFile = $file.FullName
    $fileContent = Get-Content -Path $csprojFile -Raw

    # Remove common properties by searching for lines that match each common property
    foreach ($property in $commonProperties.Keys) {
        # Use regex to remove the line containing the common property while keeping blank lines
        $propertyPattern = [regex]::Escape("<$property>$($commonProperties[$property])</$property>")
        $fileContent = $fileContent -replace "(?m)^\s*${propertyPattern}\s*$\r?\n?", ""
    }

    # Write back the modified content to the .csproj file, preserving blank lines
    Set-Content -Path $csprojFile -Value $fileContent
    # Write-Host "Updated and removed common properties from $csprojFile"
    Write-Host "Updated and removed common properties: $($file.Name)"
}

Write-Host "Script completed."
