# 소스 생성기

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
- FunctionalDdd.SourceGenerator: 어셈블리
- Generators.EntityIdGenerator: 네임스페이스
- EntityIdGenerator: IIncrementalGenerator 구현 클래스