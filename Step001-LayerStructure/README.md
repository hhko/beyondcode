# 레이어 구성

## 목표
- [x] 레이어 구성
  ```shell
  Adapters.Infrastructure     # 기술
  Adapters.Persistence
  Adapters.Presentation
  Application                 # 비즈니스 흐름
  Domain                      # 비즈니스 단위
  ```
- [x] 레이어 테스트 구분
  ```shell
  [Trait(nameof(UnitTest), UnitTest.Architecture)]
  ```
- [x] 레이어 의존성 테스트
  - [ArchUnitNET](https://github.com/TNG/ArchUnitNET)
  - [Bulletproof Your Software Architecture With ArchUnitNET](https://www.youtube.com/watch?v=R_srbvA6IQM)
- [x] 레이어 다이어그램
  - [DependencyVisualizer](https://github.com/nkolev92/DependencyVisualizer)
  ```
  dotnet tool install -g DependencyVisualizerTool --version 1.0.0-beta.3
  dotnet tool list -g

  DependencyVisualizer .\Backend\Api\Src\Crop.Hello.Api\Crop.Hello.Api.csproj --projects-only
  ```

## TODO
- [ ] AssemblyReference.cs 파일 존재 테스트

## 레이어 구성
![](./.images/Architecture.Layers.png)

![](./.images/Architecture.Diagram.png)

## AssemblyReference.cs 파일

![](./.images/AssemblyReference.png)

```cs
using System.Reflection;

namespace Crop.Hello.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```

## Directory.Build.props 파일

```xml
<Project>

  <!-- 상위 Directory.Build.props 파일 지정 Import -->
  <Import Project="$([MSBuild]::GetPathOfFileAbove('Directory.Build.props', '$(MSBuildThisFileDirectory)../'))" />

  <!-- 테스트 프로젝트 공통 속성 -->
  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <!-- 솔루션 탐색기에서 TestResults 폴더 제외 -->
  <ItemGroup>
    <None Remove="TestResults\**" />
  </ItemGroup>

  <!-- xunit.runner.json 설정 -->
  <ItemGroup>
    <Content Include="xunit.runner.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>

  <!-- 전역 using 구문 -->
  <ItemGroup>
    <Using Include="Xunit" />
    <Using Include="FluentAssertions" />
  </ItemGroup>

</Project>
```

## xunit.runner.json 파일

```json
{
  "$schema": "https://xunit.net/schema/current/xunit.runner.schema.json",
  "methodDisplay": "method",
  "diagnosticMessages": true
}
```

## 레이어 테스트 구분
```cs
internal static partial class Constants
{
    public static class UnitTest
    {
        public const string Architecture = nameof(Architecture);
    }
}

[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class LayerDependencyTests : ArchitectureBaseTest
```

## 레이어 의존성 테스트

![](./.images/Architecture.TestSolution.png)

![](./.images/Architecture.Tests.png)

- 테스트 목록
  - DomainLayer_ShouldNotHave_Dependencies_OnAnyOtherLayer
  - ApplicationLayer_ShouldNotHave_Dependencies_OnAdapterLayer
  - AdapterLayer_ShouldNotHave_Dependencies_OnDomainLayer

```cs
// 검증 대상
protected static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(
        Adapters.Infrastructure.AssemblyReference.Assembly,
        Adapters.Persistence.AssemblyReference.Assembly,
        Application.AssemblyReference.Assembly,
        Domain.AssemblyReference.Assembly)
    .Build();

ArchRuleDefinition
    .Types()
    .That()
    .Are(DomainLayer)           // SUT
    .Should()
    .NotDependOnAny(layer)
    .Check(Architecture);       // 검증 대상
```