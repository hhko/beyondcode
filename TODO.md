```
ValueObject         -> Error -> Result(Fin)                 -> Pipeline: 로그, 지표, 추적  -> OpenSearch
Entity              -> Source Generator: Id, IAdapter
Aggregate Root
```

## Error/Validation/ValueObject
- [x] Error, ManyErrors, Validation
- [ ] Fin?
- [ ] 연속 함수: 생성 이후 사용 방법???
----
- [ ] ValueObject 구현 이해
- [ ] public static ManyErrors Validate(string firstName) public한 이유
- [ ] 도메인 에러 가이드
- [ ] 도메인 네이밍컨벤션 규칙 테스트
  - public sealed record? 클래스
  - private 생성사
  - public static ManyErrors Validate 메서드

<br/>
<br/>
<br/>

## appsettings.json IOptions | 필수
- [x] IOptions + Fluent Validation
- [x] 복수개 IOptions 처리
- [x] IOptions + Fluent Validation 단위 테스트
- [ ] IOptions + Fluent Validation 통합 테스트
- [ ] IOptions + Fluent Validation 네이밍컨벤션 테스트

## appsettings.json IOptions | 개선
- [ ] FluentValidation ErrorMessage 영문 기본값
- [ ] Domain Type Validation 활용

## CI
- [ ] .runsettings 정리
- [ ] trx 생성 위치 버그
- [ ] 테스트 수행이 안됨

---
- [x] C# -> otel-collector
- [x] C# -> otel-collector -> kafka
- [x] C# -> otel-collector -> opensearch
- [x] opensearch ppl 이해
- [ ] ppl 인식 데이터 타입?
- [ ] metricbeat ppl?
- [ ] C# -> otel-collector -> kafka -> {logstash ->} opensearch
---
- [ ] Serilog + OpenTelemetry + Aspire Dashboard
- [ ] OpenSearch
- [ ] 시작 로그(로그 초기화 전): appsettings.json 형식 에러러
---
- [ ] Source Generator
- [ ] Decorator 패턴(Pipeline)
---
- [ ] Result
- [ ] Domain Error
- [ ] Application Error
- [ ] Adapter Error
- [ ] Exception
---
- [ ] Entity
- [ ] ValueObject
- [ ] Aggregate Root
- [ ] Domain Event
- [ ] Audit 로그?
---
- [ ] 회복력
---
- [ ] contextiv
- [ ] backstage
- [ ] komodo


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