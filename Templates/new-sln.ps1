# .\new-sln.ps1 -t1 Crop -t2 Hello -t3s Master, Api
#   - T1: Corporation
#   - T2: Solution
#   - T3S: Service    // Backend
#   - T3U: UI         // Frontend

# DONE
#   - [x] Solution 생성
#   - [x] Assets 생성
#   - [x] Backend 생성
# TODO
#   - [ ] Frontend 생성

# {T2}.sln
#   │ # Assets 범주: 공윶 자원
#   ├─Assets
#   │   ├─Frameworks
#   │   │   ├─Src
#   │   │   │   ├─{T1}.{T2}.Framework
#   │   │   │   └─{T1}.{T2}.Framework.Contracts
#   │   │   └─Tests
#   │   │       └─{T1}.{T2}.Framework.Test.Unit
#   │   ├─Libraries
#   │   │   └─{T1}.{T2}.[Tech]                                    // 예. RabbitMQ, ...
#   │   └─Domains
#   │       ├─Src
#   │       │   └─{T1}.{T2}.Domain
#   │       └─Tests
#   │           └─{T1}.{T2}.Domain.Test.Unit                      // 공유 도메인
#   │
#   │ # Backend 범주
#   ├─Backend
#   │   ├─{T3}
#   │   │   ├─Src
#   │   │   │   ├─{T1}.{T2}.{T3}                                  // 호스트 프로젝트
#   │   │   │   ├─{T1}.{T2}.{T3}.Adapters.Infrastructure          // Adapter 레이어
#   │   │   │   ├─{T1}.{T2}.{T3}.Adapters.Persistence             // Adapter 레이어
#   │   │   │   ├─{T1}.{T2}.{T3}.Application                      // Application 레이어
#   │   │   │   └─{T1}.{T2}.{T3}.Domain                           // Domain 레이어
#   │   │   └─Tests
#   │   │       ├─{T1}.{T2}.{T3}.Tests.Integration                // Integration 테스트
#   │   │       ├─{T1}.{T2}.{T3}.Tests.Performance                // Performance 테스트
#   │   │       └─{T1}.{T2}.{T3}.Tests.Unit                       // Unit Test
#   │   ├─{T3}
#   │   │   ├─Src
#   │   │   └─Tests
#   │   └─Tests
#   │       └─{T1}.{T2}.Tests.E2E                                 // End to End 테스트
#   │
#   │ # Frontend 범주
#   └─Frontend
#       └─{T3}
#           ├─Src
#           │   ├─{T1}.{T2}.{T3}                                  // 호스트 프로젝트
#           │   ├─{T1}.{T2}.{T3}.Adapters.Infrastructure          // Adapter 레이어
#           │   ├─{T1}.{T2}.{T3}.Adapters.Persistence             // Adapter 레이어
#           │   ├─{T1}.{T2}.{T3}.Application                      // Application 레이어
#           │   └─{T1}.{T2}.{T3}.Domain                           // Domain 레이어
#           └─Tests
#               ├─{T1}.{T2}.{T3}.Tests.Integration                // Integration 테스트
#               ├─{T1}.{T2}.{T3}.Tests.Performance                // Performance 테스트
#               └─{T1}.{T2}.{T3}.Tests.Unit                       // Unit Test

param(
  [Parameter(Mandatory=$true)]
  [Alias("t1", "corporation")]
  [string]$T1_CORPORATION,

  [Parameter(Mandatory=$true)]
  [Alias("t2", "solution")]
  [string]$T2_SOLUTION,

  [Parameter(Mandatory=$true)]
  [Alias("t3s", "services")]
  [string[]]$T3_SERVICES
)

$commonServicePackages = @{
  Host = @(
    "Microsoft.Extensions.Hosting",
    "Microsoft.Extensions.Logging",
    "Quartz.Extensions.Hosting")
  AdaptersInfrastructure = @(
    "Serilog.Sinks.Console",
    "Serilog.Sinks.File",
    "Serilog.Exceptions", "Serilog.AspNetCore")
  AdaptersPersistence = @(
    "Microsoft.EntityFrameworkCore.Design",
    "Microsoft.EntityFrameworkCore.Tools",
    "Npgsql.EntityFrameworkCore.PostgreSQL",
    "Oracle.EntityFrameworkCore")
  Application = @(
    "MediatR")
  Domain = @(
    "MediatR.Contracts")
}

$commonTestPackages = @{
  Unit = @(
    "Microsoft.NET.Test.Sdk",
    "xunit",
    "xunit.runner.visualstudio",
    "FluentAssertions",
    "coverlet.collector",
    "JunitXml.TestLogger",
    "NetArchTest.Rules")
  Integration = @(
    "Microsoft.NET.Test.Sdk",
    "xunit",
    "xunit.runner.visualstudio",
    "FluentAssertions",
    "coverlet.collector",
    "JunitXml.TestLogger",
    "Testcontainers.PostgreSql",
    "Testcontainers.RabbitMq",
    "Testcontainers.Oracle")
  E2E = @(
    "Microsoft.NET.Test.Sdk",
    "xunit",
    "xunit.runner.visualstudio",
    "FluentAssertions",
    "coverlet.collector",
    "JunitXml.TestLogger")
}

$curDir = Split-Path -parent $MyInvocation.MyCommand.Definition

enum TestType {
  Unit
  Integration
  E2E
}

function Check-PowerShellVersion {
  if ($PSVersionTable.PSVersion.Major -lt 7) {
    Write-Error "This script requires PowerShell 7 or higher."
    exit
  }
}

function Initialize-Solution {
  param ([string]$solutionName)

  dotnet new sln -n $solutionName --force
  dotnet new nuget.config --force
}

function Create-AssetsStructure {
  $baseFrameworkPath = "${curDir}/Assets/Frameworks/Src/${T1_CORPORATION}.${T2_SOLUTION}.Framework"

  # Frameworks
  dotnet new classlib -o $baseFrameworkPath --force
  dotnet new classlib -o "${baseFrameworkPath}.Contracts" --force
  dotnet add $baseFrameworkPath reference "${baseFrameworkPath}.Contracts"

  # Frameworks Tests
  $testBaseFrameworkPath = "${curDir}/Assets/Frameworks/Tests/${T1_CORPORATION}.${T2_SOLUTION}.Framework.Tests"
  dotnet new xunit -o "${testBaseFrameworkPath}.Unit" --force
  dotnet add "${testBaseFrameworkPath}.Unit" reference $baseFrameworkPath

  # Libraries
  New-Item "${curDir}/Assets/Libraries/.gitkeep" -ItemType File -Force | Out-Null

  # Domains
  $baseDomainPath = "${curDir}/Assets/Domains/Src/${T1_CORPORATION}.${T2_SOLUTION}.Domain"
  dotnet new classlib -o $baseDomainPath --force
  dotnet add $baseDomainPath reference "${baseFrameworkPath}.Contracts"

  # Domains Tests
  $testBaseDomainPath = "${curDir}/Assets/Domains/Tests/${T1_CORPORATION}.${T2_SOLUTION}.Domain"
  dotnet new xunit -o "${testBaseDomainPath}.Unit" --force
  dotnet add "${testBaseDomainPath}.Unit" reference $baseDomainPath
}

function Create-ProjectStructure {
  param ([string]$service)

  # Service Projects
  $basePath = "${curDir}/Backend/${service}/Src/${T1_CORPORATION}.${T2_SOLUTION}.${service}"
  dotnet new console -o  "${basePath}" --force
  dotnet new classlib -o "${basePath}.Adapters.Infrastructure" --force
  dotnet new classlib -o "${basePath}.Adapters.Persistence" --force
  dotnet new classlib -o "${basePath}.Application" --force
  dotnet new classlib -o "${basePath}.Domain" --force

  # Test Projects
  $testBasePath = "${curDir}/Backend/${service}/Tests/${T1_CORPORATION}.${T2_SOLUTION}.${service}.Tests"
  dotnet new xunit -o "${testBasePath}.Integration" --force
  dotnet new xunit -o "${testBasePath}.Unit" --force
  New-Item "${testBasePath}.Perforance/.gitkeep" -ItemType File -Force | Out-Null
}

function Add-ServiceReferences {
  param ([string]$service)

  $basePath = "${curDir}/Backend/${service}/Src/${T1_CORPORATION}.${T2_SOLUTION}.${service}"

  # References for different project layers
  dotnet add $basePath reference "${basePath}.Adapters.Infrastructure"
  dotnet add $basePath reference "${basePath}.Adapters.Persistence"
  dotnet add $basePath reference "${basePath}.Application"

  dotnet add "${basePath}.Adapters.Infrastructure" reference "${basePath}.Application"
  dotnet add "${basePath}.Adapters.Persistence"    reference "${basePath}.Application"
  dotnet add "${basePath}.Application"             reference "${basePath}.Domain"

  # Assets References
  $frameworkPath = "${curDir}/Assets/Frameworks/Src/${T1_CORPORATION}.${T2_SOLUTION}.Framework"
  $domainPath = "${curDir}/Assets/Domains/Src/${T1_CORPORATION}.${T2_SOLUTION}.Domain"

  dotnet add "${basePath}.Application" reference $frameworkPath
  dotnet add "${basePath}.Application" reference $domainPath
  dotnet add "${basePath}.Domain"      reference "${frameworkPath}.Contracts"
}

function Add-TestReferences {
  param (
    [string]$service,
    [TestType]$testType
  )
  $testPath = switch ($testType) {
    Unit        { "${curDir}/Backend/${service}/Tests/${T1_CORPORATION}.${T2_SOLUTION}.${service}.Tests.Unit" }
    Integration { "${curDir}/Backend/${service}/Tests/${T1_CORPORATION}.${T2_SOLUTION}.${service}.Tests.Integration" }
    E2E         { "${curDir}/Backend/Tests/${T1_CORPORATION}.${T2_SOLUTION}.Tests.E2E" }
  }

  $servicePath = "${curDir}/Backend/${service}/Src/${T1_CORPORATION}.${T2_SOLUTION}.${service}"

  Write-Host $testPath -ForegroundColor Yellow
  Write-Host $servicePath -ForegroundColor Yellow

  switch ($testType) {
    Unit {
      dotnet add $testPath reference "${servicePath}.Adapters.Infrastructure"
      dotnet add $testPath reference "${servicePath}.Adapters.Persistence"
      dotnet add $testPath reference "${servicePath}.Application"
      dotnet add $testPath reference "${servicePath}.Domain"
    }
    Integration {
      dotnet add $testPath reference "${servicePath}.Adapters.Infrastructure"
      dotnet add $testPath reference "${servicePath}.Adapters.Persistence"
    }
    E2E {
      Write-Host "xxxx---->>>"
      dotnet add $testPath reference $servicePath
    }
  }
}

function Add-Packages {
  param (
    [string]$path,
    [string[]]$packages
  )
  foreach ($package in $packages) {
    dotnet add $path package $package
  }
}

function Add-ServicePackages {
  param ([string]$service)

  $basePath = "${curDir}/Backend/${service}/Src/${T1_CORPORATION}.${T2_SOLUTION}.${service}"
  Add-Packages $basePath $commonServicePackages.Host
  Add-Packages "${basePath}.Adapters.Infrastructure" $commonServicePackages.AdaptersInfrastructure
  Add-Packages "${basePath}.Adapters.Persistence" $commonServicePackages.AdaptersPersistence
  Add-Packages "${basePath}.Application" $commonServicePackages.Application
  Add-Packages "${basePath}.Domain" $commonServicePackages.Domain
}

function Add-TestPackages {
  param (
    [string]$service,
    [TestType]$testType
  )
  $testPath = switch ($testType) {
    Unit        { "${curDir}/Backend/${service}/Tests/${T1_CORPORATION}.${T2_SOLUTION}.${service}.Tests.Unit" }
    Integration { "${curDir}/Backend/${service}/Tests/${T1_CORPORATION}.${T2_SOLUTION}.${service}.Tests.Integration" }
    E2E         { "${curDir}/Backend/Tests/${T1_CORPORATION}.${T2_SOLUTION}.Tests.E2E" }
  }

  $packages = switch ($testType) {
    Unit        { $commonTestPackages.Unit }
    Integration { $commonTestPackages.Integration }
    E2E         { $commonTestPackages.E2E }
  }

  Add-Packages $testPath $packages
}

# ----------------------------
# 메일 스크립트
# ----------------------------

Check-PowerShellVersion

# 1/5. 솔루션 초기화
Write-Host
Write-Host "Initialize Solution: ${T2_SOLUTION}" -ForegroundColor Blue
Initialize-Solution -solutionName $T2_SOLUTION

# 2/5. Asset 프로젝트 생성
Write-Host
Write-Host "Create Asset" -ForegroundColor Blue
Create-AssetsStructure

# 3/5. Backend 프로젝트 생성
foreach ($service in $T3_SERVICES) {
  Write-Host
  Write-Host "Create Service: ${service}" -ForegroundColor Blue

  Create-ProjectStructure -service $service
  Add-ServiceReferences -service $service
  Add-TestReferences -service $service -testType Unit
  Add-TestReferences -service $service -testType Integration

  Add-ServicePackages -service $service
  Add-TestPackages -service $service -testType Unit
  Add-TestPackages -service $service -testType Integration
}

dotnet new xunit -o "${curDir}/Backend/Tests/${T1_CORPORATION}.${T2_SOLUTION}.Tests.E2E" --force
Add-TestPackages -testType E2E
Write-Host "테스트" -ForegroundColor Yellow
foreach ($service in $T3_SERVICES) {
  Write-Host "$service" -ForegroundColor Yellow
  Add-TestReferences -service $service -testType E2E
}

# 4/5. TODO: Frontend 프로젝트 생성
Write-Host
Write-Host "Create Frontend" -ForegroundColor Blue
New-Item "${curDir}/Frontend/.gitkeep" -ItemType File -Force | Out-Null

# 5/5. 솔루션 빌드
Write-Host
Write-Host "Building Solution" -ForegroundColor Blue
dotnet sln "${curDir}/${T2_SOLUTION}.sln" add (ls -r ./**/*.csproj)
dotnet build "${curDir}/${T2_SOLUTION}.sln"