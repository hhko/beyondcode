
## TODO

Usecase         | Service
---             | ---
Profiles        | UserManagement/Profile
Authentication  | UserManagement/Authentication
Gyms            |
Participants    |
Admins          |
Reservations    |
Rooms           |
Sessions        |
Subscriptions   |
Trainers        |


## 2025-06-13(금)
- Authentication Controller
- Register WebApi 디버깅
- Login WebApi 디버깅
- Hash 이해
- WebApi Authentication 이해
---
- Getting DDD 디버깅 환경 구축
- UserManagement 서비스 이해
- WebApi 이해
- DB 처리 이해
---
- UserManagement Persistance 구현
- Persistance 개선
  - Command: EF Core
  - Query: Dapper
- DB 업데이트 자동화
- ER 다이어그램
---
- 중첩 에러???
- Entity Guid -> EntityId


## TODO
- CQRS 메시지
- Error
- 함수형
- Value Object 생성: Create, Validate(pipeline 통합???)
- Entity 생성: Create
---
- 과제 목차 재구성
---
- pipeline Fin 통합
---
- 관찰 가능성
- 로그: Error
- 지표: Kafka Stream 시간별, 월별, ...? KQL
- 추적: IAdapter 인터페이스 소스 생성기
- 추적 패키지
  - Quartz
  - EFCore
  - Dapper?
  - FTP?
  - RabbitMQ
  - File System
- Audit 로그?
---
- 컨테이너화
- 이벤트 분리
  - 로컬 이벤트
  - 원격 이벤트
---
- 패턴 Specification
- 패턴 Unit of Work
- 패턴 Outbox
- 회복력
---
- 성능 테스트
- 통합 테스트 WebApi
- 통합 테스트 WebApi + DB
---
- Aspire
- Dapper
---


## IAdapter 소스 생성기
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


## 배움
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

<br/>

## 사이트
- 튜토리얼 https://fsharpforfunandprofit.com/series/designing-with-types/
- 튜토리얼 https://github.com/swlaschin/DmmfWorkshop
- 튜토리얼 eShop
- 튜토리얼 https://github.com/backstage/backstage 개발 포탈 사이트
- 튜토리얼 https://github.com/moghtech/komodo 배포
- 튜토리얼 openfeature
- 튜토리얼 https://github.com/kgrzybek/hotels-manager/tree/main