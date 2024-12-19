
# 참고 자료

## 아키텍처
### 클린 아키텍처 템플릿 소스
- [ ] **[SSW.CleanArchitecture](https://github.com/SSWConsulting/SSW.CleanArchitecture)**
- [ ] **[ardalis | CleanArchitecture](https://github.com/ardalis/CleanArchitecture)**
- [ ] **[ardalis | CleanArchitecture.WorkerService](https://github.com/ardalis/CleanArchitecture.WorkerService/tree/main)**
- [ ] **[amantinband | clean-architecture](https://github.com/amantinband/clean-architecture)**
- [ ] [Sam.CleanArchitecture](https://github.com/samanazadi1996/Sam.CleanArchitecture)
  - 개별 템플릿 만들기
- [ ] [Clean-Architecture-Template](https://github.com/babaktaremi/Clean-Architecture-Template)
- [ ] [Clean-Architecture-Template](https://github.com/babaktaremi/Clean-Architecture-Template)
  ```shell
  dotnet dev-certs https -ep $env:USERPROFILE/.aspnet/https/cleanarc.pfx -p Strong@Password
  dotnet dev-certs https --trust
  docker build -t bobby-cleanarc -f dockerfile.
  docker-compose up -d
  ```
- [ ] [dotnet-new-caju](https://github.com/ivanpaulovich/dotnet-new-caju)
  - https://paulovich.net/clean-architecture-for-net-applications/
- [ ] [clean-architecture-template](https://github.com/Genocs/clean-architecture-template)
- [ ] [VerticalSliceArchitecture](https://github.com/Hona/VerticalSliceArchitecture)
- [ ] [VerticalSliceArchitecture.Samples.Todos](https://github.com/Hona/VerticalSliceArchitecture.Samples.Todos)
- [ ] [from-zero-to-hero-vertical-slice-architecture](https://github.com/Dometrain/from-zero-to-hero-vertical-slice-architecture)
- [ ] [CleanMinimalApi](https://github.com/stphnwlsh/CleanMinimalApi)

### 관련 소스
- [ ] [eshop-app-workshop](https://github.com/dotnet-presentations/eshop-app-workshop)
- [ ] [SharedKernelSample](https://github.com/NimblePros/SharedKernelSample)
  - Domain과 Application 레이어 구현을 위한 기본 타입 기본 구현과 테스트 참고
- [ ] [modular-monolith-with-ddd](https://github.com/kgrzybek/modular-monolith-with-ddd)
- [ ] [ddd-guestbook](https://github.com/ardalis/ddd-guestbook)
- [ ] [CqrsInPractice](https://github.com/vkhorikov/CqrsInPractice)

### 아키텍처 이해
- [ ] [Hexagonal Architecture (Alistair Cockburn)](https://www.youtube.com/watch?v=k0ykTxw7s0Y)
  - [Hexagonal Architecture PDF](https://alistaircockburn.com/Hexagonal%20Budapest%2023-05-18.pdf)
- [ ] [Hexagonal Architecture - What Is It? Why Should You Use It?](https://www.happycoders.eu/software-craftsmanship/hexagonal-architecture/)
- [ ] [CodeMaze | Clean Architecture in .NET](https://code-maze.com/dotnet-clean-architecture/)
- [ ] [What are the Differences Between Onion Architecture and Clean Architecture in .NET?](https://code-maze.com/dotnet-differences-between-onion-architecture-and-clean-architecture/)


### DDD
- [x] [DDD 그리고 MSA](https://www.youtube.com/watch?v=DOpt6IWU6LU)  
  [![](./.images/DDDandMSA.png)](https://www.youtube.com/watch?v=DOpt6IWU6LU)
  - 주요 도서를 중심으로 도메인 주도 설계 역사를 이해할 수 있습니다.
- [ ] [Moving IO to the edges of your app](https://www.youtube.com/watch?v=P1vES9AgfC4)  
  [![](https://img.youtube.com/vi/P1vES9AgfC4/0.jpg)](https://www.youtube.com/watch?v=P1vES9AgfC4)
  - 아키텍처 관점에서 Pure Function과 Impure Function 배치의 중요성을 이해할 수 있습니다.
- [ ] [함수형 도메인 주도 설계 구현](https://liftio.org/2021/files/jisoo-park-ppt.pdf)

<br/>

## 테스트
### 아키텍처 테스트
- [x] [Bulletproof Your Software Architecture With ArchUnitNET](https://www.youtube.com/watch?v=R_srbvA6IQM)
  - ArchUnit 패키지 이해
- [ ] [Enforcing Software Architecture With Architecture Tests](https://www.milanjovanovic.tech/blog/enforcing-software-architecture-with-architecture-tests)
- [ ] [Shift Left With Architecture Testing in .NET](https://www.milanjovanovic.tech/blog/shift-left-with-architecture-testing-in-dotnet)
- [ ] [Enforcing Architecture Rules In .NET](https://honesdev.com/enforcing-architecture-rules-in-dotnet/)
- [ ] [rchitecture Refactoring with ArchUnitNET](https://www.production-ready.de/2023/12/10/architecture-refactoring-with-archunitnet-en.html)
- [ ] [PlantUML file diagram builder](https://archunitnet.readthedocs.io/en/latest/guide/#51-full-diagram-dependencies)

### 로그 테스트
- [ ] [How To: Test Logging when Using Microsoft.Extensions.Logging and Serilog](https://seankilleen.com/2024/04/how-to-test-logging-when-using-microsoft-extensions-logging-and-serilog/)

### 성능 테스트
- [ ] [Performance Testing of ASP.NET Core APIs With k6](https://code-maze.com/aspnetcore-performance-testing-with-k6/)

<br/>

## .NET
### SDK
- [ ] [.NET's hidden Garbage Collector - from 1.9GB to 85MB of memory?](https://www.youtube.com/watch?v=y7FTxAqExyU)
- [ ] [C#10 `record struct` Deep Dive & Performance Implications](https://nietras.com/2021/06/14/csharp-10-record-struct/)

### 코드 분석
- [ ] [Editorconfig In Visual Studio In 10 Minutes or Less](https://www.youtube.com/watch?v=CQW5b58mPdg&t)
  - editorconfig 탭 간격, 마지막 라인, 네임스페이 기본 값(컴파일러 수준)
- [ ] [How To Write Clean Code With The Help Of Static Code Analysis](https://www.youtube.com/watch?v=0nVT1gM4vPg)
  - Directory.Build.props 파일을 이용한 코드 분석 패키지 전역화, 코드 분석을 위한 빌드 설정

### 패키지
- [ ] [Publish MediatR Notifications in Parallel](https://code-maze.com/mediatr-parallel-publishing-notifications/)

### 테스트
- [ ] [Integration Testing for ASP.NET APIs](https://knowyourtoolset.com/2024/01/integration-testing/)
- [ ] [How to use TimeProvider and FakeTimeProvider (time abstraction in .NET)](https://grantwinney.com/how-to-use-timeprovider-and-faketimeprovider/)
- BackgroundService
  - [ ] [Handling Background Worker Unit Tests in ASP.NET](https://matt-ghafouri.medium.com/handling-background-worker-unit-tests-in-asp-net-77180e25697d)
  - [ ] [The NEW Way to Test Background Jobs | .NET 8](https://www.youtube.com/watch?v=uN1V0Sw34NQ)
  - [ ] [Windows 서비스에서 ASP.NET Core 호스트](https://learn.microsoft.com/ko-kr/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-9.0&tabs=visual-studio)
  ```
    dotnet publish -c Release __output "C:\custom\publish\directory"

    sc.exe create "서비스_이름" binpath="절대경로.exe"
    sc.exe create "서비스_이름"

    get-service "서비스_이름"
    start-service "서비스_이름"
    stop-service "서비스_이름"

    RuntimeIdentifier	-r win-x64
        https://learn.microsoft.com/en-us/dotnet/core/rid-catalog
    PlatformTarget		?
    PublishSingleFile	-p:PublishSingleFile=true
    PublishReadyToRun	-p:PublishReadyToRun=true
    SelfContained		__self-contained true
    DebugType			?

  <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  <PlatformTarget>x64</PlatformTarget>
  <PublishSingleFile>true</PublishSingleFile>
  <PublishReadyToRun>true</PublishReadyToRun>
  <SelfContained>true</SelfContained>
  <DebugType>embedded</DebugType>
  ```

### 코드 품질
- [.NET Source Code Analysis](https://swharden.com/blog/2023-03-05-dotnet-code-analysis/)
- [Treemapping with C#](https://swharden.com/blog/2023-03-07-treemapping/)
- [DotNet.GitHubActionMetrics](https://github.com/MarvinDrude/DotNet.GitHubActionMetrics)
- [Automate code metrics and class diagrams with GitHub Actions](https://devblogs.microsoft.com/dotnet/automate-code-metrics-and-class-diagrams-with-github-actions/)
- [Overview of .NET source code analysis](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview?tabs=net-9#enable-on-build)
  - [Namespace declaration preferences (IDE0160 and IDE0161)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161)
  - [Language and unnecessary rules](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/language-rules#option-format)
- [코드 메트릭 데이터 생성](https://learn.microsoft.com/ko-kr/visualstudio/code-quality/how-to-generate-code-metrics-data?view=vs-2022)


<br/>

## 시스템
### OpenTelemetry
- [ ] [practical-net-otelcollector](https://github.com/kimcuhoang/practical-net-otelcollector/tree/main)
  - https://dev.to/kim-ch/observability-net-opentelemetry-collector-25g1

### GitHub Actions
- [ ] [Beautiful .NET Test Reports Using GitHub Actions](https://seankilleen.com/2024/03/beautiful-net-test-reports-using-github-actions/)
- [ ] [.NET test and coverage reports in GitHub Actions](https://www.damirscorner.com/blog/posts/20240719-DotNetTestAndCoverageReportsInGitHubActions.html)
- [ ] [Code Coverage in .NET](https://code-maze.com/dotnet-code-coverage/)
- [ ] [Code Coverage Reports for .NET Projects](https://knowyourtoolset.com/2024/01/coverage-reports/)
