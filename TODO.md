## 규칙
1. 전역 버전(docker 이미지 버전 포함)
1. global.json
1. nuget.config
1. Directory.Build.props
1. Directory.Package.props
---
1. 클래스 sealed
1. 클래스 상속
---
1. 생성사 private
1. 생성 Create
1. ValueObject 생성
   - Error.Empty.If(조건, 에러 코드)
   - Error CreateValueObject
---
1. 로컬 function
   - https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/local-functions
   - https://www.geeksforgeeks.org/local-function-in-c-sharp/
1. Apply 함수 1개(로컬 function -{x}-> 로컬 function)
1. 순수 함수: `A -> B`
1. 부수 효과 함수: `A -> Fin<B>` (실패가 존재하지 않아도)
---
1. 에러 코드: {레이어Errors}.{대상Errors}.{에러_원인}
1. 에러 코드 생성: ErrorCodeFactory.Create
1. 에러 코드 포맷: ErrorCodeFactory.Format
1. 에러 추가: `Fin<T> .CombinErrors`
---
1. 이벤트 이름
1. 이벤트 출처 확인인
---
1. Ensure 참조건
   ```
   거짓조건
		? 참(에러)
		: 거짓(성공)		// unit
   ```
---
1. appsettings
---
1. 의존성 등록
---
1. 테스트 코드 범주화


## 할일
### 할일 1.
- [x] 에러 클래스 분리
- [x] 이벤트 클래스 분리
- [ ] https://github.com/dev-cycles/contextive 용어집
- [ ] Ensure -> Validate
- [ ] ? -> Option

### 할일 2.
- [ ] User Register
- [ ] User Login
- [ ] Application -> Domain 연동 이해
  -	Domain 테스트 코드
  - Application 테스트 코드

### 할일 3.
- [ ] Reqnroll 테스트

### 할일 4.
- ] ] ValueObject 코딩 규칙 테스트
- ] ] Entity 기본 구현
- ] ] Entity 코딩 규칙 테스트
- ] ] AggregateRoot 기본 구현

### 할일 5.
RabbitMQ 연동

### 할일 6.
- [ ] Pipeline
  - 유효성 검사
  - 도메인 Validate 메서드를 이용한 파이프라인 Validation
  - OpenTelemetry 로그
  - OpenTelemetry 추적
  - OpenTelemetry 지표
  - 예외
  - 트랜잭션? 시점
  - 캐시

### 할일 7.
- [ ] dapper(query) / ef core(command)

### 할일 8.
- [ ] Id 타입
- [ ] IAdapter Pipeline

### 할일 9.
- [ ] Exception 호스트 에러
- [ ] 컨테이너화
- [ ] Container HealthCheck
- [ ] Audit
- [ ] Specification???

### 할일 10.
- [ ] 회복력 adapter 레이어

### 할일 11.
- [ ] https://github.com/backstage/backstage 개발 포탈 사이트
- [ ] https://github.com/moghtech/komodo 배포
- [ ] openfeature

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

## 학습
- value task vs task
- 험블객체
- mock, ...

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