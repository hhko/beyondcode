---
outline: deep
---

# OpenTelemetry Collector

- 소스: [링크](https://github.com/hhko/beyondcode/tree/main/infra/observability/otel-collector)

```
otel-collector
  - 8899:8899     # Prometheus exporter metrics
  - 13133:13133   # health_check extension
  - 4317:4317     # OTLP gRPC receiver
  - 4318:4318     # OTLP HTTP receiver

aspire dashboard
  - 18889:18889   # OTLP gRPC receiver
  - 18888:18888   # Aspire Dashboard
```
```
netstat -ano | findstr :4317
netstat -ano | findstr :8899
```

## 폴더 구성
```shell
.env                    # 환경 설정
docker-compose.yml      # 컴포즈
config\                 # 서비스 설정
  otel-collector\
    config.yaml
dockerfiles\            # 도커 파일
  aspire\
    Dockerfile
  otel-collector\
    Dockerfile
```

## Config
```yaml
receivers:
  otlp:
    protocols:
      # OTLP gRPC receiver
      grpc:
        endpoint: "0.0.0.0:4317"
      # OTLP HTTP receiver
      http:
        endpoint: "0.0.0.0:4318"

processors:
  batch:

exporters:
  # debug:
  #   verbosity: detailed

  # Prometheus exporter metrics
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

## Dockerfile
```dockerfile
FROM otel/opentelemetry-collector:0.106.1
```
- 기본 이미지를 수정하기 위해 Dockerfile을 사용합니다.

## docker-compose.yml
```ini
COMPOSE_PROJECT_NAME={조직}-{프로젝트}

INFRA_TAG=1.0.1-infra
OTELCOL_ARGS=
```

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
      - "8899:8899"   # Prometheus exporter metrics
      # - "13133:13133" # health_check extension
      - "4317:4317"   # OTLP gRPC receiver
      - "4318:4318"   # OTLP HTTP receiver
    logging: *logging-common
    stdin_open: true
    tty: true
```

## OpenTelemetry 전송
- 패키지: Serilog.Sinks.OpenTelemetry

```cs
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.OpenTelemetry;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatter: new JsonFormatter())
    .WriteTo.OpenTelemetry(x =>             // Serilog.Sinks.OpenTelemetry
    {
        x.Endpoint = "http://localhost:4317";
        x.Protocol = OtlpProtocol.Grpc;
    })
    .CreateLogger();

Log.Information("Hello World! Logging is {Description}.", "fun");
Log.CloseAndFlush();
```
