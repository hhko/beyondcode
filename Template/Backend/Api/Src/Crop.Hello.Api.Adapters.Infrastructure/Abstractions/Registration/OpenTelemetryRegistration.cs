using Microsoft.Extensions.DependencyInjection;
using Crop.Hello.Framework.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using OpenTelemetry.Logs;
using OpenTelemetry;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Options.OpenTelemetry;
using Serilog.Sinks.OpenTelemetry;
using System.Net.Sockets;
using OpenTelemetry.Trace;
using OpenTelemetry.Instrumentation.Quartz;
using Quartz;
using OpenTelemetry.Exporter;
using Crop.Hello.Api.Adapters.Infrastructure.Jobs;
using Serilog.Events;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using System;
using System.Configuration;

namespace Crop.Hello.Api.Adapters.Infrastructure.Abstractions.Registration;

internal static class OpenTelemetryRegistration
{
    internal static IServiceCollection RegisterOpenTelemetry(
        this IServiceCollection services,
        IHostEnvironment environment,
        IConfiguration configuration)
    {
        OpenTelemetryOptions openTelemetryOptions = services.GetOptions<OpenTelemetryOptions>();
        //bool useOnlyConsoleExporter = openTelemetryOptions.IsLocal();
        string otlpCollectorExporterEndpoint = CreateOtlpCollectorExporterEndpoint(openTelemetryOptions.OtlpCollectorHost);

        //logging.AddSerilog();       // Microsoft Logging -> Microsoft Logging 
        //services.AddSerilog();      // Microsoft Logging -> Serilog
        services
            .AddSerilog(configure => ConfigureLog(
                configure,
                configuration,
                environment,
                openTelemetryOptions,
                otlpCollectorExporterEndpoint));

        services
            .AddOpenTelemetry()
            .AddResources(environment.EnvironmentName, openTelemetryOptions)
            .WithMetrics(builder => ConfigureMetrics(builder, otlpCollectorExporterEndpoint))
            .WithTracing(builder => ConfigureTrace(builder, environment, otlpCollectorExporterEndpoint));

        return services;
    }

    private static void ConfigureLog(
        LoggerConfiguration configure,
        IConfiguration configuration,
        IHostEnvironment environment,
        OpenTelemetryOptions openTelemetryOptions,
        string endpoint)
    {
        configure
            .ReadFrom.Configuration(configuration)
            .WriteTo.OpenTelemetry(options =>
            {
                // OTLP/gRPC: 4317
                //  - Host:     "http://127.0.0.1:4317";
                //  - Docker:   "http://host.docker.internal:4317";
                options.Endpoint = endpoint;
                options.Protocol = OtlpProtocol.Grpc;

                // 리소스
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = openTelemetryOptions.ApplicationName,
                    ["service.version"] = openTelemetryOptions.Version,
                    ["environment.name"] = environment.EnvironmentName,
                    ["team.name"] = openTelemetryOptions.TeamName
                };

                // 구조적 로그
                //
                // Level                   | Information
                // Message                 | 값1 is 값2
                // Key1                    | 값1
                // Key2                    | 값2
                // message_template.text   | {Key1} is {Key2}

                // 기본 값
                //options.IncludedData = 
                //    IncludedData.MessageTemplateTextAttribute | 
                //    IncludedData.TraceIdField | 
                //    IncludedData.SpanIdField | 
                //    IncludedData.SpecRequiredResourceAttributes;
            });

        /*
        logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;

            if (useOnlyConsoleExporter)
            {
                options.AddConsoleExporter();
            }
            else
            {
                //options.AddOtlpExporter(options => ConfigureOtlpCollectorExporter(options, openTelemetryOptions.OtlpCollectorHost));
            }

            // Resource associated with LogRecord:
            //  - service.name
            //  - service.version
            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
                serviceName: openTelemetryOptions.ApplicationName,
                serviceVersion: openTelemetryOptions.Version));

            // 분산 추적과 로그 데이터 간의 연관성 지정합니다.
            //  - 로그가 완료되면 LogRecord의 메시지를 추출하여 현재 Activity에 이벤트로 추가합니다.
            options.AddProcessor(new ActivityEventLogProcessor());
        });
        */
    }

    private static void ConfigureTrace(
        TracerProviderBuilder builder,
        IHostEnvironment environment,
        string endpoint)
    {
        // OpenTelemetry.Instrumentation.Quartz
        // https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.Quartz
        builder.AddQuartzInstrumentation();
        //    x.AddQuartzInstrumentation(
        //        opts =>
        //        {
        //            // you can trace only execute operations using this snippet
        //            opts.TracedOperations = new HashSet<string>(new[] {
        //                OperationName.Job.Execute,
        //            });

        //            // activity.IsAllDataRequested
        //            // Enable enriching an activity after it is created.
        //            opts.Enrich = (activity, eventName, quartzJobDetails) =>
        //            {
        //                // update activity
        //                if (quartzJobDetails is IJobDetail jobDetail)
        //                {
        //                    activity.SetTag("customProperty", jobDetail.JobDataMap["customProperty"]);
        //                    //...
        //                }
        //            };
        //        });

        if (environment.IsDevelopmentOrDocker())
        {
            builder.SetSampler<AlwaysOnSampler>();
        }

        //traceBuilder
        //    .AddAspNetCoreInstrumentation()
        //    .AddHttpClientInstrumentation()
        //    .AddFusionCacheInstrumentation()
        //    .AddEntityFrameworkCoreInstrumentation();

        builder.AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(endpoint);
            options.Protocol = OtlpExportProtocol.Grpc;
        });
    }

    private static void ConfigureMetrics(MeterProviderBuilder builder, string endpoint)
    {
        builder
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation();
        //.AddAspNetCoreInstrumentation()
        //.AddFusionCacheInstrumentation()
        //.AddMeter(openTelemetryOptions.Meters);

        builder.AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(endpoint);
            options.Protocol = OtlpExportProtocol.Grpc;
        });
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

    //private static void ConfigureOtlpCollectorExporter(this OtlpExporterOptions options, string otlpCollectorHost)
    //{
    //    const string _grpcCollectorPort = "4317";
    //    options.Endpoint = new Uri($"http://{otlpCollectorHost}:{_grpcCollectorPort}");
    //    options.Protocol = OtlpExportProtocol.Grpc;
    //}

    private static string CreateOtlpCollectorExporterEndpoint(string otlpCollectorHost)
    {
        const string _grpcCollectorPort = "4317";
        return $"http://{otlpCollectorHost}:{_grpcCollectorPort}";
    }
}


// 참고 소스
//
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using OpenTelemetry.Logs;
//using OpenTelemetry.Resources;
//using Serilog;
//using Serilog.Sinks.OpenTelemetry;
//using System.Diagnostics;

//class Program
//{
//    static void Main(string[] args)
//    {
//        // Configure Serilog
//        Log.Logger = new LoggerConfiguration()
//            .WriteTo.Console() // Console sink for debugging
//            .WriteTo.OpenTelemetry(options =>
//            {
//                options.ResourceAttributes = new Dictionary<string, object>
//                {
//                    { "service.name", "YourServiceName" }
//                };
//            })
//            .CreateLogger();

//        var host = Host.CreateDefaultBuilder(args)
//            .ConfigureServices(services =>
//            {
//                // Add OpenTelemetry
//                services.AddOpenTelemetry()
//                    .WithLogging(logging =>
//                    {
//                        logging.IncludeScopes = true; // Include scope information
//                        logging.IncludeFormattedMessage = true; // Log formatted messages
//                        logging.ParseStateValues = true; // Parse state values for structured logging
//                        logging.AddProcessor<SimpleLogRecordExportProcessor>(); // Optional: Custom log processors
//                    });

//                // Add additional services if needed
//            })
//            .UseSerilog() // Use Serilog for logging
//            .Build();

//        // Example logging
//        var logger = host.Services.GetRequiredService<ILogger<Program>>();

//        using (var activity = new Activity("ExampleActivity"))
//        {
//            activity.Start();
//            activity.AddEvent(new ActivityEvent("ExampleEvent"));
//            logger.LogInformation("Activity started and event added.");
//            activity.Stop();
//        }

//        host.Run();
//    }
//}
