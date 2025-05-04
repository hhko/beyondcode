# 테스트 Allure

## Allure 설치
- https://github.com/allure-framework/allure2/releases
  - `allure-*.zip` 최신 버전 다운로드

```shell
# Path 환경 설정
$AllureBinPath = "C:\Workspace\Tools\allure-2.34.0\bin"
$NewPath = (([Environment]::GetEnvironmentVariable("PATH", "User") -split ";") | ?{ $_ -and $_ -notlike "*\allure-*\bin" }) -join ";"
[Environment]::SetEnvironmentVariable("PATH", "$NewPath;$AllureBinPath", "User")
```

```shell
# 버전 확인
allure --version
```

<br/>

## Allure
- 패키지
  - Allure.Xunit
  - Allure.Reqnroll

- `.runsettings` 파일: reporter 변경(`xunit.runner.visualstudio` -> `allure`)
  ```xml
  <?xml version="1.0" encoding="utf-8"?>
  <RunSettings>
    <RunConfiguration>
      <ReporterSwitch>allure</ReporterSwitch>
    </RunConfiguration>
  </RunSettings>
  ```
  - 대안: `dotnet test -- RunConfiguration.ReporterSwitch=allure`
  - `--logger "console;verbosity=normal"`
  - https://xunit.net/docs/runsettings#ReporterSwitch

- `allureConfig.json`
  ```json
  {
    "$schema": "https://raw.githubusercontent.com/allure-framework/allure-csharp/2.10.0/Allure.XUnit/Schemas/allureConfig.schema.json",
    "allure": {
      "directory": "../../../../allure-results"
    }
  }
  ```
  ```xml
  <ItemGroup>
    <Content Include="allureConfig.json" CopyToOutputDirectory="PreserveNewest" />
  </ItemGroup>
  ```

- 실행
  ```shell
  # 테스트
  dotnet test

  # allure-report 폴더에 보고서 생성
  allure generate --clean

  # allure-report 폴더의 index.html 열기
  allure open
  ```

<br/>

## 참고 자료
- [Getting started with Allure xUnit.net](https://allurereport.org/docs/xunit/)
- [Getting started with Allure Reqnroll](https://allurereport.org/docs/reqnroll/)