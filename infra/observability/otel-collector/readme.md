---
outline: deep
---

# OpenTelemetry Collector

```
otel-collector
  - "9999:8899"   # Prometheus exporter metrics
  - "13133:13133" # health_check extension
  - "4317:4317"   # OTLP gRPC receiver          --> aspire dashboard
  - "4318:4318"   # OTLP HTTP receiver
```
```
netstat -ano | findstr :4317
netstat -ano | findstr :9999
```

## Config
```yaml
receivers:
  otlp:
    protocols:
      grpc:
        endpoint: "0.0.0.0:4317"
      http:
        endpoint: "0.0.0.0:4318"

processors:
  batch:

exporters:
  # debug:
  #   verbosity: detailed
  prometheus:
    endpoint: "0.0.0.0:8899"

service:
  pipelines:
    metrics:
      receivers:
        - otlp
      processors:
        - batch
      exporters:
        - prometheus

```
- Config.yaml: [링크](./config/otel-collector/config.yaml)

## Dockerfile
```dockerfile
FROM otel/opentelemetry-collector:0.106.1
```
- Dockerfile: [링크](./dockerfiles/otel-collector/Dockerfile)

## docker-compose.yml
```ini
COMPOSE_PROJECT_NAME={조직}-{프로젝트}

INFRA_TAG=1.0.1.1-infra
OTELCOL_ARGS=
```
- .env: [링크](./.env)

```yml
services:
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
```
- [docker-compose.yml]: [링크](./docker-compose.yml)

## OpenTelemetry 전송
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
        // OpenTelemetry Collector -> Aspire Dashboard
        //
        x.Endpoint = "http://localhost:4317";
        x.Protocol = OtlpProtocol.Grpc;
    })
    .CreateLogger();

Log.Information("Hello World! Logging is {Description}.", "fun");
Log.CloseAndFlush();
```
