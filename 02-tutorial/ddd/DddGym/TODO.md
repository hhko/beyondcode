## 규칙
- [ ] 전역 버전(docker 이미지 버전 포함)
- [x] global.json
- [ ] nuget.config
- [ ] Directory.Build.props
  - 정적 분석
- [ ] Directory.Package.props
---
- [ ] GitHub Actions
- [ ] 클래스 다이어그램
- [ ] ER 다이어그램
- [ ] 프로젝트 의존성 다이어그램
- [ ] 정적 분석?
- [ ] 코드 커버리지
- [ ] 컨테이너 배포
---
- [x] 클래스 internal sealed
---
- [ ] 생성사 private
- [ ] 생성 Create
- [ ] ValueObject 생성
   - Error.Empty.If(조건, 에러 코드)
   - Error CreateValueObject
---
- [ ] 로컬 function
   - https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/local-functions
   - https://www.geeksforgeeks.org/local-function-in-c-sharp/
- [ ] Apply 함수 1개(로컬 function -{x}-> 로컬 function)
- [ ] 순수 함수: `A -> B`
- [ ] 부수 효과 함수: `A -> Fin<B>` (실패가 존재하지 않아도)
---
- [ ] 에러 코드: {레이어Errors}.{대상Errors}.{에러_원인}
- [ ] 에러 코드 생성: ErrorCodeFactory.Create
- [ ] 에러 코드 포맷: ErrorCodeFactory.Format
- [ ] 에러 추가: `Fin<T> .CombinErrors`
---
- [ ] 이벤트 이름
- [ ] 이벤트 출처 확인
---
- [ ] Ensure 참조건
   ```
   거짓조건
		? 참(에러)
		: 거짓(성공)		// unit
   ```
---
- [ ] 결과 Application 레이어 ToResponse 
- [ ] 결과 Adapter ...ToOkResult, ToProblemHttpResult 
---
- [ ] appsettings
---
- [ ] 의존성 등록: 클래스 이름/메서드 이름
- [ ] 인터페이스 기준 클래스 등록 자동화
---
- [ ] 테스트 코드 범주화


## 할일

### 할일 1. 함수화
  - [x] 에러 클래스 분리
  - [x] 이벤트 클래스 분리
  - [ ] https://github.com/dev-cycles/contextive 용어집
  - [ ] Ensure -> Validate
  - [x] ? -> Option

### 할일 2. WebApi 함수화
- [x] User Contoller 클래스
- [x] User Controller 통합 테스트 기본 구현
- [x] Bogus 기반 Fake 데이터 생성
  ```
  var faker = new Faker<User>()
            .CustomInstantiator(f => User.Create( ... );
  var fakerUser = faker.Generate();
  ```
- [x] 인터페이스 기준 의존성 등록 개선
- [x] CreateProfile 3개 함수 구현
- [x] CreateProfile 3개 테스트 함수 구현
- [x] Monad Transformers 필요성 이해: from 구문에서 await 2개 사용할 때
- [x] Application Response DTO 반환 확장 메서드: To{Response 클래스}();, {AggregateRoot}Mapping.cs
- [x] Adapters.Presentation WebApi Results 반환 확장 메서드: ToResult();
- [x] WebApi 결과 Json 구조 개선
  ```
  [                        <- 불 필요
    {
      "adminId": [
        "ae5ec89d-7c3a-46b3-bbb8-5c29acc08e17"
      ]
    }
  ]
  ```
- [ ] WebApi 실패 결과 Json  변환 에러
  ```
  The collection type 'LanguageExt.Option`1[System.Guid]' is abstract, an interface, or is read only, and could not be instantiated and populated. Path: $.adminId | LineNumber: 0 | BytePositionInLine: 12.'
  ```
- [ ] Exception 호스트 에러

### 할일 3. 로그인 인증
- [ ] User Register
- [ ] User Login

### 할일 4. 함수화 Transformer
- [ ] 실패 처리(컴파일러 에러)
  ```cs
  return from response in await Sender.Send(new CreateAdminProfileCommand(userId))
       select Ok(response);
  ```
- [ ] FinT<Task, Guid>???: from 구문에서는 첫 번째 from에서만 await을 사용할 수 있다.
  ```
  Fin<User> userResult = await _usersRepository.GetByIdAsync(request.UserId);	// 첫번째 await
  if (userResult.IsFail)
  {
      return (Error)userResult;
  }
  User user = (User)userResult;

  Fin<Guid> adminResult = user.CreateAdminProfile();
  if (adminResult.IsFail)
  {
      return (Error)adminResult;
  }
  Guid adminId = (Guid)adminResult;

  await _usersRepository.UpdateAsync(user);				// 두번째 await

  return new CreateAdminProfileResponse(adminId);
  ```

<br/>
<br/>
<br/>


### 할일 5. 데이터베이스(CQRS)
- [ ] EFCore SQL
- [ ] Dapper 통합 연동

### 할일 6. User 외 구현(DDD 이해)
- [ ] Application -> Domain 연동 이해
  -	Domain 테스트 코드
  - Application 테스트 코드
- [ ] 이벤트 활용 방법 학습
- [ ] Repository 활용 방법 학습
---
- [ ] ValueObject 코딩 규칙 테스트
- [ ] Entity 기본 구현
- [ ] Entity 코딩 규칙 테스트
- [ ] AggregateRoot 기본 구현
- [ ] Specification???

### 할일 7. 시나리오 테스트
- [ ] Reqnroll 테스트

### 할일 8. OpenTelemetry
- [ ] Pipeline
  - 유효성 검사
  - 도메인 Validate 메서드를 이용한 파이프라인 Validation
  - OpenTelemetry 로그
  - OpenTelemetry 추적
  - OpenTelemetry 지표
  - 예외
  - 트랜잭션? 시점
  - 캐시
- [ ] Audit

<br/>
<br/>
<br/>

### 할일 9. 소스 생성기
- [ ] Id 타입
- [ ] IAdapter Pipeline

### 할일 10. RabbitMQ

### 할일 11. 컨테이너
- [ ] Container HealthCheck

### 할일 12. 스케줄러 통합 테스트

### 할일 13. Adapter
- [ ] 회복력 adapter 레이어

### 할일 14. 기타
- [ ] https://github.com/backstage/backstage 개발 포탈 사이트
- [ ] https://github.com/moghtech/komodo 배포
- [ ] openfeature

### 패턴
- [x] Factory: Value Object
- [ ] Event
- [ ] Repository
- [ ] Unit of Work
- [ ] Outbox
- [ ] Specification

<br/>

## 고민
- 메서드 이름
  - HasReservationForParticipant(Guid participantId)
  - HasReservationBy(Guid participantId)
- 생성 이름 구분
  - New			// 자동(외부): EFCore ???
  - Create		// 직접
- 이벤트 ?
  - 데이터 전달 대상: id, 객체?
  - 시점: 객체 생성?, 데이터 저장?

<br/>

## PR
- dailySession
- Session에서 `List<T> -> IReadOnlyList<T>`
- Loing과 Register 변경

<br/>

## IAdapter 파이프라인 소스 생성기
```cs
// https://chatgpt.com/c/67ed3c20-6f64-800f-9bc0-e10aed627fdc
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public abstract class ConsoleApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    private IHost _host;
    
    public IHost Host => _host ?? throw new InvalidOperationException("Host has not been initialized.");
    
    public ConsoleApplicationFactory()
    {
        _host = CreateHostBuilder().Build();
        ConfigureConsoleApp(_host.Services);
    }
    
    protected virtual IHostBuilder CreateHostBuilder()
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureAppConfiguration((context, config) =>
        {
            ConfigureConfiguration(context.HostingEnvironment, config);
        });
        
        builder.ConfigureServices((context, services) =>
        {
            ConfigureServices(context, services);
        });
        
        return builder;
    }
    
    protected virtual void ConfigureConfiguration(IHostEnvironment environment, IConfigurationBuilder configuration)
    {
        // 기본적으로 환경설정 파일을 로드
        configuration.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                     .AddEnvironmentVariables();
    }
    
    protected virtual void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // 기본 서비스 설정 (필요시 오버라이드)
    }
    
    protected virtual void ConfigureConsoleApp(IServiceProvider services)
    {
        // 하위 클래스에서 서비스 활성화 로직 등을 조정 가능
    }
    
    public IServiceProvider Services => _host.Services;
    
    public void Dispose()
    {
        _host?.Dispose();
    }
}

// 실제 콘솔 애플리케이션 코드
public class Program
{
    public static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IMyService, MyService>();
            })
            .Build();

        var service = host.Services.GetRequiredService<IMyService>();
        Console.WriteLine(service.GetMessage());
    }
}

public interface IMyService
{
    string GetMessage();
}

public class MyService : IMyService
{
    public string GetMessage() => "Hello from MyService!";
}

// 통합 테스트 코드
using Xunit;

public class ConsoleApplicationTests : IDisposable
{
    private readonly ConsoleApplicationFactory<Program> _factory;

    public ConsoleApplicationTests()
    {
        _factory = new TestConsoleApplicationFactory();
    }

    public void Dispose()
    {
        _factory.Dispose();
    }

    [Fact]
    public void Service_ShouldReturnMockMessage()
    {
        var service = _factory.Services.GetRequiredService<IMyService>();
        Assert.Equal("Hello from MockService!", service.GetMessage());
    }
}

public class TestConsoleApplicationFactory : ConsoleApplicationFactory<Program>
{
    protected override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddSingleton<IMyService, MockService>();
    }
}

public class MockService : IMyService
{
    public string GetMessage() => "Hello from MockService!";
}
```

```cs
// https://gemini.google.com/app/f9085dae29663bc2
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

public class ConsoleApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public ConsoleApplicationFactory()
    {
        var services = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder();

        // 기본 설정 로드
        configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        // 하위 클래스에서 설정 재정의
        ConfigureConfiguration(configurationBuilder);

        _configuration = configurationBuilder.Build();

        // 서비스 컬렉션에 설정 추가
        services.AddSingleton(_configuration);

        // 하위 클래스에서 서비스 컬렉션 재정의
        ConfigureServices(services);

        _serviceProvider = services.BuildServiceProvider();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        // 기본 서비스 등록 (예: TEntryPoint 등록)
        services.AddSingleton<TEntryPoint>();
    }

    protected virtual void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
    {
        // 기본 설정 재정의 (예: 환경 변수 추가)
        configurationBuilder.AddEnvironmentVariables();
    }

    public async Task RunAsync(string[] args)
    {
        var entryPoint = _serviceProvider.GetRequiredService<TEntryPoint>();
        await InvokeEntryPointAsync(entryPoint, args);
    }

    protected virtual async Task InvokeEntryPointAsync(TEntryPoint entryPoint, string[] args)
    {
        // TEntryPoint의 Main 메서드 호출 (반사 사용)
        var mainMethod = typeof(TEntryPoint).GetMethod("Main", new[] { typeof(string[]) });
        if (mainMethod != null)
        {
            await (Task)mainMethod.Invoke(entryPoint, new object[] { args });
        }
        else
        {
            throw new InvalidOperationException("TEntryPoint에 Main 메서드가 없습니다.");
        }
    }

    public T GetService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public IConfiguration Configuration => _configuration;
}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class Program
{
    private readonly IConfiguration _configuration;

    public Program(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static async Task Main(string[] args)
    {
        var program = new Program(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());
        await program.Run(args);
    }

    public async Task Run(string[] args)
    {
        Console.WriteLine("Hello, Console Application!");
        Console.WriteLine($"Configuration Value: {_configuration["MySetting"]}");
        await Task.CompletedTask;
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

public class ProgramTests : ConsoleApplicationFactory<Program>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // 테스트를 위한 서비스 재정의 (예: Mock 서비스 등록)
        base.ConfigureServices(services);
        // services.AddSingleton<IMyService, MockMyService>();
    }

    protected override void ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
    {
        // 테스트를 위한 설정 재정의 (예: 메모리 설정 추가)
        base.ConfigureConfiguration(configurationBuilder);
        configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            { "MySetting", "TestValue" }
        });
    }

    [Fact]
    public async Task Run_ShouldPrintHelloWorld()
    {
        // Arrange
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        await RunAsync(new string[] { });

        // Assert
        Assert.Contains("Hello, Console Application!", consoleOutput.ToString());
        Assert.Contains("Configuration Value: TestValue", consoleOutput.ToString());
    }
}
```

## 학습
### 공변성
- Covariant: 공변성
  - `co` : 타입 계층 구조에서 같은 방향(하위 → 상위)으로의 변환이 허용되는 것
- Contravariant: 반공변성
  - `contra`: 타입 계층 구조에서 반대 방향(상위 → 하위)으로의 변환이 허용되는 것
- Invariant: 무공변
  - `in`:  타입 간 형변환을 허용하지 않음

### 모노이드
- 결합 법칙이 성립하는 이항 연산(Binary Operation)과 항등원(identity element)을 갖는 구조입니다. 
  - 폐쇄성 (Closure): a • b ∈ M
    - 두 원소 a, b가 집합 M에 속해 있다면, 이 둘을 연산 ⊕으로 결합한 결과도 반드시 M에 속해야 한다.
	- 폐쇄성이 없으면, 연산을 반복할 때 일관성이 없어서 프로그램 로직이 깨질 수 있습니다.
  - 결합법칙 (Associativity): (a • b) • c = a • (b • c)
  - 항등원 (Identity): a • e = e • a = a

### 모나드
- **모나드(Monad)**는 컨텍스트(Context)를 가진 값을 다루는 **연산 규약(패턴)**입니다.
  - 값을 **랩(wrap)**하거나 **연산을 체이닝(bind)**하는 연산을 정의
  - 합성 함수을 안전하게 연결해주는 도구
  - 연산의 순서를 제어하고 부작용을 추상화함

### TODO  
- value task vs task
- 험블객체
- mock, ...


