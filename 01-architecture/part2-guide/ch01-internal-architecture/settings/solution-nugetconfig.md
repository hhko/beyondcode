# 솔루션 설정 nuget.config

## 개요
- NuGet 패키지 관리자의 동작을 설정하는 XML 기반 구성 파일입니다.

## 명령
### 생성 명령
```shell
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

### 확인 명령
```shell
# 구성된 모든 NuGet 소스를 나열합니다
dotnet nuget list source

등록된 소스:
  1.  nuget [사용]
      https://api.nuget.org/v3/index.json

# http 요청 캐시, 패키지 폴더, 플러그 인 작업 캐시 또는 시스템 전체의 전역 패키지 폴더와 같은
# 로컬 NuGet 리소스를 지우거나 나열합니다.
dotnet nuget locals all --list

    http-cache: C:\Users\{사용자}\AppData\Local\NuGet\v3-cache
    global-packages: C:\Users\{사용자}\.nuget\packages\
    temp: C:\Users\{사용자}\AppData\Local\Temp\NuGetScratch
    plugins-cache: C:\Users\{사용자}\AppData\Local\NuGet\plugins-cache
```