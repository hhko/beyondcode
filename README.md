> 슬기로운 코드를 만들기 위한 발자취

# 목차
- Part 0. 세미나
  - [x] [Ch 01. Internal 아키텍처 개요](./Part0.Seminar/Ch01.InternalArchitecture-Overview/README.md)
- Part 1. 개요
  - [x] [Ch 01. 기술 맵](#ch-1-기술-맵)
  - [x] [Ch 02. Internal 아키텍처](#ch-2-internal-아키텍처)
  - [ ] Ch 03. External 아키텍처
- Part 2. 아키텍처
  - [x] [Ch 03. 아키텍처 개요](#ch-3-아키텍처-개요)
  - [x] [Ch 04. 아키텍처 원칙](#ch-4-아키텍처-원칙)
  - [x] [Ch 05. 레이어 격리](#ch-5-레이어-격리)
  - [x] [Ch 06. 레이어 테스트](#ch-6-레이어-테스트)
  - [x] [Ch 07. 레이어 고도화](#ch-7-레이어-고도화)
  - [x] [Ch 08. 서비스 통합](#ch-8-서비스-통합)
  - [x] [Ch 09. Internal 아키텍처 비교](#ch-9-internal-아키텍처-비교)
- Part 3. 솔루션
  - [x] [Ch 10. 솔루션 구조](#ch-10-솔루션-구조)
  - [x] [Ch 11. 솔루션 빌드 설정](#ch-11-솔루션-빌드-설정)
  - [ ] [Ch 12. 솔루션 코드 분석](#ch-12-솔루션-코드-분석)
  - [x] [Ch 13. 솔루션 아키텍처 테스트](#ch-13-솔루션-아키텍처-테스트)
  - [ ] [Ch 14. 솔루션 레이어 의존성 주입](#ch-14-솔루션-레이어-의존성-주입)
  - [ ] Ch 15. 솔루션 빌드 자동화
  - [ ] Ch 16. 솔루션 컨테이너 배포 자동화
- Part 4. 관찰 가능성
  - [ ] Ch 17. Aspire 대시보드
  - [ ] cH 18. Grafana 시스템
  - [ ] Ch 19. OpenSearch 시스템
  - [ ] Ch 20. 로그
  - [ ] Ch 21. 추적
  - [ ] Ch 22. 지표
- Part 5. Internal 전술 설계
  - [x] [Ch 23. 전술 설계 맵](#ch-23-전술-설계-맵)
  - [ ] [Ch 24. 출력 기본 타입(Result)](#ch-24-출력-기본-타입)
  - [ ] Ch 25. 도메인 기본 타입
  - [ ] TODO
- Part 6. External 전술 설계
- Part 7. 전략 설계

<br/>

# Part 1. 개요
## Ch 1. 기술 맵
![](./.images/TechMap.png)

## Ch 2. Internal 아키텍처
> - 내부 아키텍처는 레이어 배치입니다.
> - **Application 레이어가** 내부 아키텍처의 레이어를 주관(主管)합니다.

![](./.images/Architecture.Internal.png)

- 아키텍처
  - `MediatR`
- 테스트 패키지
  - `xunit`
  - `FluentAssertions`
  - `TngTech.ArchUnitNET.xUnit`

## Ch 03. External 아키텍처
> - 외부 아키텍처는 서비스 배치입니다.

- TODO

<br/>


---

<br/>

# Part 2. 아키텍처

# Ch 3. 아키텍처 개요

## Ch 3.1 아키텍처 정의
![](./.images/Architecture.png)

※ 출처: [Making Architecture Matter, 소프트웨어 아키텍처의 중요성](https://www.youtube.com/watch?v=4E1BHTvhB7Y)

## Ch 3.2 아키텍처 범주
![](./.images/Architecture.Category.png)

※ 출처: [Making old applications new again](https://sellingsimplifiedinsights.com/asset/app-development/ASSET_co-modernization-whitepaper-inc0460201-122016kata-v1-en_1511772094768.pdf)

```
Application Architecture
  ├─ Monolithic Architecture
  ├─ Modular Monolithic Architecture
  ├─ N-tier Architecture
  └─ Microservices Architecture
      ├─ Internal Architecture
      │    └─ Layered Architecture
      │         ├─ Hexagonal Architecture
      │         ├─ Onion Architecture
      │         ├─ Clean Architecture
      │         ├─ Vertical Slice Architecture
      │         └─ ...
      │
      └─ External Architecture
           └─ 외부 시스템 구성 아키텍처: 예. CNCF Landscape
```
- Microservices Architecture = Internal Architecture + External Architecture
  ![](./.images/Architecture.Microservices.png)

  ※ 출처: [DDD 및 CQRS 패턴을 사용하여 마이크로 서비스에서 비즈니스 복잡성 처리](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/)

## Ch 3.3 아키텍처 역사
![](./.images/Architecture.History.png)

  ※ 출처: [The Grand Unified Theory of Clean Architecture and  Pyramid ](https://www.youtube.com/watch?v=mzznsq4jCHY)

<br/>

# Ch 4. 아키텍처 원칙
> 아키텍처 원칙: [Separation of concerns](https://learn.microsoft.com/ko-kr/dotnet/architecture/modern-web-apps-azure/architectural-principles#separation-of-concerns)

![](./.images/Architecture.Vs.png)

## Ch 4.1 관심사의 분리
- 개발 시 요구사항과 운영 시 로그는 서로 다른 시점이지만, **코드에 대한 관점은 Biz.와 Tech. 관심사 기준으로 같아야 합니다.**
  - **개발 시** 요구사항을 비즈니스와 기술 관심사로 분해합니다.
  - **운영 시** 로그를 비즈니스와 기술 관심사로 식별합니다.

![](./.images/Layer.SoC.Abstraction.png)

## Ch 4.2 레이어
- 개발 시 요구사항과 운영 시 로그는 서로 다른 시점이지만, **코드에 대한 관점은 레이어 기준으로 동일해야 합니다.**

![](./.images/Layer.SoC.png)

- **비즈니스 관심사**
  - Application: 비즈니스 흐름(Biz. Flow)
  - Domain: 비즈니스 단위(Biz. Unit)
- **기술 관심사**
  - Adapter
    - `Known` 입력 Adapter
    - ~~`Unknown` 입력 Adapter~~
    - `Known` 출력 Adapter
    - `Unknown` 출력 Adapter: 부수 효과(Side Effects)

## Ch 4.3 레이어 배치
![](./.images/Layer.Alignment.Known.png)

![](./.images/Layer.Alignment.Unknown.png)

<br/>

# Ch 5. 레이어 격리

## Ch 5.1 격리 전
![](./.images/Layer.Isolation.Before.png)
- 출력의 변화 영향이 입력까지 전파됩니다.

## Ch 5.2 격리 후
![](./.images/Layer.Isolation.After.png)
- 입출력 인터페이스를 활용하여, 입출력 변화의 영향이 Operation 레이어에 전파되지 않도록 차단합니다(Strategy 패턴).

<br/>

# Ch 6. 레이어 테스트

![](./.images/Layer.Isolation.Test.png)
- 단위 테스트: Biz. 관심사를 테스트합니다.
- 통합 테스트: Tech. 관심사까지 포함하여 Biz. 관심사를 테스트합니다.

<br/>

# Ch 7. 레이어 고도화

## Ch 7.1 격리 고도화
![](./.images/Layer.Mediator.png)

- Mediator 패턴을 활용하여, 격리된 레이어 간의 소통을 위해 인터페이스의 입출력을 메시지 기반으로 단순화합니다.
  - 메시지는 컴파일 타임과 런타임 모두에서 호출자와 수신자 정보를 숨길 수 있습니다(느슨한 결합).
    | 구분             | Mediator  패턴  | Strategy  패턴 |
    | ---              | ---            | ---            |
    | **Compile-time** | Unknown        | Unknown        |
    | **Runtime**      | Unknown        | Known          |
  - 메시지는 런타임에 메시지에 부가 기능을 더 쉽게 추가할 수 있습니다(Decorator 패턴)
  - 메시지는 입출력을 범주화할 수 있습니다(Command 메시지와 Query 메시지: CQRS 패턴).

## Ch 7.2 메시지 고도화
![](./.images/Layer.Decorator.Known.png)
- Mediator 패턴은 Decorator 패턴과 조합하여 동적으로 메시지에 새 기능을 추가할 수 있습니다.
  - 예. 메시지 처리 시간 로그
  - 예. 입력 메시지 유효성 검사
  - 예. Command 메시지일 때 트랜잭션 처리(CQRS 패턴)

![](./.images/Layer.Decorator.Unknown.png)

## Ch 7.3 메시지 범주화(CQRS)
![](./.images/Layer.CQRS.png)

- Mediator 패턴을 통해 데이터 쓰기를 위한 메시지(Command)와 데이터를 읽기 위한 메시지(Query)로 구분할 수 있습니다.
  - Command 메시지: 데이터 가변(`CUD`:`Create, Update, Delete`)
  - Query 메시지: 데이터 불변(`R`: `Read`)
- `Command`: ORM(OLTP, Create, Update, Delete)
  - Command는 데이터의 상태를 변경하는 작업을 담당합니다.
  - 이 작업은 일반적으로 여러 테이블을 참조하거나 복잡한 트랜잭션을 포함할 수 있습니다.
  - 따라서 Command는 데이터베이스에 변경을 가하는데, 복잡한 로직을 처리하거나 여러 엔티티와 상호작용할 수 있습니다.
- `Query`: SQL(OLAP, Read)
  - Query는 데이터베이스에서 데이터를 읽어오는 작업에 해당합니다.
  - 일반적으로 Command보다 쿼리의 수가 많을 수 있으며, 데이터 조회만을 목적으로 하므로 복잡도가 낮고 최적화된 방식으로 실행됩니다.
  - Query는 데이터의 상태를 변경하지 않고, 데이터를 읽어오는 데 집중합니다.

## Ch 7.4 메시지 범주화(CQRS) 흐름
![](./.images/Layer.CQRS.Flow.png)
※ 출처: [Module Requests Processing via CQRS](https://github.com/kgrzybek/modular-monolith-with-ddd?tab=readme-ov-file#34-module-requests-processing-via-cqrs)  

| 구분       | Command  | Query      |
| ---        | ---      | ---        |
| 트랜잭션    | O(필요)  | X(불 필요)  |
| 구현       | ORM      | SQL        |
| DTO 변환   | O(필요)  | X(불 필요)  |
| SQL 복잡도 | ↓(낮다)  | ↑(높다)     |

- 데이터 읽기 위한 메시지 처리에서는 SQL 구문을 사용하여 DTO 데이터 변환 없이 데이터베이스 조회 결과를 바로 반환합니다.

<br/>

# Ch 8. 서비스 통합
![](./.images/Architecture.Internal.Integration.png)

- 서비스 통합은 Biz. 관심사와 분리하여 Tech. 관심사(Adapter 레이어) 중심으로 구성할 수 있게 됩니다(Microservice 아키텍처 패턴).

<br/>

# Ch 9. Internal 아키텍처 비교
## Ch 9.1 Port 비교
![](./.images/Architecture.Vs.Port.png)

| 구분                | 아키텍처        | 헥사고날 아키텍처 |
| ---                 | ---           | ---               |
| Known 입출력 Port   | Mediator 패턴  | Strategy 패턴      |
| Unknown 입출력 Port | Strategy 패턴  | Strategy 패턴      |

- 헥사고날 아키텍처에서는 Known과 Unknown 외부 입출력을 명시적으로 구분하지 않지만, 우리는 이를 구분하여 Port를 정의합니다.
  - Known 입출력은 Mediator 패턴을 활용하여 메시지 기반으로 처리합니다.
  - Unknown 입출력은 Strategy 패턴을 사용하여 인터페이스를 통해 처리합니다.

## Ch 9.2 Message 비교
![](./.images/Architecture.Vs.Message.png)

- 데이터 쓰기를 위한 메시지(Command)와 데이터를 읽기 위한 메시지(Query)로 구분합니다.
- 모든 메시지를 대상으로 부가 기능을 Decorator로 추가합니다.

## Ch 9.3 Adapter 비교
![](./.images/Architecture.Vs.Adapter.png)

- Known과 Unknown 외부 입출력을 명시적으로 구분하여 Adapter 위치를 배치합니다.
  - Known 입출력은 Mediator 패턴을 활용하여 메시지 발신과 수신을 구현합니다.
  - Unknown 입출력은 Strategy 패턴을 사용하여 인터페이스을 구현합니다.

## Ch 9.4 Application 비교
![](./.images/Architecture.Vs.Application.png)

- Application은 동일하게 모두 DDD 전술 설계 패턴에서 제시하는 Application Service 중심으로 구현됩니다.

## Ch 9.5 Domain 비교
![](./.images/Architecture.Vs.Domain.png)

- Domain은 동일하게 모두 DDD 전술 설계 패턴에서 제시하는 Entity와 Value Object 그리고 Domain Service을 중심으로 구현됩니다.

<br/>

---

<br/>

# Part 3. 솔루션

# Ch 10. 솔루션 구조
> 예제 코드: [링크](./Ch08.SolutionStructure/)

```shell
.\new-sln.ps1 -t1 Crop -t2 Hello -t3s Master, Api
```
- new-sln.ps1 파일: [링크](./Templates/new-sln.ps1)

## Ch 10.1 솔루션 구조 템플릿
```shell
{T2}.sln
  │ # Asset 범주: 공유 자산
  ├─Assets
  │   ├─Frameworks
  │   │   ├─Src
  │   │   │   ├─{T1}.{T2}.Framework
  │   │   │   └─{T1}.{T2}.Framework.Contracts
  │   │   └─Tests
  │   │       └─{T1}.{T2}.Framework.Tests.Unit
  │   ├─Libraries
  │   │   └─{T1}.{T2}.[Tech]                                    // 예. RabbitMQ, ...
  │   └─Domains
  │       ├─Src
  │       │   └─{T1}.{T2}.Domain
  │       └─Tests
  │           └─{T1}.{T2}.Domain.Tests.Unit                      // 공유 도메인
  │
  │ # Backend 범주
  ├─Backend
  │   ├─{T3}
  │   │   ├─Src
  │   │   │   ├─{T1}.{T2}.{T3}                                  // Host
  │   │   │   ├─{T1}.{T2}.{T3}.Adapters.Infrastructure          // Adapter 레이어
  │   │   │   ├─{T1}.{T2}.{T3}.Adapters.Persistence             // Adapter 레이어
  │   │   │   ├─{T1}.{T2}.{T3}.Application                      // Application 레이어
  │   │   │   └─{T1}.{T2}.{T3}.Domain                           // Domain 레이어
  │   │   └─Tests
  │   │       ├─{T1}.{T2}.{T3}.Tests.Integration                // Integration 테스트
  │   │       ├─{T1}.{T2}.{T3}.Tests.Performance                // Performance 테스트
  │   │       └─{T1}.{T2}.{T3}.Tests.Unit                       // Unit Test
  │   ├─{T3}
  │   │   ├─Src
  │   │   └─Tests
  │   └─Tests
  │       └─{T1}.{T2}.Tests.E2E                                 // End to End 테스트
  │
  │ # Frontend 범주
  └─Frontend
      └─{T3}
          ├─Src
          │   ├─{T1}.{T2}.{T3}                                  // Host
          │   ├─{T1}.{T2}.{T3}.Adapters.Infrastructure          // Adapter 레이어
          │   ├─{T1}.{T2}.{T3}.Adapters.Persistence             // Adapter 레이어
          │   ├─{T1}.{T2}.{T3}.Application                      // Application 레이어
          │   └─{T1}.{T2}.{T3}.Domain                           // Domain 레이어
          └─Tests
              ├─{T1}.{T2}.{T3}.Tests.Integration                // Integration 테스트
              ├─{T1}.{T2}.{T3}.Tests.Performance                // Performance 테스트
              └─{T1}.{T2}.{T3}.Tests.Unit                       // Unit Test
```

## Ch 10.2 솔루션 구조 형식

| Level  | Src             | Tests            |
|------- |-------------    |--------------    |
| `{T1}` | Corporation     | Corporation      |
| `{T2}` | Solution        | Solution          |
| `T3`   | Service 또는 UI  | Service 또는 UI  |
| `T4`   | **Layers**      | **Tests**        |
| `T5`   | **Sub-Layers**  | **Test Pyramid** |

- Layers
  - Domain: 비즈니스 단위(Biz. Unit)
  - Application: 비즈니스 흐름(Biz. Flow)
  - Adapters: 기술 관심사
    - Infrastructure
    - Persistence
    - Presentation
- Test Pyramid
  - Unit
  - Integration
  - Performance
  - E2E(End to End)

## Ch 10.3 솔루션 구조 예제
![](./.images/SolutionExplorer.png)

- Src 예: `Corporation`.`Solution`.`Service`.`Adapters`.`Infrastructure`
    - T1: Corporation
    - T2: Solution
    - T3: Service
    - T4: Adapters
    - T5: Infrastructure
- Src 예: `Corporation`.`Solution`.`Service`.`Domain`
  - T5 생략일 때
- Src 예:`Service`.`Adapters`.`Infrastructure`
  - T1, T2 생략일 때
- Tests 예: `Corporation`.`Solution`.`Service`.`Tests`.`Unit`
  - T1: Corporation
  - T2: Solution
  - T3: Service
  - T4: Tests
  - T5: Unit
- Tests 예: `Service`.`Tests`.`Unit`
  - T1, T2 생략일 때

<br/>

# Ch 11. 솔루션 빌드 설정

## Ch 11.1 .NET SDK 빌드 버전
- `global-json` 파일은 .NET 프로젝트에서 특정 .NET SDK 버전을 지정하여 일관된 개발 환경을 유지하기 위해 사용됩니다.
  - 예제 코드: [global-json](./Ch09.SolutionBuildSettings/global.json)

```shell
# Host에 설치된 .NET SDK 목록
dotnet --list-sdks

# 템플릿 확인
dotnet new list | findstr nuget
  템플릿 이름          약식 이름                       언어     태그
  ------------------- -----------------------------  -------  -----------
  global.json 파일     globaljson,global.json                  Config

# globaljson 파일 생성
#  - 8.0.100 이상 8.0.xxx 버전(예: 8.0.303 또는 8.0.402)을 허용합니다.
dotnet new globaljson --sdk-version 8.0.100 --roll-forward latestFeature --force
#  - 8.0.100 이상 8.0.1xx 버전(예: 8.0.103 또는 8.0.199)을 허용합니다.
dotnet new globaljson --sdk-version 8.0.100 --roll-forward latestPatch --force
#  - 8.0.100 지정된 버전만을 사용합니다.
dotnet new globaljson --sdk-version 8.0.100 --roll-forward disable --force

# .NET SDK 빌드 버전 확인
dotnet --version
```

- .NET SDK 버전 형식: "[global.json](https://learn.microsoft.com/ko-kr/dotnet/core/tools/global-json)"에 지정된 버전에서부터 상위 버전(rollForward) 범위를 지정합니다.
  ```
  x.y.znn
  ```
  - `x`: major
  - `y`: minor
  - `z`: feature, 0 ~ 9
  - `n`: patch, 0 ~ 99

- `latestFeature` 예
  ```json
  {
    "sdk": {
      "version": "8.0.302",
      "rollForward": "latestFeature"
    }
  }
  ```
  - 8.0.302 이전의 모든 .NET SDK 버전을 허용하지 않으며 8.0.302 이상 8.0.xxx 버전(예: 8.0.303 또는 8.0.402)을 허용합니다.

- `latestPatch` 예
  ```json
  {
    "sdk": {
      "version": "8.0.102",
      "rollForward": "latestPatch"
    }
  }
  ```
  - 8.0.102 이전의 모든 .NET SDK 버전을 허용하지 않으며 8.0.102 이상 8.0.1xx 버전(예: 8.0.103 또는 8.0.199)을 허용합니다.
- `disable` 예
  ```json
  {
    "sdk": {
      "version": "8.0.102",
      "rollForward": "disable"
    }
  }
  ```
  - 8.0.102 지정된 .NET SDK 버전만을 허용하빈다.

## Ch 11.2 패키지 소스
- `nuget.config` 파일은 솔루션 수준에서 패키지 소스을 관리합니다.
  - 예제 코드: [nuget.config](./Ch09.SolutionBuildSettings/nuget.config)

```shell
# 템플릿 확인
dotnet new list | findstr nuget
  템플릿 이름        약식 이름                      언어     태그
  ---------------- -----------------------------  -------  ---------
  NuGet 구성        nugetconfig,nuget.config               Config

# 템플릿 파일 생성
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
- 전역 설정에 지정된 기존 NuGet 패키지 소스 목록을 모두 제거 후에 새 패키지 저장소 `https://api.nuget.org/v3/index.json`을 지정합니다.

## Ch 11.3 중앙 패키지 버전 관리
- `Directory.Package.props` 파일을 통해 각 프로젝트의 패키지 버전을 일일이 수정하지 않고, 한 곳에서 공통 패키지 버전을 정의할 수 있습니다.
  - 예제 코드: [Directory.Packages.props](./Ch09.SolutionBuildSettings/Directory.Packages.props)

```shell
# 도구 설치
dotnet tool install -g upgrade-assistant

# 도구 확인
dotnet tool list -g
  패키지 ID             버전            명령
  ------------------------------------------------------
  upgrade-assistant     0.5.820        upgrade-assistant

# 중앙 패키지 파일 생성
upgrade-assistant upgrade
```
![](./.images/upgrade-assistant.png)

![](./.images/Directory.Package.props.concept.png)

- 프로젝트 파일 변경 전/후
  ![](./.images/Directory.Package.props.csproj.png)
  - 프로젝트 파일에서 `PackageReference`의 `Version`을 제거 시킵니다.
- Directory.Package.props 변경 전/후
  ![](./.images/Directory.Package.props.png)
  - 프로젝트 파일에서 제거된 `PackageReference`의 `Version` 값을 `PackageVersion`으로 추가하여 버전을 중앙에서 관리합니다.

## Ch 11.4 중앙 빌드 속성 관리
- `Directory.Build.props` 파일을 사용하면 각 프로젝트 파일에 일일이 동일한 속성을 추가할 필요 없이, 한 곳에서 공통 속성을 정의하고 관리할 수 있습니다.
  - 예제 코드: 솔루션 빌드 속성 [Directory.Build.props](./Ch09.SolutionBuildSettings/Directory.Build.props)
  - 예제 코드: 테스트 빌드 속성 [Directory.Build.props](./Ch09.SolutionBuildSettings/Backend/Tests/Directory.Build.props)

```shell
# 전체 공통 빌드 속성
#   - 전체 프로젝트 대상 Directory.Build.props 파일 생성
.\new-buildprops.ps1

# 테스트 공통 빌드 속성
#   - .\Backend\Api\Tests\ 프로젝트 대상으로
#   - 상위 `Directory.Build.props`을 Import한 Directory.Build.props 파일 생성
.\new-buildprops.ps1 -t .\Backend\Api\Tests\ -i

# 테스트 공통 빌드 속성
#   - .\Backend\Master\Tests\ 프로젝트 대상으로
#   - 상위 `Directory.Build.props`을 Import한 Directory.Build.props 파일 생성
.\new-buildprops.ps1 -t .\Backend\Master\Tests\ -i
```
- new-buildprops.ps1 파일: [링크](./Templates/new-buildprops.ps1)

```shell
{T2}.sln
Directory.Build.props                                // 전역 프로젝트 공통 빌드 속성
  │
  ├─Backend
  │   ├─{T3}
  │   │   ├─Src
  │   │   │   ├─{T1}.{T2}.{T3}
  │   │   │   └─...
  │   │   └─Tests
  │   │       ├─Directory.Build.props                // 테스트 프로젝트 공통 빌드 속성
  │   │       ├─{T1}.{T2}.{T3}.Tests.Integration
  │   │       ├─{T1}.{T2}.{T3}.Tests.Performance
  │   │       └─{T1}.{T2}.{T3}.Tests.Unit
```
- 전역 프로젝트 공통 빌드 속성: 솔루션 파일(.sln)과 같은 경로에 있는 `Directory.Build.props` 파일은 전체 공통 빌드 속성을 정의합니다.
  ```xml
  <Project>

    <PropertyGroup>
      <TargetFramework>net8.0</TargetFramework>
      <ImplicitUsings>enable</ImplicitUsings>
      <Nullable>enable</Nullable>
    </PropertyGroup>

  </Project>
  ```
- 테스트 프로젝트 공통 빌드 속성: Tests 폴더에 있는 `Directory.Build.props` 파일은 Test 프로젝트 공통 빌드 속성을 정의합니다.
  ```xml
  <Project>
    <!--
      현재 파일의 위치에서 상위로 디렉터리를 거슬러 올라가면서 Directory.Build.props 파일을 찾고,
      해당 파일이 발견되면 프로젝트에 포함시키는 역할을 합니다.
    -->
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
- 테스트 Runner 설정: xunit.runner.json
  ```json
  {
    "$schema": "https://xunit.net/schema/current/xunit.runner.schema.json",
    "methodDisplay": "method",
    "diagnosticMessages": true
  }
  ```
- `Directory.Build.props` 적용 후
  - EXE 프로젝트 .csproj 파일
    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <OutputType>Exe</OutputType>
        <!--
        // 솔루션 폴더에 있는 Directory.Build.props 빌드 속성을 사용합니다.

        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        -->
      </PropertyGroup>
    </Project>
    ```
  - ClassLibrary 프로젝트 .csproj 파일
    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <!--
        // 솔루션 폴더에 있는 Directory.Build.props 빌드 속성을 사용합니다.

        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
        -->
      </PropertyGroup>
    </Project>
    ```
  - Test 프로젝트 .csproj 파일
    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <!--
        // 솔루션 폴더에 있는 Directory.Build.props 빌드 속성을 사용합니다.

        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>

        // Tests 폴더에 있는 Directory.Build.props 빌드 속성을 사용합니다.
        <IsPackable>false</IsPackable>
        <IsTestProject>true</IsTestProject>
        -->
      </PropertyGroup>
    </Project>
    ```

## Ch 11.5 버전 공유
- TODO

<br/>

# Ch 12. 솔루션 코드 분석

## Ch 12.1 코드 스타일 분석
```xml
<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
```

- `IDExxxx`
- `EnforceCodeStyleInBuild`: 코드 스타일 분석 활성화
  - [코드 스타일 규칙](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/)은 .NET 프로젝트 빌드 시 기본적으로 비활성화되어 있으므로, 이를 사용하려면 명시적으로 활성화해야 합니다.
- `TreatWarningsAsErrors`: 경고를 에러화

```shell
# 템플릿 확인
dotnet new list | findstr editor
  템플릿 이름          약식 이름                       언어     태그
  ------------------- -----------------------------  -------  ---------
  EditorConfig 파일    editorconfig,.editorconfig              Config

# 템플릿 파일 생성
dotnet new editorconfig
```

```ini
[*.{cs,vb}]
# - IDE0160: Use block-scoped namespace
# - IDE0161: Use file-scoped namespace
dotnet_diagnostic.IDE0161.severity = warning

# csharp_style_namespace_declarations = block_scoped
# csharp_style_namespace_declarations = file_scoped
csharp_style_namespace_declarations = file_scoped:warning
```
- `.editorConfig` 파일을 이용하여 코드 스타일을 정의할 수 있습니다. `.editorConfig`은 Visual Studio 옵션 대화 상자에 지정된 코드 스타일보다 우선합니다.
- 네임스페이스 규칙: [file_scoped](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161)

```cs
// -{EnforceCodeStyleInBuild}-> 경고 -{TreatWarningsAsErrors}-> 에러
namespace Crop.Hello.Api.Adapters.Infrastructure   // block-scoped
{
    public class Class1
    {

    }
}
```
```
error IDE0161:
 파일 범위 namespace 스로 변환 (https://learn.microsoft.com/dotnet/fundamentals/code-analysis/style-rules/ide0161)
```

## Ch 12.2 코드 품질 분석
```xml
<EnableNETAnalyzers>true</EnableNETAnalyzers>
<AnalysisLevel>latest</AnalysisLevel>
<AnalysisMode>All</AnalysisMode>
<CodeAnalysisTreatWarningsAsErrors>true</CodeAnalysisTreatWarningsAsErrors>
<WarningsNotAsErrors>$(WarningsNotAsErrors);CS8073;CS8882;CS8887;CS8848</WarningsNotAsErrors>
```
- `CAxxxx`: AnalysisMode가 .editorconfig보다 우선 순위가 높습니다.
- `EnableNETAnalyzers`: 코드 품질 분석 활성화
- `AnalysisLevel`: 코드 품질 분석 버전
- `AnalysisMode`: 코드 품질 분석 범위
- `CodeAnalysisTreatWarningsAsErrors`: AnalysisMode에서 검출된 코드 품질 분석 경고를 에러화(.editorconfig에서 검출된 경고를 에러화하지는 않는다)
- `WarningsNotAsErrors` 경고 무시

## Ch 12.3 코드 품질 지표
- TODO

<br/>

# Ch 13. 솔루션 아키텍처 테스트
![](./.images/Architecture.UnitTestStructure.png)

- Abstractions
  - 테스트를 위해 부가적으로 필요한 코드를 배치 시킵니다.
- ArchitectureTests
  - 아키텍처 테스트 코드를 배치합니다.

## Ch 13.1 레이어 어셈블리

```cs
using System.Reflection;

namespace Crop.Hello.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
```

- 모든 레이어 어셈블리(프로젝트)에 공통적으로 `AssemblyReference`을 구현합니다.
  ![](./.images/AssemblyReference.png)

## Ch 13.2 레이어 의존성 테스트
- [ArchUnitNET](https://github.com/TNG/ArchUnitNET) 패키지

```cs
using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

// ArchitectureTests/ArchitectureBaseTest.cs
public abstract class ArchitectureBaseTest
{
  // 레이어 어셈블리 집합
  protected static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(
      Adapters.Infrastructure.AssemblyReference.Assembly,
      Adapters.Persistence.AssemblyReference.Assembly,
      Application.AssemblyReference.Assembly,
      Domain.AssemblyReference.Assembly)
    .Build();

  // Adapter Infrastructure 레이어
  protected static readonly IObjectProvider<IType> AdapterInfrastructureLayer = ArchRuleDefinition
    .Types()
    .That()
    .ResideInAssembly(Adapters.Infrastructure.AssemblyReference.Assembly)
    .As("Adapters.Infrastructure");

  // Adapter Persistence 레이어
  protected static readonly IObjectProvider<IType> AdapterPersistenceLayer = ArchRuleDefinition
    .Types()
    .That()
    .ResideInAssembly(Adapters.Persistence.AssemblyReference.Assembly)
    .As("Adapters.Persistence");

  // Application 레이어
  protected static readonly IObjectProvider<IType> ApplicationLayer = ArchRuleDefinition
    .Types()
    .That()
    .ResideInAssembly(Application.AssemblyReference.Assembly)
    .As("Application");

  // Domain 레이어
  protected static readonly IObjectProvider<IType> DomainLayer = ArchRuleDefinition
    .Types()
    .That()
    .ResideInAssembly(Domain.AssemblyReference.Assembly)
    .As("Domain");
}
```
- ArchLoader을 통해 검증을 수행할 전체 어셈블리를 구성합니다.
- ArchRuleDefinition으로 개별 어셈블리을 정의합니다.

```cs
// Abstractions/Constants/Constants.UnitTest.cs
internal static partial class Constants
{
  public static class UnitTest
  {
    public const string Architecture = nameof(Architecture);

    public const string Infrastructure = nameof(Infrastructure);
    public const string Persistence = nameof(Persistence);
    public const string Presentation = nameof(Presentation);
    public const string Application = nameof(Application);
    public const string Domain = nameof(Domain);
  }
}

// ArchitectureTests/LayerDependencyTests.cs
[Trait(nameof(UnitTest), UnitTest.Architecture)]
public class LayerDependencyTests : ArchitectureBaseTest
{
  [Fact]
  public void DomainLayer_ShouldNotHave_Dependencies_OnAnyOtherLayer()
  {
    IObjectProvider<IType>[] layers = [
      AdapterInfrastructureLayer,
      AdapterPersistenceLayer,
      ApplicationLayer
    ];

    foreach (var layer in layers)
    {
      ArchRuleDefinition
        .Types()
        .That()
        .Are(DomainLayer)
        .Should()
        .NotDependOnAny(layer)
        .Check(Architecture);
    }
  }
```

- 레이어 의존성 테스트
  - DomainLayer_ShouldNotHave_Dependencies_OnAnyOtherLayer
  - ApplicationLayer_ShouldNotHave_Dependencies_OnAdapterLayer
  - AdapterLayer_ShouldNotHave_Dependencies_OnDomainLayer

## Ch 13.3 CQRS 네이밍 컨벤션 테스트

```cs
[Fact]
public void CommandMessages_ShouldEndWith_Command()
{
  var suts = ArchRuleDefinition
    .Classes()
    .That()
    .ImplementInterface(typeof(ICommand));

  if (!suts.GetObjects(Architecture).Any())
    return;

  // public sealed recoard XyzCommand : ICommand { }
  suts.Should().BePublic()
    .AndShould().BeSealed()
    .AndShould().BeRecord()
    .AndShould().HaveNameEndingWith(NamingConvention.Command)
    .Check(Architecture);
}
```

- CQRS 테스트
  - CommandMessages_ShouldEndWith_Command
  - CommandMessagesT_ShouldEndWith_Command
  - CommandUseCases_ShouldEndWith_CommandUsecase
  - CommandUseCasesT_ShouldEndWith_CommandUsecase
  - QueryMessagesT_ShouldEndWith_Query
  - QueryUseCasesT_ShouldEndWith_QueryUsecase

## Ch 13.4 레이어 의존성 다이어그램

![](./.images/Architecture.LayerDiagram.png)

```shell
dotnet tool install -g DependencyVisualizerTool --version 1.0.0-beta.3
dotnet tool list -g

DependencyVisualizer .\Backend\Api\Src\Crop.Hello.Api\Crop.Hello.Api.csproj --projects-only
```

<br/>

# Ch 14. 솔루션 레이어 의존성 주입

## Ch 14.1 폴더 구성

---

<br/>

# Part 4. Internal 전술 설계

# Ch 23. 전술 설계 맵
![](./.images/TacticalDesign.Pattern.png)

<br/>

# Ch 24. 출력 기본 타입
- IResult 타입으로 모든 Known과 Unknown 입출력 메서드 결과 타입으로 정의합니다.

## Ch 24.1 IResult 타입 정의
- 성공과 실패를 구분하며, 성공 시에는 값을 가지고, 실패 시에는 에러 값을 포함합니다.
- 특히, 유효성 검사 실패의 경우 다수의 에러 값을 정의할 수 있습니다.

```cs
// IResult/IResult<out TValue> 타입
public interface IResult
{
  bool IsSuccess { get; }
  bool IsFailure { get; }
  Error Error { get; }
}

public interface IResult<out TValue>
  : IResult
{
  TValue Value { get; }
}

// IValidationResult 타입
public interface IValidationResult
{
  Error[] ValidationErrors { get; }
}

public sealed class ValidationResult
  : Result
  , IValidationResult
{ }

public sealed class ValidationResult<TValue>
  : Result<TValue>
  , IValidationResult
{ }

// Error 타입
public sealed partial record class Error(string Code, string Message)
```

- `IResult/IResult<TValue>`
  - 생성
    - 성공
      - 값이 없을 때: Success()
      - 값이 있을 때: Success\<TValue\>(TValue value)
    - 실패
      - 값이 없을 때: Failure(Error error)
      - 값이 있을 때: Failure\<TValue\>(Error error)
      - 값이 있을 때: Failure\<TValue\>()
  - 타입 변환: 실패일 때 & 값이 있을 때
    - ValidationResult\<TValue\> ToValidationResult\<TValue\>()
    - ValidationResult\<TValue\> ToValidationResult()
- `ValidationResult/ValidationResult<TValue>`
  - 생성
    - 성공
      - 값이 없을 떄: WithoutErrors()
      - 값이 있을 때: WithoutErrors(TValue? value)
    - 실패
      - 값이 없을 때: WithErrors(params Error[] validationErrors)
      - 값이 있을 때: WithErrors(params Error[] validationErrors)
      - 값이 없을 때: WithErrors(ICollection\<Error\> validationErrors)
      - 값이 있을 때: ???
  - 타입 변환
    - none
- `Error`
  - 생성
    - New(string code, string message)
    - FromException\<TException\>(TException exception)
  - 타입 변환
    - string
      - 암시적(Code): operator
      - 명시적(Message): ToString()
    - ValidationResult
      - ToValidationResult()
      - ToValidationResult\<TValue\>()
    - Result
      - ToResult()
      - ToResult\<TValue\>()
