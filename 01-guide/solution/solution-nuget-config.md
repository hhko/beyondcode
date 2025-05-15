# NuGet 저장소 소스

## 개요
- `nuget.config` 파일은 NuGet 패키지 소스, 캐시 위치, 인증 정보 등 NuGet 동작을 설정하는 구성 파일입니다.

<br/>

## 지침
- 솔루션 루트에 `nuget.config` 파일을 생성하여, 프로젝트 빌드 시 사용할 NuGet 패키지 소스 경로를 명확히 지정합니다.

<br/>

## 생성 명령어
```shell
# nuget.config 파일 생성
dotnet new nuget.config
```

```xml
<?xml version="1.0" encoding="utf-8"?>

<configuration>

  <packageSources>
    <!--모든 기존 패키지 소스 제거 -->
    <clear />

    <!-- 공식 NuGet 저장소만 추가 -->
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </packageSources>

</configuration>
```
- 기존의 모든 NuGet 소스를 지우고(`<clearn />`), 공식 NuGet 저장소만 사용(`add key="nuget" ... />`)하도록 명시합니다.

<br/>

## 관리 명령어
```shell
# 구성된 모든 NuGet 소스 나열
dotnet nuget list source

  등록된 소스:
    1.  nuget [사용]
        https://api.nuget.org/v3/index.json
```

```shell
# NuGet이 사용하는 모든 로컬 캐시의 경로 표시
dotnet nuget locals all --list

    http-cache      : C:\Users\{사용자}\AppData\Local\NuGet\v3-cache
    global-packages : C:\Users\{사용자}\.nuget\packages\
    temp            : C:\Users\{사용자}\AppData\Local\Temp\NuGetScratch
    plugins-cache   : C:\Users\{사용자}\AppData\Local\NuGet\plugins-cache
```

```shell
# NuGet이 사용하는 모든 로컬 캐시 정리
dotnet nuget locals all --clear
```