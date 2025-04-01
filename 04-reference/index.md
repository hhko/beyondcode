---
outline: deep
---

# 참고 자료료

## 도전
- [x] Youtube | [꿈을 현실로 만든 10년의 여정 | 이승건 토스팀 리더](https://www.youtube.com/watch?v=UogHHGN3U3Q)

## 객체지향
- [x] Youtube | [[통합본] 쉽게 설명하는 객체 지향 프로그래밍의 본질 1-3편](https://www.youtube.com/watch?v=zgeCwYWzK-k)
  ```
  OOP to me means only messaging, local retention and protection and hiding of state-process, and extreme late-binding of all things.
  - 메시징
  - 상태 데이터의 캡슐화
  - 동적 바인딩
  ```

## 도메인 주도 설계
- [x] Youtube | [Moving IO to the edges of your app: Functional Core, Imperative Shell - Scott Wlaschin](https://www.youtube.com/watch?v=P1vES9AgfC4)
  ```
  - 불순(Impure: IO)과 순수(Pure) 함수를 구분합니다.
  - 불순 함수를 외곽에 배치합니다.
  ```
- [x] Youtube | [개발 아키텍처에서 말하는 비즈니스 로직•도메인 로직 한 방에 이해하기](https://www.youtube.com/watch?v=gbzDG_2XQYk)
  ```
  현실 문제에 대한 의사 결정을 하고 있는가?
    - 비즈니스 규칙을 기반으로 의사 결정을 수행하는가?
    - 도메인 고유의 정책과 규칙을 적용하여 판단을 내리는가?
    - 단순한 데이터 입출력이 아니라 비즈니스 규칙을 반영하는가?
  ```
- [x] Youtube | [DDD 그리고 MSA](https://www.youtube.com/watch?v=DOpt6IWU6LU)
  ```
  DDD 역사
  ```
- [x] Youtube | [소프트웨어 설계를 위한 추상적, 구조적 사고│인프콘2023](https://www.youtube.com/watch?v=EgLxbFmPRoU)
  ```
  - 도메인: 우리가 해결할 '문제'에 대한 비즈니스 전문 지식
  - 도메인 모델링: 비즈니스를 프로그램 세계로 표현한 것
  ```
- [ ] Youtube | [Navigating complexity in event-driven architectures: A domain-driven approach](https://www.youtube.com/watch?v=HpFWRpyyvrk)
  ```
  ```
- [x] Youtube | [우리는 이렇게 모듈을 나눴어요: 멀티 모듈을 설계하는 또 다른 관점 | 인프콘2023](https://www.youtube.com/watch?v=uvG-amw2u2s)
  ```
  - Domain과 Infra 영역은 다른 수준(Level)
    = 다른 변경의 속도
      - Domain: 비즈니스 요구사항에 의해 변경
      - Infra: 기능 요구사항에 의해 변경
    = 다른 성질(순수성 vs. 비순수성)
  - 좋은 시스템은 제약이 많은 시스템
  - A good architecture allows you to defer critical decisions.
    좋은 아키텍처는 중요한 결정을 미룰 수 있도록 해준다.
  - 신뢰 자본
  ```
- [ ] GitHub | [contextive](https://github.com/dev-cycles/contextive)
- [ ] GitHub | https://github.com/dotnet-presentations/eshop-app-workshop/tree/main: eShop
- [ ] GitHub | https://github.com/dotnet/eshop
- [ ] GitHub | https://github.com/henriquelourente/Domain-Driven-Design-Sample: DDD 예제
- [ ] GitHub | https://github.com/kgrzybek/hotels-manager/tree/main: DDD 예제
- [ ] GitHub | **[ing-ddd](https://github.com/kgrzybek/ing-ddd)**
- [ ] GitHub | **[ddd-guestbook](https://github.com/ardalis/ddd-guestbook)**
- [ ] GitHub | [ValueObjectsDemo](https://github.com/ardalis/ValueObjectsDemo)

## 아키텍처
- [ ] Blog | [1/5 Hexagonal Architecture – What Is It? Why Should You Use It?](https://www.happycoders.eu/software-craftsmanship/hexagonal-architecture/): hexagonal-architecture 체계적 설명, dto 경우의 수 표현, 클린아키텍처와 비교
- [ ] Blog | [2/5 Hexagonal Architecture with Java – Tutorial](https://www.happycoders.eu/software-craftsmanship/hexagonal-architecture-java/)
- [ ] Blog | [3/5 Ports and Adapters Java Tutorial: Adding a Database Adapter](https://www.happycoders.eu/software-craftsmanship/ports-and-adapters-java-tutorial-db/)
- [ ] Blog | [4/5 Hexagonal Architecture With Quarkus](https://www.happycoders.eu/software-craftsmanship/hexagonal-architecture-quarkus/)
- [ ] Blog | [5/5 Hexagonal Architecture with Spring Boot](https://www.happycoders.eu/software-craftsmanship/hexagonal-architecture-spring-boot/)
- [ ] Blog | [Moving IO to the edges of your app: Functional Core, Imperative Shell - Scott Wlaschin](https://www.youtube.com/watch?v=P1vES9AgfC4)
- [ ] Blog | [Exposing the not-so-secret practices of the cult of DDD](https://www.youtube.com/watch?v=ESPnfFT6iD0)

## 개발 방법론
- [x] Youtube | [클린 스프링: 스프링 개발자를 위한 클린코드 전략 | 인프콘2024](https://www.youtube.com/watch?v=d3krJ4el8Hg)
  ```
  클린 코드를 지향할 수록 점점 구현능력이 떨어진다.
    - that works가 없다.
    - 리팩토링 선순환 기간이 너무 길다.

  clean code that works
  ```

## 리팩토링
- [ ] Youtube | [점진적 추상화 | 인프콘2023](https://www.youtube.com/watch?v=dzDCToa0XNg)
  ```
  추상화 범위
    - 타입 확장 지향: DTO
    - 행위 확장 지향: 인터페이스
  ```

## 단위 테스트
### 아키텍처
- [x] Youtube | [Bulletproof Your Software Architecture With ArchUnitNET](https://www.youtube.com/watch?v=R_srbvA6IQM)
  ```
  ArchUnitNET 패키지를 이용한 프로젝트 참조 테스트
  ```
- [x] [Unit Test your Architecture (and more) with ArchUnit](https://goatreview.com/unit-test-architecture-with-archunit/amp/)

### 설정
- [ ] [Configure unit tests by using a .runsettings file](https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022)

### 코드 커버리지
- [x] [Code Coverage Reports for .NET Projects](https://knowyourtoolset.com/2024/01/coverage-reports/)


## 통합 테스트
### WebApi
- [ ] GitHub | [IntegrationTests ASP.NET v9 Sample](https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/test/integration-tests/9.x/IntegrationTestsSample)
- [x] Blog | [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-9.0)
- [x] Blog | [Integration Testing in ASP.NET Core](https://code-maze.com/aspnet-core-integration-testing/)
- [ ] Blog | [Integration Testing for ASP.NET APIs (1/3) - Basics](https://knowyourtoolset.com/2024/01/integration-testing/)
- [ ] Blog | [Integration Testing for ASP.NET APIs (2/3) - Data](https://knowyourtoolset.com/2024/01/integration-testing-data/)
- [ ] Blog | [Integration Testing for ASP.NET APIs (3/3) - Auth](https://knowyourtoolset.com/2024/01/integration-testing-auth/)

## IOption
- [x] Youtube | [The Best Way to Validate Your Settings in .NET](https://www.youtube.com/watch?v=jblRYDMTtvg)
  ```
  IValidateOptions<T>   // .NET
  IValidator<T>         // Fluent Validation

  var config = builder.Configuration;

  builder.Services
    // Case 1.
    .AddOptions<T>()
    .Bind(config.GetSection({T}.ConfigurationSectionName))

    // Case 2.
    .BindConfiguration({T}.ConfigurationSectionName)

    ...
    .ValidateOnStart();
  ```
- [x] Youtube | [Easily Validate the Options Pattern with FluentValidation](https://www.youtube.com/watch?v=I0YPTeCYvrE)
- [ ] Blog | [Adding validation to strongly typed configuration objects using FluentValidation](https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/)

## 아키텍처 템플릿
- [ ] GitHub | **[SSW.CleanArchitecture](https://github.com/SSWConsulting/SSW.CleanArchitecture)**
- [ ] GitHub | **[ardalis | CleanArchitecture](https://github.com/ardalis/CleanArchitecture)**
- [ ] GitHub | **[ardalis | CleanArchitecture.WorkerService](https://github.com/ardalis/CleanArchitecture.WorkerService/tree/main)**
- [ ] GitHub | **[amantinband | clean-architecture](https://github.com/amantinband/clean-architecture)**
- [ ] GitHub | https://github.com/samanazadi1996/Sam.CleanArchitecture: Localization, Functional Tests
- [ ] GitHub | https://github.com/babaktaremi/Clean-Architecture-Template : 도커 self-signed SSL certificate
- [ ] GitHub | https://github.com/ivanpaulovich/dotnet-new-caju: dotnet template 조건
- [ ] GitHub | https://github.com/Genocs/clean-architecture-template: worker
- [ ] GitHub | https://github.com/Hona/VerticalSliceArchitecture
- [ ] GitHub | https://github.com/Hona/VerticalSliceArchitecture.Samples.Todos
- [ ] GitHub | https://github.com/stphnwlsh/CleanMinimalApi: MinimalWebApi

## 강의 소스
- [ ] GitHub | https://github.com/Dometrain/getting-started-domain-driven-design
- [ ] GitHub | https://github.com/Dometrain/deep-dive-domain-driven-design
- [ ] GitHub | https://github.com/kgrzybek/ing-ddd
---
- [ ] GitHub | https://github.com/Dometrain/from-zero-to-hero-open-telemetry-in-dotnet
---
- [ ] GitHub | https://github.com/Dometrain/from-zero-to-hero-vertical-slice-architecture
- [ ] GitHub | https://github.com/Dometrain/zero-to-hero-event-driven-architecture
---
- [ ] GitHub | https://github.com/Dometrain/from-zero-to-hero-test-driven-development-tdd-csharp
- [ ] GitHub | https://github.com/Dometrain/from-zero-to-hero-clean-code-with-csharp
- [ ] GitHub | https://github.com/Dometrain/from-zero-to-hero-asynchronous-programming-in-csharp
- [ ] GitHub | https://github.com/ardalis/ddd-guestbook

```
## Result
- [ ] [Part 1 - Replacing Exceptions-as-flow-control with the result pattern](https://andrewlock.net/working-with-the-result-pattern-part-1-replacing-exceptions-as-control-flow/)
- [ ] [Part 2 - Safety and simplicity with LINQ](https://andrewlock.net/working-with-the-result-pattern-part-2-safety-and-simplicity-with-linq/)
- [ ] [Part 3 - Adding more extensions to Result<T>](https://andrewlock.net/working-with-the-result-pattern-part-3-adding-more-extensions/)
- [ ] [Part 4 - Is the result pattern worth it?](https://andrewlock.net/working-with-the-result-pattern-part-4-is-the-result-pattern-worth-it/)

## 컨테이너
### Health Check
- [ ] Blog | [Mastering Custom Health Checks in ASP.NET Core for Robust and Reliable Applications](https://programmingpulse.vercel.app/blog/mastering-custom-health-checks-in-aspnet-core)
```