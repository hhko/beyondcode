using Microsoft.Extensions.DependencyInjection;
using Crop.Hello.Framework.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using OpenTelemetry.Logs;
using OpenTelemetry;
using Microsoft.Extensions.Logging;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;
using Serilog.Sinks.OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using System.Net;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class OpenTelemetryRegistration
{
    private const string OLTP_COLLECTOR_EXPORTER_GRPC_PORT = "4317";

    internal static IServiceCollection RegisterOpenTelemetry(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        OpenTelemetryOptions openTelemetryOptions = services.GetOptions<OpenTelemetryOptions>();
        string otlpCollectorExporterEndpoint = $"http://{openTelemetryOptions.OtlpCollectorHost}:{OLTP_COLLECTOR_EXPORTER_GRPC_PORT}";

        // Log
        //  - logging.AddSerilog();       // Microsoft Logging -> Microsoft Logging 
        //  - services.AddSerilog();      // Microsoft Logging -> Serilog
        //
        // 구조적 로그
        //  Level                   | Information
        //  Message                 | 값1 is 값2
        //  Key1                    | 값1
        //  Key2                    | 값2
        //  message_template.text   | {Key1} is {Key2}
        services
            .AddSerilog(configure =>
            {
                configure
                    .ReadFrom.Configuration(configuration)
                    .WriteTo.OpenTelemetry(options =>
                    {
                        //// OTLP/gRPC: 4317
                        ////  - Host:     "http://127.0.0.1:4317";
                        ////  - Docker:   "http://host.docker.internal:4317";
                        options.Endpoint = otlpCollectorExporterEndpoint;
                        options.Protocol = OtlpProtocol.Grpc;

                        // 리소스
                        options.ResourceAttributes = new Dictionary<string, object>
                        {
                            ["service.name"] = openTelemetryOptions.ApplicationName,
                            ["service.version"] = openTelemetryOptions.Version,
                            ["environment.name"] = environment.EnvironmentName,
                            ["team.name"] = openTelemetryOptions.TeamName
                        };
                    });
            });

        // Metric, Trace
        services
            .AddOpenTelemetry()
            .AddResources(environment.EnvironmentName, openTelemetryOptions)
            .WithMetrics(builder =>
            {
                builder
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    //.AddAspNetCoreInstrumentation()
                    //.AddFusionCacheInstrumentation()
                    .AddMeter(openTelemetryOptions.Meters);
            })
            .WithTracing(builder =>
            {
                if (environment.IsDevelopmentOrLocal())
                {
                    builder.SetSampler<AlwaysOnSampler>();
                }

                // OpenTelemetry.Instrumentation.Quartz
                // https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.Quartz
                builder.AddQuartzInstrumentation();

            })
            .UseOtlpExporter(OtlpExportProtocol.Grpc, new Uri(otlpCollectorExporterEndpoint));

        return services;
    }

    //// ActivityEventLogProcessor는 LogRecord와 OpenTelemetry의 Activity를 연결하는 데 사용됩니다.
    ////  로그가 완료되면 LogRecord의 메시지를 추출하여 현재 Activity에 이벤트로 추가합니다.
    ////  이를 통해, 분산 추적과 로그 데이터 간의 연관성을 강화하여 더 나은 관찰 가능성을 제공합니다.
    //private sealed class ActivityEventLogProcessor : BaseProcessor<LogRecord>
    //{
    //    public override void OnEnd(LogRecord log)
    //    {
    //        base.OnEnd(log);
    //        Activity.Current?.AddEvent(new ActivityEvent(log.FormattedMessage!));
    //    }
    //}

    private static IOpenTelemetryBuilder AddResources(
        this IOpenTelemetryBuilder builder,
        string environment,
        OpenTelemetryOptions openTelemetryOptions)
    {
        return builder.ConfigureResource(resourceBuilder => resourceBuilder
            .AddService(
                serviceName: openTelemetryOptions.ApplicationName, 
                serviceVersion: openTelemetryOptions.Version)
            .AddAttributes(new Dictionary<string, object>
            {
                ["environment.name"] = environment,
                ["team.name"] = openTelemetryOptions.TeamName
            }));
    }
}
