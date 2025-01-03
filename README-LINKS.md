
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
- [ ] [Domain-Driven Design Sample](https://github.com/henriquelourente/Domain-Driven-Design-Sample)

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

### Option
- [x] [Easily Validate the Options Pattern with FluentValidation](https://www.youtube.com/watch?v=I0YPTeCYvrE)
- [x] [Adding validation to strongly typed configuration objects using FluentValidation](https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-using-flentvalidation/)

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
### AOP
- [ ] [.NET AOP DynamicProxy](https://jandari91.tistory.com/102)
- [ ] [Migrating RealProxy Usage to DispatchProxy](https://devblogs.microsoft.com/dotnet/migrating-realproxy-usage-to-dispatchproxy/)

```cs
public interface IAdapter
{
    // 빈 인터페이스
}

public class Foo : IAdapter
{
    public int DoSomething(int x)
    {
        if (x < 0) throw new ArgumentException("x cannot be negative");
        return x + 1;
    }
}

public class Bar : IAdapter
{
    public string DoSomethingElse(string message)
    {
        return $"Message: {message}";
    }
}

using System;
using System.Reflection;

public class AdapterProxy<T> : DispatchProxy where T : class
{
    private T _target;

    public void SetTarget(T target)
    {
        _target = target;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        try
        {
            Console.WriteLine($"Before calling {targetMethod.Name}");
            var result = targetMethod.Invoke(_target, args);
            Console.WriteLine($"After calling {targetMethod.Name}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {targetMethod.Name}: {ex.Message}");
            throw; // 필요시 예외를 처리하거나 변환
        }
    }
}

public static class AdapterFactory
{
    public static T Create<T>(T target) where T : class, IAdapter
    {
        var proxy = DispatchProxy.Create<T, AdapterProxy<T>>() as AdapterProxy<T>;
        proxy.SetTarget(target);
        return proxy as T;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var foo = AdapterFactory.Create(new Foo());
        var bar = AdapterFactory.Create(new Bar());

        try
        {
            Console.WriteLine(foo.DoSomething(5)); // Foo의 메서드 호출
            Console.WriteLine(foo.DoSomething(-1)); // 예외 발생
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled exception: {ex.Message}");
        }

        Console.WriteLine(bar.DoSomethingElse("Hello, Bar!")); // Bar의 메서드 호출
    }
}
```

```cs
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapterWithProxy<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddTransient<TImplementation>();
        services.AddTransient<TInterface>(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            var proxy = DispatchProxy.Create<TInterface, AdapterProxy<TInterface>>() as AdapterProxy<TInterface>;
            proxy.SetTarget(implementation);
            return proxy as TInterface;
        });

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // DI 컨테이너 구성
        var services = new ServiceCollection();

        services.AddAdapterWithProxy<IAdapter, Foo>();
        services.AddAdapterWithProxy<IAdapter, Bar>();

        var serviceProvider = services.BuildServiceProvider();

        // 서비스 요청
        var foo = serviceProvider.GetRequiredService<IAdapter>();
        var bar = serviceProvider.GetRequiredService<IAdapter>();

        // 사용
        try
        {
            Console.WriteLine(foo.DoSomething(5)); // Foo의 메서드 호출
            Console.WriteLine(foo.DoSomething(-1)); // 예외 발생
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled exception: {ex.Message}");
        }

        Console.WriteLine(bar.DoSomethingElse("Hello, Bar!")); // Bar의 메서드 호출
    }
}
```

```cs
// 성능 개선
using System;
using System.Collections.Concurrent;
using System.Reflection;

public class AdapterProxy<T> : DispatchProxy where T : class
{
    private static readonly ConcurrentDictionary<Type, AdapterProxy<T>> _proxyCache = new();
    private T _target;

    private AdapterProxy() { }

    public static AdapterProxy<T> Create(T target)
    {
        var proxy = _proxyCache.GetOrAdd(typeof(T), _ => new AdapterProxy<T>());
        proxy.SetTarget(target);
        return proxy;
    }

    public void SetTarget(T target)
    {
        _target = target;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        try
        {
            Console.WriteLine($"Before calling {targetMethod.Name}");
            var result = targetMethod.Invoke(_target, args);
            Console.WriteLine($"After calling {targetMethod.Name}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {targetMethod.Name}: {ex.Message}");
            throw;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapterWithProxy<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        services.AddTransient<TImplementation>();

        services.AddTransient<TInterface>(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            return AdapterProxy<TInterface>.Create(implementation);
        });

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // DI 컨테이너 구성
        var services = new ServiceCollection();

        services.AddAdapterWithProxy<IAdapter, Foo>();
        services.AddAdapterWithProxy<IAdapter, Bar>();

        var serviceProvider = services.BuildServiceProvider();

        // 서비스 요청
        var foo = serviceProvider.GetRequiredService<IAdapter>();
        var bar = serviceProvider.GetRequiredService<IAdapter>();

        // 사용
        try
        {
            Console.WriteLine(foo.DoSomething(5)); // Foo의 메서드 호출
            Console.WriteLine(foo.DoSomething(-1)); // 예외 발생
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled exception: {ex.Message}");
        }

        Console.WriteLine(bar.DoSomethingElse("Hello, Bar!")); // Bar의 메서드 호출
    }
}
```

```cs
// dotnet add package Castle.Core
public interface IAdapter
{
    // 빈 인터페이스
}

public class Foo : IAdapter
{
    public int DoSomething(int x)
    {
        if (x < 0) throw new ArgumentException("x cannot be negative");
        return x + 1;
    }
}

public class Bar : IAdapter
{
    public string DoSomethingElse(string message)
    {
        return $"Message: {message}";
    }
}

using Castle.DynamicProxy;
using System;

public class AdapterInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        try
        {
            // 메서드 호출 전 공통 작업
            Console.WriteLine($"Before calling {invocation.Method.Name}");

            // 메서드 호출
            invocation.Proceed();

            // 메서드 호출 후 공통 작업
            Console.WriteLine($"After calling {invocation.Method.Name}");
        }
        catch (Exception ex)
        {
            // 예외 처리
            Console.WriteLine($"Exception in {invocation.Method.Name}: {ex.Message}");
            throw; // 예외를 다시 던지거나, 필요한 경우 처리
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapterWithDynamicProxy<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var proxyGenerator = new ProxyGenerator();
        services.AddTransient<TImplementation>();

        // 프록시 생성 및 인터셉터 설정
        services.AddTransient<TInterface>(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            var interceptor = new AdapterInterceptor();
            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, interceptor);
        });

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // DI 컨테이너 구성
        var services = new ServiceCollection();

        services.AddAdapterWithDynamicProxy<IAdapter, Foo>();
        services.AddAdapterWithDynamicProxy<IAdapter, Bar>();

        var serviceProvider = services.BuildServiceProvider();

        // 서비스 요청
        var foo = serviceProvider.GetRequiredService<IAdapter>();
        var bar = serviceProvider.GetRequiredService<IAdapter>();

        // 사용
        try
        {
            Console.WriteLine(foo.DoSomething(5)); // Foo의 메서드 호출
            Console.WriteLine(foo.DoSomething(-1)); // 예외 발생
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled exception: {ex.Message}");
        }

        Console.WriteLine(bar.DoSomethingElse("Hello, Bar!")); // Bar의 메서드 호출
    }
}
```

```cs
// n개
using Castle.DynamicProxy;
using System;

public class ExceptionHandlingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        try
        {
            invocation.Proceed();  // 실제 메서드 실행
        }
        catch (Exception ex)
        {
            // 예외 처리 로직
            Console.WriteLine($"Exception in {invocation.Method.Name}: {ex.Message}");
            throw; // 예외를 다시 던짐
        }
    }
}

using Castle.DynamicProxy;
using System;

public class LoggingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        Console.WriteLine($"Calling method {invocation.Method.Name} with arguments {string.Join(", ", invocation.Arguments)}");
        
        invocation.Proceed();  // 실제 메서드 실행
        
        Console.WriteLine($"Method {invocation.Method.Name} returned {invocation.ReturnValue}");
    }
}

using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapterWithDynamicProxy<TInterface, TImplementation>(
        this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var proxyGenerator = new ProxyGenerator();
        
        // 프록시 객체 생성 시 여러 인터셉터를 사용할 수 있도록 설정
        services.AddTransient<TImplementation>();

        services.AddTransient<TInterface>(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            var interceptors = new IInterceptor[]
            {
                provider.GetRequiredService<LoggingInterceptor>(),  // 로그 인터셉터
                provider.GetRequiredService<ExceptionHandlingInterceptor>()  // 예외 처리 인터셉터
            };
            
            // 인터셉터 배열을 전달하여 프록시 생성
            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, interceptors);
        });

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // DI 컨테이너 구성
        var services = new ServiceCollection();

        // 인터셉터 등록
        services.AddTransient<LoggingInterceptor>();
        services.AddTransient<ExceptionHandlingInterceptor>();

        // 서비스 및 프록시 등록
        services.AddAdapterWithDynamicProxy<IAdapter, Foo>();

        var serviceProvider = services.BuildServiceProvider();

        // 서비스 요청
        var foo = serviceProvider.GetRequiredService<IAdapter>();

        // 사용
        try
        {
            Console.WriteLine(foo.DoSomething(5)); // Foo의 메서드 호출
            Console.WriteLine(foo.DoSomething(-1)); // 예외 발생
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Handled exception: {ex.Message}");
        }
    }
}

using Castle.DynamicProxy;
using System;
using System.Diagnostics;

public class PerformanceInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var stopwatch = Stopwatch.StartNew();
        
        invocation.Proceed();  // 실제 메서드 실행
        
        stopwatch.Stop();
        Console.WriteLine($"Method {invocation.Method.Name} executed in {stopwatch.ElapsedMilliseconds}ms");
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapterWithDynamicProxy<TInterface, TImplementation>(
        this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var proxyGenerator = new ProxyGenerator();
        
        services.AddTransient<TImplementation>();

        services.AddTransient<TInterface>(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            var interceptors = new IInterceptor[]
            {
                provider.GetRequiredService<LoggingInterceptor>(),  // 로그 인터셉터
                provider.GetRequiredService<ExceptionHandlingInterceptor>(),  // 예외 처리 인터셉터
                provider.GetRequiredService<PerformanceInterceptor>()  // 성능 측정 인터셉터
            };
            
            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, interceptors);
        });

        return services;
    }
}
```

```cs
public interface IResult
{
    bool Success { get; }
    string Message { get; }
}

public class SuccessResult : IResult
{
    public SuccessResult(string message)
    {
        Success = true;
        Message = message;
    }

    public bool Success { get; }
    public string Message { get; }
}

public interface IErrorResult : IResult
{
    string ErrorCode { get; }
    string Details { get; }
}

public class ErrorResult : IErrorResult
{
    public ErrorResult(string message, string errorCode = null, string details = null)
    {
        Success = false;
        Message = message;
        ErrorCode = errorCode;
        Details = details;
    }

    public bool Success { get; }
    public string Message { get; }
    public string ErrorCode { get; }
    public string Details { get; }
}


public interface IAdapter
{
    // 이 인터페이스는 메서드를 정의하지 않습니다.
}

using Castle.DynamicProxy;
using System;

public class ExceptionHandlingInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        try
        {
            invocation.Proceed();  // 실제 메서드 호출
        }
        catch (Exception ex)
        {
            // 예외 발생 시 ErrorResult 반환
            var errorResult = new ErrorResult(
                $"Exception in {invocation.Method.Name}: {ex.Message}",
                "EXCEPTION_OCCURRED",
                ex.StackTrace
            );
            invocation.ReturnValue = errorResult;
        }
    }
}

public class Foo : IAdapter
{
    public IResult DoSomething(int x)
    {
        // 예외 처리 없이 정상 결과 반환
        return new SuccessResult($"Processed {x}");
    }
}

public class Bar : IAdapter
{
    public IResult DoSomethingElse(string message)
    {
        // 예외 처리 없이 정상 결과 반환
        return new SuccessResult($"Processed message: {message}");
    }
}

using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAdapterWithDynamicProxy<TInterface, TImplementation>(
        this IServiceCollection services)
        where TInterface : class
        where TImplementation : class, TInterface
    {
        var proxyGenerator = new ProxyGenerator();

        services.AddTransient<TImplementation>();

        services.AddTransient<TInterface>(provider =>
        {
            var implementation = provider.GetRequiredService<TImplementation>();
            var interceptors = new IInterceptor[]
            {
                provider.GetRequiredService<ExceptionHandlingInterceptor>(),  // 예외 처리 인터셉터
                // 필요한 다른 인터셉터들 추가 가능
            };
            return proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(implementation, interceptors);
        });

        return services;
    }
}

using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // DI 컨테이너 구성
        var services = new ServiceCollection();

        // 인터셉터 등록
        services.AddTransient<ExceptionHandlingInterceptor>();

        // 서비스 및 프록시 등록
        services.AddAdapterWithDynamicProxy<IAdapter, Foo>();
        services.AddAdapterWithDynamicProxy<IAdapter, Bar>();

        var serviceProvider = services.BuildServiceProvider();

        // 서비스 요청
        var foo = serviceProvider.GetRequiredService<IAdapter>();
        var bar = serviceProvider.GetRequiredService<IAdapter>();

        // Foo 사용
        var resultFoo = ((Foo)foo).DoSomething(5);
        Console.WriteLine($"Foo Result: {resultFoo.Message}");

        // 예외 발생을 시도해보겠습니다
        var resultFooError = ((Foo)foo).DoSomething(-1);  // 예외가 발생하지 않지만, 예외 처리 인터셉터로 처리됨
        Console.WriteLine($"Foo Error Result: {resultFooError.Message}");

        // Bar 사용
        var resultBar = ((Bar)bar).DoSomethingElse("Hello!");
        Console.WriteLine($"Bar Result: {resultBar.Message}");
    }
}


// 테스트
//dotnet add package xunit
//dotnet add package Moq
//dotnet add package Castle.Core

using Castle.DynamicProxy;
using Moq;
using System;
using Xunit;

public class AdapterTests
{
    // 예외 처리 인터셉터 테스트
    [Fact]
    public void Foo_ShouldReturnErrorResult_WhenExceptionOccurs()
    {
        // Arrange
        var interceptor = new Mock<IInterceptor>();
        var foo = new Foo();
        var proxyGenerator = new ProxyGenerator();

        // 인터셉터 동작 정의: 예외를 던지도록 설정
        interceptor.Setup(i => i.Intercept(It.IsAny<IInvocation>())).Callback<IInvocation>(invocation =>
        {
            invocation.Proceed();  // 메서드 실행
            throw new InvalidOperationException("Test exception");
        });

        var proxy = proxyGenerator.CreateInterfaceProxyWithTarget<IAdapter>(foo, interceptor.Object);

        // Act
        var result = proxy.DoSomething(5);

        // Assert
        Assert.False(result.Success);  // 실패한 경우
        Assert.Equal("Exception in DoSomething: Test exception", result.Message);  // 예외 메시지 확인
    }

    [Fact]
    public void Foo_ShouldReturnSuccessResult_WhenNoExceptionOccurs()
    {
        // Arrange
        var foo = new Foo();

        // Act
        var result = foo.DoSomething(5);

        // Assert
        Assert.True(result.Success);  // 성공한 경우
        Assert.Equal("Processed 5", result.Message);  // 메시지 확인
    }

    [Fact]
    public void Bar_ShouldReturnSuccessResult_WhenNoExceptionOccurs()
    {
        // Arrange
        var bar = new Bar();

        // Act
        var result = bar.DoSomethingElse("Hello!");

        // Assert
        Assert.True(result.Success);  // 성공한 경우
        Assert.Equal("Processed message: Hello!", result.Message);  // 메시지 확인
    }
}

using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

public class ServiceTests
{
    [Fact]
    public void ShouldReturnErrorResult_WhenExceptionIsThrownInProxy()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddTransient<ExceptionHandlingInterceptor>();
        services.AddAdapterWithDynamicProxy<IAdapter, Foo>();  // Foo에 대한 프록시 등록

        var serviceProvider = services.BuildServiceProvider();
        var foo = serviceProvider.GetRequiredService<IAdapter>();  // Foo 인터페이스 요청

        // Act
        var result = foo.DoSomething(5);  // 정상적인 호출
        var resultError = foo.DoSomething(-1);  // 예외 발생을 유발할 수 있는 호출

        // Assert
        Assert.True(result.Success);  // 정상적인 결과
        Assert.Equal("Processed 5", result.Message);

        Assert.False(resultError.Success);  // 예외 처리된 결과
        Assert.Equal("Exception in DoSomething: Test exception", resultError.Message);
    }
}

// Foo_ShouldReturnErrorResult_WhenExceptionOccurs
//    이 테스트는 Foo 클래스에서 예외가 발생할 때, ExceptionHandlingInterceptor가 예외를 처리하고 ErrorResult를 반환하는지 확인합니다.
//    Moq를 사용하여 IInterceptor를 모킹하고, 예외가 발생하도록 설정합니다. 그런 다음 결과가 ErrorResult인지 확인합니다.
// Foo_ShouldReturnSuccessResult_WhenNoExceptionOccurs
//    이 테스트는 예외가 발생하지 않고 Foo 클래스에서 정상적인 결과를 반환하는지 확인합니다.
//    Bar_ShouldReturnSuccessResult_WhenNoExceptionOccurs
// Bar 클래스에서 예외 없이 정상적인 결과를 반환하는지 확인하는 테스트입니다.
//    ShouldReturnErrorResult_WhenExceptionIsThrownInProxy
//    DI 컨테이너와 Castle DynamicProxy를 사용하여 Foo 클래스에 대한 프록시를 생성하고, 예외가 발생할 때 예외가 제대로 처리되는지 테스트합니다.
```

```cs
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Castle.DynamicProxy;
using System;
using System.Linq;

public class PerformanceTests
{
    private ProxyGenerator _proxyGenerator;
    private IAdapter _fooProxy;
    private IAdapter _barProxy;
    private IAdapter _foo;
    private IAdapter _bar;

    public PerformanceTests()
    {
        _proxyGenerator = new ProxyGenerator();
        _foo = new Foo();
        _bar = new Bar();
    }

    // 성능 테스트 - Foo 클래스의 메서드 실행
    [Benchmark]
    public IResult TestFooWithoutInterceptor()
    {
        return _foo.DoSomething(5);
    }

    // 성능 테스트 - Foo 클래스의 메서드 실행 (인터셉터 포함)
    [Benchmark]
    public IResult TestFooWithInterceptor()
    {
        var interceptor = new ExceptionHandlingInterceptor();
        _fooProxy = _proxyGenerator.CreateInterfaceProxyWithTarget<IAdapter>(_foo, interceptor);
        return _fooProxy.DoSomething(5);
    }

    // 성능 테스트 - Bar 클래스의 메서드 실행
    [Benchmark]
    public IResult TestBarWithoutInterceptor()
    {
        return _bar.DoSomethingElse("Hello!");
    }

    // 성능 테스트 - Bar 클래스의 메서드 실행 (인터셉터 포함)
    [Benchmark]
    public IResult TestBarWithInterceptor()
    {
        var interceptor = new ExceptionHandlingInterceptor();
        _barProxy = _proxyGenerator.CreateInterfaceProxyWithTarget<IAdapter>(_bar, interceptor);
        return _barProxy.DoSomethingElse("Hello!");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<PerformanceTests>();
    }
}
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

### MediatR
- [ ] [5 Amazing Use Cases for MediatR Pipelines - Cross-Cutting Concerns](https://www.youtube.com/watch?v=Iql4yjHYRiA)
  - 예외
  - 로그
  - 유효성 검사
  - Command 트랜잭션
  - Query 캐싱
