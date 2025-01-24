---
outline: deep
---

# 로그

## 개요

### 구조적 로그
![](https://messagetemplates.org/img/MessageTemplates.png)
- 출처: [Message Templates](https://messagetemplates.org/)

## Microsoft 로그 패키지
### 기본 출력
```json
{
  "Timestamp": "15:32:53 ",
  "EventId": 0,
  "LogLevel": "Information",
  "Category": "Program",
  "Message": "Hello World! Logging is fun.",
  "State": {
    "Message": "Hello World! Logging is fun.",
    "Description": "fun",
    "{OriginalFormat}": "Hello World! Logging is {Description}."
  }
}
```

![](./.images/structured-logging.png)

- 패키지
  - Microsoft.Extensions.Logging
  - Microsoft.Extensions.Logging.Console

```cs
using Microsoft.Extensions.Logging;
using System.Text.Json;

using ILoggerFactory factory = LoggerFactory.Create(builder =>
{
    //builder.AddConsole();
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss ";
        options.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
});
ILogger logger = factory.CreateLogger("Program");
logger.LogInformation("Hello World! Logging is {Description}.", "fun");
```

### 예외 출력
```json
{
    "Timestamp": "18:03:32 ",
    "EventId": 0,
    "LogLevel": "Critical",
    "Category": "Program",
    "Message": "\uBD84\uBAA8 Y\uAC12 \uD655\uC778\uC774 \uD544\uC694\uD558\uB2E4.",
    "Exception": "System.DivideByZeroException: Attempted to divide by zero.\r\n   at Cacluator.Divide(Int32 x, Int32 y) in E:\\Workspace\\observability\\logs\\StructuredSeriLoggingException\\Cacluator.cs:line 5\r\n   at Program.\u003CMain\u003E$(String[] args) in E:\\Workspace\\observability\\logs\\StructuredSeriLoggingException\\Program.cs:line 24",
    "State": {
      "Message": "\uBD84\uBAA8 Y\uAC12 \uD655\uC778\uC774 \uD544\uC694\uD558\uB2E4.",
      "{OriginalFormat}": "\uBD84\uBAA8 Y\uAC12 \uD655\uC778\uC774 \uD544\uC694\uD558\uB2E4."
    }
}
```

```cs
using ILoggerFactory factory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss ";
        options.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        };
    });
});

ILogger logger = factory.CreateLogger("Program");

var cacluator = new Cacluator();

// 예외 발생 -> 장애 처리 전략 : 호출자 책임
try
{
    cacluator.Divide(2023, 0);
}
catch (DivideByZeroException exp)
{
    logger.LogCritical(exp, "divide 0");
}

public class Cacluator
{
    public void Divide(int x, int y)
    {
        var result = x / y;
    }
}
```

## Serilog 로그 패키지
### 기본 출력
```json
{
    "Timestamp": "2024-08-22T17:49:18.1213280+09:00",
    "Level": "Information",
    "MessageTemplate": "Hello World! Logging is {Description}.",
    "Properties": {
        "Description": "fun"
    }
}
```
- 로그 형식
  ```json
  MessageTemplate": "Hello World! Logging is {Description}.",
  ```
- 로그 속성: 키/값
  ```
  "Properties": {
      "Description": "fun"
  }
  ```
- 패키지
  - Serilog.Sinks.Console

```cs
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatter: new JsonFormatter())
    .CreateLogger();

Log.Information("Hello World! Logging is {Description}.", "fun");

Log.CloseAndFlush();
```

### 예외 출력
```json
{
    "Timestamp": "2024-08-22T17:55:19.8009347+09:00",
    "Level": "Error",
    "MessageTemplate": "분모 Y값 확인이 필요하다.",
    "Exception": "System.DivideByZeroException: Attempted to divide by zero.\r\n   at Cacluator.Divide(Int32 x, Int32 y) in E:\\Ch05-StructuredSeriLoggingException\\Cacluator.cs:line 14\r\n   at Program.<Main>$(String[] args) in E:\\Ch05-StructuredSeriLoggingException\\Program.cs:line 15",
    "Properties": {
        "ExceptionDetail": {
            "Type": "System.DivideByZeroException",
            "HResult": -2147352558,
            "Message": "Attempted to divide by zero.",
            "Source": "Ch05-StructuredSeriLoggingException",
            "TargetSite": "Void Divide(Int32, Int32)"
        }
    }
}
```

- 로그 속성(예외): 키/값
  ```json
  "Properties": {
      "ExceptionDetail": {
        ...
      }
  }
  ```
- 패키지: Serilog.Exceptions

```cs
using Serilog;
using Serilog.Exceptions;           // Serilog.Exceptions 패키지
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .Enrich.WithExceptionDetails()  // Serilog.Exceptions 패키지
    .WriteTo.Console(formatter: new JsonFormatter())
    .CreateLogger();

var cacluator = new Cacluator(Log.Logger);

// 예외 발생 -> 장애 처리 전략 : 호출자 책임
try
{
    cacluator.Divide(2023, 0);
}
catch (DivideByZeroException exp)
{
    Log.Error(exp, "분모 Y값 확인이 필요하다.");
}

Log.CloseAndFlush();
```

## OpenTelemetry

### Aspire 대시보드
![](./.images/aspire-dashboard.png)

```
netstat -ano | findstr :4317
netstat -ano | findstr :9001
```

```dockerfile
FROM mcr.microsoft.com/dotnet/aspire-dashboard:9.0
```

```yml
services:
  {조직}.{프로젝트}.infra.aspire:
    env_file: .env
    image: {조직}/{프로젝트}/infra/aspire:${INFRA_TAG}
    build:
      context: .
      dockerfile: Dockerfile
    container_name: {조직}.{프로젝트}.infra.aspire
    hostname: {조직}.{프로젝트}.infra.aspire
    restart: always
    environment:
      - DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS=true
    networks:
      - net
    ports:
      - 4317:18889
      - 18888:18888
    volumes:
      - /etc/localtime:/etc/localtime
    logging: *logging-common
```

- `DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS=true`: Token 입력을 비활성화합니다.

### OpenTelemetry 전송
- 패키지: Serilog.Sinks.OpenTelemetry

```cs
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.OpenTelemetry;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatter: new JsonFormatter())
    .Enrich.WithExceptionDetails()
    .WriteTo.OpenTelemetry(x =>
    {
        //
        // OpenTelemetry Collector, Aspire Dashboard
        //
        x.Endpoint = "http://localhost:9001";
        x.Protocol = OtlpProtocol.Grpc;
    })
    .CreateLogger();

Log.Information("Hello World! Logging is {Description}.", "fun");

try
{
    Calculator calculator = new(Log.Logger);
    calculator.Divide(2023, 0);
}
catch (DivideByZeroException exp)
{
    Log.Error(exp, "분모 Y값 확인이 필요하다.");
}

Log.CloseAndFlush();
```

## Dockerization
### Dockerfile
```dockerfile
#
# Base 스테이지
#

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

#
# Build 스테이지
#

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# root 권한으로 dotnet-counter을 설치합니다.
RUN dotnet tool install --tool-path /tools dotnet-counters

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Helloworld/Helloworld.csproj", "Helloworld/"]
RUN dotnet restore "./Helloworld/Helloworld.csproj"
COPY . .
WORKDIR "/src/Helloworld"
RUN dotnet build "./Helloworld.csproj" -c $BUILD_CONFIGURATION -o /app/build

#
# Publish 스테이지
#

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Helloworld.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#
# Final 스테이지
#

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final

WORKDIR /tools
COPY --from=build /tools .
ENV PATH="/tools:${PATH}"

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Helloworld.dll"]
```

- dotnet 도구: `RUN dotnet tool install --tool-path /tools dotnet-counters`

```yml
x-logging-common: &logging-common
  driver: "json-file"
  options:
    max-size: "10m"
    max-file: "7"

services:
  #
  # service
  #
  helloworld:
    env_file: .env
    image: {조직}/{프로젝트}/helloworld
    build:
      context: .
      dockerfile: Helloworld/Dockerfile
    container_name: {조직}.{프로젝트}.helloworld
    hostname: {조직}.{프로젝트}.helloworld
    restart: always
    networks:
      - net

  #
  # aspire
  #
  {조직}.{프로젝트}.infra.aspire:
    env_file: .env
    image: {조직}/{프로젝트}/infra/aspire:${INFRA_TAG}
    build:
      context: .
      dockerfile: dockerfiles/aspire/Dockerfile
    container_name: {조직}.{프로젝트}.infra.aspire
    hostname: {조직}.{프로젝트}.infra.aspire
    restart: always
    environment:
      - DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS=true
    networks:
      - net
    ports:
      - 18889:18889   # OTLP gRPC receiver: 4317
      - 18888:18888   # Aspire 대시보드
    volumes:
      - /etc/localtime:/etc/localtime
    logging: *logging-common

  #
  # otel-collector
  #
  {조직}.{프로젝트}.infra.otel-collector:
    env_file: .env
    image: {조직}/{프로젝트}/infra/otel-collector:${INFRA_TAG}
    build:
      context: .
      dockerfile: dockerfiles/otel-collector/Dockerfile
    container_name: {조직}.{프로젝트}.infra.otel-collector
    hostname: {조직}.{프로젝트}.infra.otel-collector
    restart: always
    command: ["--config=/etc/otel-collector-config.yaml", "${OTELCOL_ARGS}"]
    volumes:
      - /etc/localtime:/etc/localtime
      - ./config/otel-collector/config.yaml:/etc/otel-collector-config.yaml
      # Linux Only
      # 호스트 루트 파일 시스템 (/) 전체를 컨테이너에 마운트합니다.
      #- /:/hostfs
    networks:
      - net
    ports:
      - "9999:8899"   # Prometheus exporter metrics
      # - "13133:13133" # health_check extension
      - "4317:4317"   # OTLP gRPC receiver
      - "4318:4318"   # OTLP HTTP receiver
    logging: *logging-common

networks:
  net:
    driver: bridge
```