---
outline: deep
---

# Aspire 대시보드

- 소스: [링크](https://github.com/hhko/beyondcode/tree/main/infra/observability/aspire)

![](./.images/aspire-dashboard.png)

```
aspire dashboard
  - 4317:18889    # OTLP gRPC receiver
  - 18888:18888   # Aspire Dashboard
```
```
netstat -ano | findstr :4317
netstat -ano | findstr :9001
```

## 폴더 구성
```shell
.env                    # 환경 설정
docker-compose.yml      # 컴포즈
dockerfiles\            # 도커 파일
  aspire\
    Dockerfile
```

## Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspire-dashboard:9.0
```
- 이미지 저장소: [링크](https://hub.docker.com/r/microsoft/dotnet-aspire-dashboard/)
- 기본 이미지를 수정하기 위해 Dockerfile을 사용합니다.

## docker-compose.yml
```ini
COMPOSE_PROJECT_NAME={조직}-{프로젝트}

INFRA_TAG=1.0.1-infra
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
      - 4317:18889      # OTLP gRPC receiver
      - 18888:18888     # WebUI
    volumes:
      - /etc/localtime:/etc/localtime
    logging: *logging-common
    stdin_open: true
    tty: true
```

- `DOTNET_DASHBOARD_UNSECURED_ALLOW_ANONYMOUS=true`: Token 입력을 비활성화합니다.

## OpenTelemetry 전송
- 패키지: Serilog.Sinks.OpenTelemetry

```cs
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.OpenTelemetry;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatter: new JsonFormatter())
    .WriteTo.OpenTelemetry(x =>
    {
        x.Endpoint = "http://localhost:4317";
        x.Protocol = OtlpProtocol.Grpc;
    })
    .CreateLogger();

Log.Information("Hello World! Logging is {Description}.", "fun");
Log.CloseAndFlush();
```