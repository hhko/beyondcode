# 소스 생성기

## 개요

<br/>

## 지침


<br/>

## 소스 생성기 개발
### 패키징
- .nupkg 내부에서 분석기(Analyzer)로 인식시키기 위해 NuGet이 요구하는 경로에 어셈블리를 배포합니다.

```xml
<ItemGroup>
  <None Include="$(OutputPath)\$(AssemblyName).dll"
    Pack="true"
    PackagePath="analyzers/dotnet/cs"
    Visible="false" />
</ItemGroup>
```
```
YourPackage.nupkg
 ├── analyzers
 │   └── dotnet
 │       └── cs
 │           └── YourAnalyzer.dll   👈 여기 위치해야 인식됨
```

### 테스트
```cs
[Fact]
public Task EntityIdGenerator_ShouldGenerate_EntityIdAttribute()
{
  // Assert
  string input = string.Empty;

  // Act
  string? actual = _sut.Generate(input);  // TestGeneratorUtilities: 메모리에서 소스 코드 컴파일

  // Assert
  return Verify(actual);                  // 스냅샷 테스트
}

public static class TestGeneratorUtilities
{
    public static string? Generate<TGenerator>(this TGenerator generator, string sourceCode)
        where TGenerator : IIncrementalGenerator, new()
    {
        // 소스 코드에서 Syntax Tree 생성
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

        // 현재 로드된 어셈블리 중 동적이 아닌 것들을 참조로 변환
        var references = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .Where(assembly => !assembly.IsDynamic)
            .Select(assembly => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        var compilation = CSharpCompilation.Create(
            "SourceGeneratorTests",     // 생성할 어셈블리 이름
            [syntaxTree],               // 소스
            references,                 // 참조
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // 컴파일: 소스 생성기 호출출
        CSharpGeneratorDriver
            .Create(generator)
            .RunGeneratorsAndUpdateCompilation(
                compilation,
                out var outputCompilation,          // 소스 생성기 결과: 소스
                out var diagnostics);               // 소스 생성기 진단: 경고, 에러

        // 소스 생성기 진단(컴파일러 에러)
        diagnostics
            .Where(d => d.Severity == DiagnosticSeverity.Error)
            .ShouldBeEmpty();

        // 소스 생성기 결과(컴파일러 결과)
        return outputCompilation
            .SyntaxTrees
            .Skip(1)                // [0] 원본 소스 SyntaxTree 제외
            .LastOrDefault()?
            .ToString();
    }
}
```

<br/>

## 패키지 저장소
```xml
<?xml version="1.0" encoding="utf-8"?>

<configuration>

  <packageSources>
    <!--To inherit the global NuGet package sources remove the <clear/> line below -->
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
    <add key="local" value="./Abstractions" />
  </packageSources>

</configuration>
```

<br/>

## 소스 생성기 사용
### 패키지 참조 설정
```xml
<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
<ProjectReference Include=" ... .csproj"
                  OutputItemType="Analyzer"
                  ReferenceOutputAssembly="false" />
```
- `ReferenceOutputAssembly="true"`: 디버그 목적으로 `true`을 사용합니다.

### 패키지 참조 결과
```
obj\Debug\net9.0\
  generated\
    {어셈블리}\
      {어셈블리}.{네임스페이스}.{IIncrementalGenerator 구현 클래스}\
        {className}.g.cs

```
![](2025-05-04-01-00-29.png)

```
FunctionalDdd.SourceGenerator.Generators.EntityIdGenerator.EntityIdGenerator
```
- `FunctionalDdd.SourceGenerator`: 어셈블리
- `Generators.EntityIdGenerator`: 네임스페이스
- `EntityIdGenerator`: IIncrementalGenerator 구현 클래스