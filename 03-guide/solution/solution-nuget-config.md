# 솔루션 NuGet 소스 설정

## 개요
- `nuget.config` 파일은 NuGet 패키지 소스, 캐시 위치, 인증 정보 등 NuGet 동작을 설정하는 구성 파일입니다.

<br/>

## 생성 명령
```shell
# nuget.config 파일 생성
dotnet new nuget.config
```

```xml
<?xml version="1.0" encoding="utf-8"?>

<configuration>

  <packageSources>
    <!--To inherit the global NuGet package sources remove the <clear/> line below -->
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
  </packageSources>

</configuration>
```
- 기존의 모든 NuGet 소스를 지우고(`<clearn />`), 공식 NuGet 저장소만 사용(`add key="nuget" ... />`)하도록 명시합니다.

<br/>

## 관리 명령
```shell
# 구성된 모든 NuGet 소스 나열
dotnet nuget list source

  등록된 소스:
    1.  nuget [사용]
        https://api.nuget.org/v3/index.json
```

```shell
# NuGet이 사용하는 모든 로컬 캐시의 경로를 표시합니다.
dotnet nuget locals all --list

    http-cache      : C:\Users\{사용자}\AppData\Local\NuGet\v3-cache
    global-packages : C:\Users\{사용자}\.nuget\packages\
    temp            : C:\Users\{사용자}\AppData\Local\Temp\NuGetScratch
    plugins-cache   : C:\Users\{사용자}\AppData\Local\NuGet\plugins-cache

dotnet nuget locals all --clear
```